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

    private float m_walk_speed = 5.0f;      // �ȱ� �ӵ�
    private float m_run_speed = 7.0f;       // �ٱ� �ӵ�
    private bool m_is_running = false;      // �ȱ�/�ٱ� ����
    private bool m_is_walking = false;      // ������ ����
    public bool m_is_crouching = false;     // �ɱ� ����
    private float m_gravity = -20.0f;       // �÷��̾ �޴� �߷�
    private float m_jump_speed = 10.0f;
    public AudioClip m_jump_audio_clip;
    private float m_y_velocity = 0.0f;      // y�� �ӵ�
    private float stamina = 100.0f;         // �޸��� �������� �ʱ� ��
    private float staminaDecreaseRate = 15.0f; // �ʴ� �����ϴ� ���׹̳��� ��
    private float staminaIncreaseRate = 10.0f; // �ʴ� �����ϴ� ���׹̳��� ��
    private bool m_is_staminaCharge = false;  // ���׹̳��� ������ �� �ִ� ������ ����
    public Slider staminaSlider;           // UI �����̴��� ����Ͽ� ���׹̳��� ǥ��
    private AudioSource m_audio_source;
    public AudioClip m_walk_audio_clip; // �ȱ� ���� Ŭ��
    public AudioClip m_run_audio_clip;  // �ٱ� ���� Ŭ��
    public AudioClip m_crouch_audio_clip; // �ɱ� ���� Ŭ��
    private float m_original_walk_speed;

    private bool isHeightTransitioning = false; // ������ ���� ������ ����
    private float heightTransitionTime = 0.0f; // ���� ���� �ð�
    private float heightTransitionDuration = 0.2f; // ���� �ð�

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
                if (Input.GetKeyDown(KeyCode.C)) // 'C' Ű�� ó�� ������ �� �ɱ� ���� ����
                    ToggleCrouch();

            if (Input.GetKeyUp(KeyCode.C)) // 'C' Ű�� �� �� �ɱ� ���� ����
                ToggleCrouchAudio();

            PlayAudio(); // �̵� �Է� �� PlayAudio()�� ȣ���մϴ�.

            // ī�޶� ���� ����
            float targetCameraHeightOffset = m_is_crouching ? 2f : 2.5f; // ī�޶� ���� ���� ���� �����մϴ�.
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
            speed = m_is_crouching ? speed / 2f : speed; // ���� ���¿��� �̵� �ӵ� ����

            stamina = Mathf.Clamp(stamina, 0, 100);
            staminaSlider.value = stamina / 100.0f;
            staminaSlider.gameObject.SetActive(m_is_running || stamina < 100);

            move_direction = m_camera_transform.TransformDirection(move_direction);
            move_direction *= speed;

            // ���� ���õ� �κ����� �ʿ��� �� ���
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
                if (m_character_controller.enabled) // CharacterController�� Ȱ��ȭ�Ǿ����� Ȯ��
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
        isHeightTransitioning = true; // ���� ����

        if (m_is_crouching)
        {
            // ���� ���·� ��Ʈ�ѷ� ���� ����
            float targetHeight = 0.5f;
            m_character_controller.height = Mathf.Lerp(m_character_controller.height, targetHeight, Time.deltaTime * 5f);
          
            // �̵� �ӵ� ����
            m_original_walk_speed = m_walk_speed; // ������ �̵� �ӵ��� ����
            m_walk_speed = m_walk_speed * 0.8f; // ���� �̵� �ӵ��� �������� ����
        }
        else
        {
            // �� �ִ� ���·� ��Ʈ�ѷ� ���� ����
            float targetHeight = 2f;
            m_character_controller.height = Mathf.Lerp(m_character_controller.height, targetHeight, Time.deltaTime * 5f);

            // �̵� �ӵ� ������� ����
            m_walk_speed = m_original_walk_speed;
        }

        if (m_is_crouching)
            m_audio_source.clip = m_crouch_audio_clip;
        else
            m_audio_source.clip = m_walk_audio_clip;
    }

    private void ToggleCrouchAudio()
    {
        if (m_is_crouching && m_is_walking) // �ɾ��� �� �ȴ� ���� ��쿡�� ���� ���¸� �����ϸ� ������� ����մϴ�.
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
        // ���׹̳� ����/���� ó�� �� UI ������Ʈ
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
