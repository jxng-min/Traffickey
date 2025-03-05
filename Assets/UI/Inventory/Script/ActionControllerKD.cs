using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionControllerKD : MonoBehaviour
{
    [SerializeField]
    private float m_get_range;              // ���� ������ �ִ� �Ÿ�

    private bool m_can_get = false;         // ���� ���� ����
    private RaycastHit m_hit_info;          // �浹ü ���� ����

    [SerializeField]
    private AudioSource m_pick_up_audio_source;

    [SerializeField]
    private AudioClip m_pick_up_audio_clip;

    // ������ ���̾�� �����ϵ��� ���̾� ����ũ�� ����
    [SerializeField]
    private LayerMask m_layer_mask;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private TextMeshProUGUI m_action_text;

    [SerializeField]
    private InventoryKD m_the_inventory;

    [SerializeField]
    private TextMeshProUGUI m_flashlight_text;

    public static bool is_pickup_first_flashlight = true;

    [SerializeField]
    private TextMeshProUGUI m_key_text;
    public bool is_pickup_first_key = true;

    [SerializeField]
    private TextMeshProUGUI m_diary_text;
    public bool is_pickup_first_diary = true;

    //private bool m_fading = false;          // ���̵� ����
    private float m_fade_duration = 3f;     // ���̵� ���� �ð�



    private void Start()
    {
        m_pick_up_audio_source = GetComponent<AudioSource>();
        m_flashlight_text.color = new Color(m_flashlight_text.color.r, m_flashlight_text.color.g, m_flashlight_text.color.b, 0f);
        m_key_text.color = new Color(m_key_text.color.r, m_key_text.color.g, m_key_text.color.b, 0f);
        m_diary_text.color = new Color(m_key_text.color.r, m_key_text.color.g, m_key_text.color.b, 0f);
    }

    void Update()
    {
        CheckItem();
        TryAction();
        
    }

    private void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if(m_can_get)
        {
            if(m_hit_info.transform != null)
            {
                m_the_inventory.AcquireItem(m_hit_info.transform.GetComponent<ItemPickUpKD>().item);

                if (m_hit_info.transform.GetComponent<ItemPickUpKD>().item.m_item_name == "Flashlight")
                    TutorialFlashLight();
                else if (m_hit_info.transform.GetComponent<ItemPickUpKD>().item.m_tool_type == "KEY")
                {
                    KeyCounter.m_key_counter++;
                    if (is_pickup_first_key)
                        TutorialKey();
                }
                else if (m_hit_info.transform.GetComponent<ItemPickUpKD>().item.m_tool_type == "DIARY")
                {
                    if (is_pickup_first_diary)
                        TutorialDiary();
                }
                AudioPlay();
                Destroy(m_hit_info.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void TutorialFlashLight()
    {
        is_pickup_first_flashlight = false;
        StartCoroutine(FadeText(m_flashlight_text));
    }

    private void TutorialKey()
    {
        is_pickup_first_key = false;
        StartCoroutine(FadeText(m_key_text));
    }

    private void TutorialDiary()
    {
        is_pickup_first_diary = false;
        StartCoroutine(FadeText(m_diary_text));
    }

    private IEnumerator FadeText(TextMeshProUGUI m_text)
    {
        //m_fading = true;
        float fadeSpeed = 1f / m_fade_duration;

        // �ؽ�Ʈ ������ ��Ÿ����
        while (m_text.color.a < 1f)
        {
            float newAlpha = m_text.color.a + fadeSpeed * Time.deltaTime;
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, newAlpha);
            yield return null;
        }

        yield return new WaitForSeconds(m_fade_duration);

        // �ؽ�Ʈ ������ �������
        while (m_text.color.a > 0f)
        {
            float newAlpha = m_text.color.a - fadeSpeed * Time.deltaTime;
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, newAlpha);
            yield return null;
        }

        //m_fading = false;
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out m_hit_info, m_get_range, m_layer_mask))
        {
            if (m_hit_info.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
            InfoDisappear();
    }

    private void ItemInfoAppear()
    {
        m_can_get = true;
        m_action_text.gameObject.SetActive(true);
        if(m_hit_info.transform.GetComponent<ItemPickUpKD>().item.m_item_name == "Flashlight")
            m_action_text.text = "������ ȹ�� (E)";
        else if(m_hit_info.transform.GetComponent<ItemPickUpKD>().item.m_tool_type == "KEY")
            m_action_text.text = "���� ȹ�� (E)";
        else if (m_hit_info.transform.GetComponent<ItemPickUpKD>().item.m_tool_type == "DIARY")
            m_action_text.text = "�ϱ� ȹ�� (E)";
    }

    private void InfoDisappear()
    {
        m_can_get = false;
        m_action_text.gameObject.SetActive(false);
    }

    private void AudioPlay()
    {
        m_pick_up_audio_source.clip = m_pick_up_audio_clip;
        m_pick_up_audio_source.Play();
    }
}
