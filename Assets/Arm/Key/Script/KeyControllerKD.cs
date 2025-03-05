using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyControllerKD : MonoBehaviour
{
    // ���� ������ Hand�� Ÿ�� ����
    [SerializeField]
    private KeyKD m_current_key;

    // ��� ��?
    private bool m_is_use = false;
    private bool m_is_swing = false;

    private RaycastHit m_hit_info;

    [SerializeField]
    private TextMeshProUGUI m_key_text;

    [SerializeField]
    private ToolManagerKD m_tool_manager;

    //private bool m_fading = false;          // ���̵� ����
    private float m_fade_duration = 1f;     // ���̵� ���� �ð�

    private static int m_key_count = 0;

    private void Start()
    {
        ToolManagerKD.m_current_tool = m_current_key.GetComponent<Transform>();
        m_key_text.color = new Color(m_key_text.color.r, m_key_text.color.g, m_key_text.color.b, 0f);
    }

    void Update()
    {
        TryUse();
    }

    private void TryUse()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!m_is_use)
            {
                StartCoroutine(UseCoroutine());
            }
        }
    }

    IEnumerator UseCoroutine()
    {
        m_is_use = true;
        //�ִϸ��̼��̳� ���� ��� ��� ����Ѱ� �ۼ�

        yield return new WaitForSeconds(m_current_key.m_use_delay_a);
        m_is_swing = true;

        // ���� ��� Ȱ��ȭ ����
        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(m_current_key.m_use_delay_b);
        m_is_swing = false;

        yield return new WaitForSeconds(m_current_key.m_use_delay - m_current_key.m_use_delay_a - m_current_key.m_use_delay_b);
        m_is_use = false;
    }

    IEnumerator HitCoroutine()
    {
        while (m_is_swing)
        {
            if (CheckObject())
            {
                m_is_swing = false;
                if (m_hit_info.transform.name == "Door")
                {
                    SetKeyText();
                    if (!m_current_key.m_is_used)
                    {
                        IncreaseKeyCount();
                        m_current_key.m_is_used = true;
                    }
                    StartCoroutine(FadeText(m_key_text));
                    GameManager.m_game_state = GameManager.GAMESTATE.GAMECLEAR;
                }
                // �浹����
                Debug.Log(m_hit_info.transform.name);
            }
            yield return null;
        }
    }

    private bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out m_hit_info, m_current_key.m_range))
        {
            return true;
        }
        return false;
    }

    public void KeyChange(KeyKD _key)
    {
        if (ToolManagerKD.m_current_tool != null)
        {
            ToolManagerKD.m_current_tool.gameObject.SetActive(false);
        }

        m_current_key = _key;
        ToolManagerKD.m_current_tool = m_current_key.GetComponent<Transform>();

        // m_current_flashlight.transform.localPosition = Vector3.zero;
        m_current_key.gameObject.SetActive(true);
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

    private void SetKeyText()
    {
        if (m_tool_manager.m_current_tool_type == "KEY")
        {
            if (m_current_key.m_is_used)
                m_key_text.text = "�̹� ����ߴ� �����.";
            m_key_text.text = "���谡 ���� �ʴ´�.";
        }
        else
            m_key_text.text = "";


    }

    

    private void IncreaseKeyCount()
    {
        if (m_tool_manager.m_current_tool_type == "KEY")
            m_key_count++;
    }
}
