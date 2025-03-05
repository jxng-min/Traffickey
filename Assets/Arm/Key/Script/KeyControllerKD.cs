using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyControllerKD : MonoBehaviour
{
    // 현재 장착된 Hand형 타입 도구
    [SerializeField]
    private KeyKD m_current_key;

    // 사용 중?
    private bool m_is_use = false;
    private bool m_is_swing = false;

    private RaycastHit m_hit_info;

    [SerializeField]
    private TextMeshProUGUI m_key_text;

    [SerializeField]
    private ToolManagerKD m_tool_manager;

    //private bool m_fading = false;          // 페이드 여부
    private float m_fade_duration = 1f;     // 페이드 지속 시간

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
        //애니메이션이나 도구 사용 모션 비슷한걸 작성

        yield return new WaitForSeconds(m_current_key.m_use_delay_a);
        m_is_swing = true;

        // 도구 사용 활성화 시점
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
                // 충돌했음
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

        // 텍스트 서서히 나타나기
        while (m_text.color.a < 1f)
        {
            float newAlpha = m_text.color.a + fadeSpeed * Time.deltaTime;
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, newAlpha);
            yield return null;
        }

        yield return new WaitForSeconds(m_fade_duration);

        // 텍스트 서서히 사라지기
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
                m_key_text.text = "이미 사용했던 열쇠다.";
            m_key_text.text = "열쇠가 맞지 않는다.";
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
