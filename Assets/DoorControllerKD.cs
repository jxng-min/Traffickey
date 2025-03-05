using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorControllerKD : MonoBehaviour
{
    public float raycastDistance = 2f;
    

    private Animator doorAnimator;
    public bool is_open = false;

    public Image m_screen_fade_image;
    public bool is_near_vent = false;
    private bool isFading = false;

    public TextMeshProUGUI m_vent_text;

    private CharacterController characterController;

    public AudioClip door_open_sound; // 문 열림 사운드
    public AudioClip door_close_sound; // 문 닫힘 사운드
    public AudioClip vent_enter_sound; // 환풍구 진입 사운드

    public bool is_near_hide = false;
    public TextMeshProUGUI m_hide_text;

    public TextMeshProUGUI m_ladder_text; // 사다리 상호작용 텍스트
    private bool is_near_ladder = false; // 사다리 근처 여부    

    public TextMeshProUGUI m_ladder_text2;
    


    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        m_ladder_text2.enabled = false;
    }

    private void Update()
    {
        Vector3 raycastOrigin = Camera.main.transform.position;
        Vector3 raycastDirection = Camera.main.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastDistance))
        {
            Debug.DrawRay(raycastOrigin, raycastDirection * hit.distance, Color.green);
            if (hit.collider.CompareTag("Door"))
            {
                Animator doorAnimator = hit.collider.GetComponent<Animator>();
                AudioSource audio_source = hit.collider.GetComponent<AudioSource>();


                if (Input.GetKeyDown(KeyCode.E))
                {
                    bool isOpen = doorAnimator.GetBool("open");
                    doorAnimator.SetBool("open", !isOpen);

                    if (!isOpen)
                    {
                        audio_source.clip = door_close_sound;
                        audio_source.Play();
                    }
                    else
                    {
                        audio_source.clip = door_open_sound;
                        audio_source.Play();
                    }
                }
            }

            if (hit.collider.CompareTag("Ladder"))
            {
                is_near_ladder = true;
                if (Input.GetKeyDown(KeyCode.E))
                {                    
                    if (KeyCounter.HasAllKeys())
                    {
                        // 'End' 씬으로 전환
                        UnityEngine.SceneManagement.SceneManager.LoadScene("End");
                        GameManager.m_game_state = GameManager.GAMESTATE.GAMEOVER;
                        m_ladder_text2.enabled = false; // 열쇠가 충분할 때 메시지 비활성화
                    }
                    else
                    {
                        
                        m_ladder_text2.text = "열쇠가 부족합니다";
                        m_ladder_text2.enabled = true;
                        
                        StartCoroutine(DisableTextAfterTime(1f, m_ladder_text2));
                    }
                }
            }
            else
            {
                is_near_ladder = false;
            }



            if (hit.collider.CompareTag("Vent1"))
            {
                is_near_vent = true;
                if (Input.GetKeyDown(KeyCode.E) && !isFading)
                {
                    CharacterMove.is_interacting_vent = true;
                    StartCoroutine(FadeOutAndMove(new Vector3(42f, transform.position.y, -24f)));
                }
            }

            if (hit.collider.CompareTag("Vent2"))
            {
                is_near_vent = true;
                if (Input.GetKeyDown(KeyCode.E) && !isFading)
                {
                    CharacterMove.is_interacting_vent = true;
                    StartCoroutine(FadeOutAndMove(new Vector3(-35f, transform.position.y, 35f)));
                }
            }

            if (hit.collider.CompareTag("Hide"))
            {
                is_near_hide = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CharacterMove.isHiding = !CharacterMove.isHiding; // 숨는 상태를 토글
                                                                      // 추가적인 숨기기 로직 (캐릭터를 보이지 않게 만들거나 등등)
                }
            }


            Debug.DrawRay(raycastOrigin, raycastDirection * hit.distance, Color.green);
        }
        else
        {
            is_near_vent = false;
            is_near_hide = false;
            is_near_ladder = false; // 사다리 근처가 아님
        }

        if (is_near_ladder)
        {
            if (!m_ladder_text2.enabled) // m_ladder_text2가 비활성화된 경우에만 m_ladder_text를 활성화
            {
                m_ladder_text.text = "탈출구를 이용하려면 E를 누르세요";
                m_ladder_text.enabled = true;
            }
            else
            {
                m_ladder_text.enabled = false; // m_ladder_text2가 활성화된 경우 m_ladder_text를 비활성화
            }
        }
        else
        {
            m_ladder_text.enabled = false;
        }

        

        // 문 근처에 있을 때 플레이어에게 상호작용 가능함을 보여주는 효과 등을 추가
        if (is_near_vent)
        {
            m_vent_text.text = "환풍구를 이용하려면 E를 누르세요.";
            m_vent_text.enabled = true;
        }
        else
            m_vent_text.enabled = false;

        if (is_near_hide)
        {
            m_hide_text.text = "숨기를 이용하려면 E를 누르세요.";
            m_hide_text.enabled = true;
        }
        else
            m_hide_text.enabled = false;
    }

    IEnumerator DisableTextAfterTime(float time, TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(time);
        text.enabled = false;
    }
    IEnumerator FadeOutAndMove(Vector3 destination)
    {
        is_near_vent = false;
        isFading = true;

        Color startColor = new Color(0f, 0f, 0f, 0f); // 투명한 컬러 (페이드 인)
        Color endColor = new Color(0f, 0f, 0f, 1f); // 블랙 컬러 (페이드 아웃)

        float elapsedTime = 0f;
        float fadeDuration = 1.0f; // 페이드 아웃 지속 시간

        while (elapsedTime < fadeDuration)
        {
            m_screen_fade_image.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        m_screen_fade_image.color = endColor;

        // 여기서 원하는 기능 수행 (좌표 이동 등)
        characterController.enabled = false;
        characterController.transform.position = destination;
        characterController.enabled = true;

        Debug.Log("이동");

        // 화면 밝게 하기 (페이드 인 시작)
        StartCoroutine(FadeIn(destination));
        isFading = false;
        CharacterMove.is_interacting_vent = false;

    }

    



    IEnumerator FadeIn(Vector3 destination)
    {
        Color startColor = new Color(0f, 0f, 0f, 1f); // 블랙 컬러 (페이드 아웃)
        Color endColor = new Color(0f, 0f, 0f, 0f); // 투명한 컬러 (페이드 인)

        float elapsedTime = 0f;
        float fadeDuration = 1.0f; // 페이드 인 지속 시간

        while (elapsedTime < fadeDuration)
        {
            m_screen_fade_image.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        m_screen_fade_image.color = endColor;
    }
}