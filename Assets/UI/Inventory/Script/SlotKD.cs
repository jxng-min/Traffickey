using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotKD : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemKD m_item;               // 획득한 아이템
    public Image m_item_image;          // 아이템의 이미지

    private ItemEffectDatabase m_the_item_effect_database;


    private void Start()
    {
        m_the_item_effect_database = FindObjectOfType<ItemEffectDatabase>();
    }

    // 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = m_item_image.color;
        color.a = _alpha;
        m_item_image.color = color;
    }


    // 아이템 획득
    public void AddItem(ItemKD _item)
    {
        m_item = _item;
        m_item_image.sprite = m_item.m_item_image;

        SetColor(1);
    }


    // 아이템 개수 조정
    public void SetSlotCount(int _count)
    {
        ClearSlot();
    }


    // 슬롯 초기화
    public void ClearSlot()
    {
        m_item = null;
        m_item_image.sprite = null;
        SetColor(0);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (m_item != null)
            {
                if(m_item.m_item_type == ItemKD.ITEMTYPE.EQUIPMENT)
                    m_the_item_effect_database.UseItem(m_item);
                else if(m_item.m_item_type == ItemKD.ITEMTYPE.USED)
                {

                }
            }
        }
    }

    // 마우스가 슬롯에 들어갈 때 발동
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(m_item != null)
            m_the_item_effect_database.ShowToolTip(m_item);
    }

    // 마우스가 슬롯에 빠져나갈 때 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        m_the_item_effect_database.HideToolTip();
    }
}

