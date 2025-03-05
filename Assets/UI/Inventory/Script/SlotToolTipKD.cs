using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTipKD : MonoBehaviour
{

    // 필요한 컴포넌트 들
    [SerializeField]
    private GameObject m_go_base;

    [SerializeField]
    private TextMeshProUGUI m_txt_item_name;

    [SerializeField]
    private TextMeshProUGUI m_txt_item_how_to_used;

    public void ShowToolTip(ItemKD _item)
    {
        m_go_base.SetActive(true);

        m_txt_item_name.text = _item.m_item_name;
        if (_item.m_item_name == "Flashlight")
            m_txt_item_name.text = "손전등";
        else if (_item.m_tool_type == "KEY")
            m_txt_item_name.text = "열쇠";
        else if (_item.m_tool_type == "DIARY")
            m_txt_item_name.text = "일기";


        if (_item.m_item_type == ItemKD.ITEMTYPE.EQUIPMENT)
            m_txt_item_how_to_used.text = "장착 : 우클릭";
        else if(_item.m_item_type == ItemKD.ITEMTYPE.USED)
            m_txt_item_how_to_used.text = "읽기 : 우클릭";
        else
            m_txt_item_how_to_used.text = "";


    }

    public void HideToolTip()
    {
        m_go_base.SetActive(false);
    }
}
