using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{
    public Transform m_camera_transform;
    public CharacterController m_character_controller;

    private float m_walk_speed = 5.0f;      // 걷기 속도
    private float m_run_speed = 7.0f;       // 뛰기 속도
    private bool m_is_running = false;      // 걷기/뛰기 여부
    private bool m_is_walking = false;      // 움직임 여부
    public bool m_is_crouching = false;     // 앉기 여부
    private float m_gravity = -20.0f;       // 플레이어가 받는 중력
    private float m_jump_speed = 10.0f;
    public AudioClip m_jump_audio_clip;
    private float m_y_velocity = 0.0f;      // y축 속도
    private float stamina = 100.0f;         // 달리기 게이지의 초기 값
    private float staminaDecreaseRate = 15.0f; // 초당 감소하는 스테미나의 양
    private float staminaIncreaseRate = 10.0f; // 초당 증가하는 스테미나의 양
    private bool m_is_staminaCharge = false;  // 스테미나가 증가할 수 있는 상태의 여부
    public Slider staminaSlider;           // UI 슬라이더를 사용하여 스테미나를 표시
    private AudioSource m_audio_source;
    public AudioClip m_walk_audio_clip; // 걷기 사운드 클립
    public AudioClip m_run_audio_clip;  // 뛰기 사운드 클립
    public AudioClip m_crouch_audio_clip; // 앉기 사운드 클립
    private float m_original_walk_speed;

    private bool isHeightTransitioning = false; // 보간이 진행 중인지 여부
    private float heightTransitionTime = 0.0f; // 보간 진행 시간
    private float heightTransitionDuration = 0.2f; // 보간 시간

    public static bool is_interacting_vent = false;
    public static bool isHiding;


    private void Start()
    {
        m_audio_source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameManager.m_can_player_move)
        {
            HandleMovementInput();
            UpdateStamina();

            if(!m_is_running)
                if (Input.GetKeyDown(KeyCode.C)) // 'C' 키를 처음 눌렀을 때 앉기 동작 실행
                    ToggleCrouch();

            if (Input.GetKeyUp(KeyCode.C)) // 'C' 키를 뗄 때 앉기 동작 해제
                ToggleCrouchAudio();

            PlayAudio(); // 이동 입력 후 PlayAudio()를 호출합니다.

            // 카메라 높이 보간
            float targetCameraHeightOffset = m_is_crouching ? 2f : 2.5f; // 카메라 높이 변경 값을 설정합니다.
            Vector3 targetCameraPosition = m_character_controller.transform.position + new Vector3(0f, targetCameraHeightOffset, 0f);
            m_camera_transform.position = Vector3.Lerp(m_camera_transform.position, targetCameraPosition, Time.deltaTime * 5f);
        }
    }

    private void HandleMovementInput()
    {
        if (!is_interacting_vent)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vectical = Input.GetAxis("Vertical");

            Vector3 move_direction = new Vector3(horizontal, 0, vectical);

            if (!m_is_crouching)
            {
                if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
                {
                    m_is_running = true;
                    stamina -= staminaDecreaseRate * Time.deltaTime;
                    m_audio_source.clip = m_run_audio_clip;
                }
                else
                {
                    m_is_running = false;
                    if (Input.GetKey(KeyCode.LeftShift))
                        m_is_staminaCharge = false;
                    else
                        m_is_staminaCharge = true;

                    if (m_is_staminaCharge)
                        stamina += staminaIncreaseRate * Time.deltaTime;
                    m_audio_source.clip = m_walk_audio_clip;
                }
            }
            else
            {
                m_is_staminaCharge = true;

                if (m_is_staminaCharge)
                    stamina += staminaIncreaseRate * Time.deltaTime;
            }

            if (stamina <= 0)
                m_is_running = false;

            if (move_direction != Vector3.zero)
                m_is_walking = true;
            else
                m_is_walking = false;

            float speed = m_is_running ? m_run_speed : m_walk_speed;
            speed = m_is_crouching ? speed / 2f : speed; // 앉은 상태에서 이동 속도 감소

            stamina = Mathf.Clamp(stamina, 0, 100);
            staminaSlider.value = stamina / 100.0f;
            staminaSlider.gameObject.SetActive(m_is_running || stamina < 100);

            move_direction = m_camera_transform.TransformDirection(move_direction);
            move_direction *= speed;

            // 점프 관련된 부분으로 필요할 때 사용
            if (m_character_controller.isGrounded)
            {
                m_y_velocity = 0;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_y_velocity = m_jump_speed;
                    m_audio_source.clip = m_jump_audio_clip;
                }
            }

            if (isHeightTransitioning)
            {
                heightTransitionTime += Time.deltaTime;

                float targetHeight = m_is_crouching ? 0.5f : 2f;
                float currentHeight = Mathf.Lerp(
                    m_character_controller.height,
                    targetHeight,
                    heightTransitionTime / heightTransitionDuration
                );
                m_character_controller.height = currentHeight;

                if (heightTransitionTime >= heightTransitionDuration)
                {
                    isHeightTransitioning = false;
                    heightTransitionTime = 0.0f;
                }
                if (m_character_controller.enabled) // CharacterController가 활성화되었는지 확인
                {
                    m_character_controller.Move(move_direction * Time.deltaTime);
                }
            }

            m_y_velocity += (m_gravity * Time.deltaTime);
            move_direction.y = m_y_velocity;

            m_character_controller.Move(move_direction * Time.deltaTime);
        }
    }

    private void ToggleCrouch()
    {
        m_is_crouching = !m_is_crouching;
        isHeightTransitioning = true; // 보간 시작

        if (m_is_crouching)
        {
            // 앉은 상태로 컨트롤러 높이 조정
            float targetHeight = 0.5f;
            m_character_controller.height = Mathf.Lerp(m_character_controller.height, targetHeight, Time.deltaTime * 5f);
          
            // 이동 속도 조정
            m_original_walk_speed = m_walk_speed; // 원래의 이동 속도를 저장
            m_walk_speed = m_walk_speed * 0.8f; // 현재 이동 속도를 절반으로 줄임
        }
        else
        {
            // 서 있는 상태로 컨트롤러 높이 조정
            float targetHeight = 2f;
            m_character_controller.height = Mathf.Lerp(m_character_controller.height, targetHeight, Time.deltaTime * 5f);

            // 이동 속도 원래대로 복구
            m_walk_speed = m_original_walk_speed;
        }

        if (m_is_crouching)
            m_audio_source.clip = m_crouch_audio_clip;
        else
            m_audio_source.clip = m_walk_audio_clip;
    }

    private void ToggleCrouchAudio()
    {
        if (m_is_crouching && m_is_walking) // 앉았을 때 걷는 중인 경우에만 앉은 상태를 유지하며 오디오를 재생합니다.
        {
            if (!m_audio_source.isPlaying)
                m_audio_source.Play();
        }
        else
        {
            m_audio_source.Stop();
        }
    }

    private void UpdateStamina()
    {
        // 스테미나 감소/증가 처리 및 UI 업데이트
        // ...
    }

    private void PlayAudio()
    {
        if (CharacterMove.isHiding)
        {
            if (m_is_walking && !m_is_crouching)
            {
                if (!m_audio_source.isPlaying)
                    m_audio_source.Play();
            }
            else
            {
                m_audio_source.Stop();
            }
        }
    }
}
