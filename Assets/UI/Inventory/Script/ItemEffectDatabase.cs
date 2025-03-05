using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

[System.Serializable]
public class ItemEffect
{
    public string m_item_name;
}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ToolManagerKD m_the_tool_manager;

    [SerializeField]
    private SlotToolTipKD m_the_slot_tooltip;

    public void ShowToolTip(ItemKD _item)
    {
        m_the_slot_tooltip.ShowToolTip(_item);
    }

    public void HideToolTip()
    {
        m_the_slot_tooltip.HideToolTip();
    }

    public void UseItem(ItemKD _item)
    {
        if (_item.m_item_type == ItemKD.ITEMTYPE.EQUIPMENT)
        {
            StartCoroutine(m_the_tool_manager.ChangeToolCoroutine(_item.m_tool_type, _item.m_item_name));
        }
        else if (_item.m_item_type == ItemKD.ITEMTYPE.USED)
        {
            StartCoroutine(m_the_tool_manager.ChangeToolCoroutine(_item.m_tool_type, _item.m_item_name));
        }
    }
}
