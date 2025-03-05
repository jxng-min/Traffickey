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

    public AudioClip door_open_sound; // �� ���� ����
    public AudioClip door_close_sound; // �� ���� ����
    public AudioClip vent_enter_sound; // ȯǳ�� ���� ����

    public bool is_near_hide = false;
    public TextMeshProUGUI m_hide_text;

    public TextMeshProUGUI m_ladder_text; // ��ٸ� ��ȣ�ۿ� �ؽ�Ʈ
    private bool is_near_ladder = false; // ��ٸ� ��ó ����    

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
                        // 'End' ������ ��ȯ
                        UnityEngine.SceneManagement.SceneManager.LoadScene("End");
                        GameManager.m_game_state = GameManager.GAMESTATE.GAMEOVER;
                        m_ladder_text2.enabled = false; // ���谡 ����� �� �޽��� ��Ȱ��ȭ
                    }
                    else
                    {
                        
                        m_ladder_text2.text = "���谡 �����մϴ�";
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
                    CharacterMove.isHiding = !CharacterMove.isHiding; // ���� ���¸� ���
                                                                      // �߰����� ����� ���� (ĳ���͸� ������ �ʰ� ����ų� ���)
                }
            }


            Debug.DrawRay(raycastOrigin, raycastDirection * hit.distance, Color.green);
        }
        else
        {
            is_near_vent = false;
            is_near_hide = false;
            is_near_ladder = false; // ��ٸ� ��ó�� �ƴ�
        }

        if (is_near_ladder)
        {
            if (!m_ladder_text2.enabled) // m_ladder_text2�� ��Ȱ��ȭ�� ��쿡�� m_ladder_text�� Ȱ��ȭ
            {
                m_ladder_text.text = "Ż�ⱸ�� �̿��Ϸ��� E�� ��������";
                m_ladder_text.enabled = true;
            }
            else
            {
                m_ladder_text.enabled = false; // m_ladder_text2�� Ȱ��ȭ�� ��� m_ladder_text�� ��Ȱ��ȭ
            }
        }
        else
        {
            m_ladder_text.enabled = false;
        }

        

        // �� ��ó�� ���� �� �÷��̾�� ��ȣ�ۿ� �������� �����ִ� ȿ�� ���� �߰�
        if (is_near_vent)
        {
            m_vent_text.text = "ȯǳ���� �̿��Ϸ��� E�� ��������.";
            m_vent_text.enabled = true;
        }
        else
            m_vent_text.enabled = false;

        if (is_near_hide)
        {
            m_hide_text.text = "���⸦ �̿��Ϸ��� E�� ��������.";
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

        Color startColor = new Color(0f, 0f, 0f, 0f); // ������ �÷� (���̵� ��)
        Color endColor = new Color(0f, 0f, 0f, 1f); // �� �÷� (���̵� �ƿ�)

        float elapsedTime = 0f;
        float fadeDuration = 1.0f; // ���̵� �ƿ� ���� �ð�

        while (elapsedTime < fadeDuration)
        {
            m_screen_fade_image.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        m_screen_fade_image.color = endColor;

        // ���⼭ ���ϴ� ��� ���� (��ǥ �̵� ��)
        characterController.enabled = false;
        characterController.transform.position = destination;
        characterController.enabled = true;

        Debug.Log("�̵�");

        // ȭ�� ��� �ϱ� (���̵� �� ����)
        StartCoroutine(FadeIn(destination));
        isFading = false;
        CharacterMove.is_interacting_vent = false;

    }

    



    IEnumerator FadeIn(Vector3 destination)
    {
        Color startColor = new Color(0f, 0f, 0f, 1f); // �� �÷� (���̵� �ƿ�)
        Color endColor = new Color(0f, 0f, 0f, 0f); // ������ �÷� (���̵� ��)

        float elapsedTime = 0f;
        float fadeDuration = 1.0f; // ���̵� �� ���� �ð�

        while (elapsedTime < fadeDuration)
        {
            m_screen_fade_image.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        m_screen_fade_image.color = endColor;
    }
}