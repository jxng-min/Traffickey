using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolManagerKD : MonoBehaviour
{
    // 도구 중복 교체 실행 방지
    public static bool m_is_change_tool = false;

    // 도구 교체 딜레이
    [SerializeField]
    private float m_change_tool_delay_time;

    // 도구 교체가 완전히 끝난 시점
    [SerializeField]
    private float m_change_tool_end_delay_time;

    // 무기 종류들 전부 관리
    [SerializeField]
    private FlashLightKD[] m_flashlight;
    [SerializeField]
    private HandKD[] m_hand;
    [SerializeField]
    private KeyKD[] m_key;
    [SerializeField]
    private DiaryKD[] m_diary;

    [SerializeField]
    private DiaryTextController m_diary_text_ctrl;

    // 관리 차원에서 쉽게 무기 접근이 가능하도록 함
    private Dictionary<string, FlashLightKD> m_flash_dic = new Dictionary<string, FlashLightKD>();
    private Dictionary<string, HandKD> m_hand_dic = new Dictionary<string, HandKD>();
    private Dictionary<string, KeyKD> m_key_dic = new Dictionary<string, KeyKD>();
    private Dictionary<string, DiaryKD> m_diary_dic = new Dictionary<string, DiaryKD>();

    // 필요한 컴포넌트
    [SerializeField]
    private FlashControllerKD m_the_flash_ctrl;
    [SerializeField]
    private HandControllerKD m_the_hand_ctrl;
    [SerializeField]
    private KeyControllerKD m_the_key_ctrl;
    [SerializeField]
    private DiaryControllerKD m_the_diary_ctrl;

    // 현재 무기의 타입
    [SerializeField]
    public string m_current_tool_type;

    //현재 도구
    public static Transform m_current_tool;
    public static Animator m_current_tool_anim;

    void Start()
    {
        for (int i = 0; i < m_flashlight.Length; i++)
        {
            m_flash_dic.Add(m_flashlight[i].m_flashlight_name, m_flashlight[i]);
        }
        for (int i = 0; i < m_hand.Length; i++)
        {
            m_hand_dic.Add(m_hand[i].m_hand_name, m_hand[i]);
        }
        for (int i = 0; i < m_key.Length; i++)
        {
            m_key_dic.Add(m_key[i].m_key_name, m_key[i]);
        }
        for (int i = 0; i < m_diary.Length; i++)
        {
            m_diary_dic.Add(m_diary[i].m_diary_name, m_diary[i]);
        }

        // 게임 시작 시 핸드 도구를 자동으로 선택
        StartCoroutine(ChangeToolCoroutine("HAND", "Hand"));
    }


    void Update()
    {
        if (!m_is_change_tool)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartCoroutine(ChangeToolCoroutine("HAND", "Hand"));
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                if (ActionControllerKD.is_pickup_first_flashlight == false)
                    StartCoroutine(ChangeToolCoroutine("FLASHLIGHT", "Flashlight"));
        }

        if (!GameManager.m_is_pause && m_current_tool_type == "FLASHLIGHT" && m_current_tool != null)
            m_the_flash_ctrl.m_can_toggle = true;
        else
            m_the_flash_ctrl.m_can_toggle = false;

        if (!GameManager.m_is_pause && m_current_tool_type == "DIARY" && m_current_tool != null)
            DiaryControllerKD.m_can_toggle = true;
        else
            DiaryControllerKD.m_can_toggle = false;
    }

    public IEnumerator ChangeToolCoroutine(string _type, string _name)
    {
        m_is_change_tool = true;

        // 도구 교체 애니메이션
        
        //m_current_tool_anim.SetTrigger("ChangeFL");

        yield return new WaitForSeconds(m_change_tool_delay_time);

        CancelPreToolAction();
        ToolChange(_type, _name);

        yield return new WaitForSeconds(m_change_tool_end_delay_time);

        m_current_tool_type = _type;
        m_is_change_tool = false;

    }

    private void CancelPreToolAction()
    {
        switch (m_current_tool_type)
        {
            case "FLASHLIGHT":
                m_the_flash_ctrl.FlashlightOff();
                break;
            case "HAND":
                break;
            case "KEY":
                break;
            case "DIARY":
                break;
        }
    }

    private void ToolChange(string _type, string _name)
    {
        if (_type == "FLASHLIGHT")
            m_the_flash_ctrl.FlashlightChange(m_flash_dic[_name]);
        else if (_type == "HAND")
            m_the_hand_ctrl.HandChange(m_hand_dic[_name]);
        else if (_type == "KEY")
            m_the_key_ctrl.KeyChange(m_key_dic[_name]);
        else if (_type == "DIARY")
        {
            m_the_diary_ctrl.DiaryChange(m_diary_dic[_name]);
            m_diary_text_ctrl.diaryText.text = m_diary_dic[_name].m_diary_text.Replace("\\n", "\n");
        }
    }
}