using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotKD : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemKD m_item;               // ȹ���� ������
    public Image m_item_image;          // �������� �̹���

    private ItemEffectDatabase m_the_item_effect_database;


    private void Start()
    {
        m_the_item_effect_database = FindObjectOfType<ItemEffectDatabase>();
    }

    // �̹����� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = m_item_image.color;
        color.a = _alpha;
        m_item_image.color = color;
    }


    // ������ ȹ��
    public void AddItem(ItemKD _item)
    {
        m_item = _item;
        m_item_image.sprite = m_item.m_item_image;

        SetColor(1);
    }


    // ������ ���� ����
    public void SetSlotCount(int _count)
    {
        ClearSlot();
    }


    // ���� �ʱ�ȭ
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

    // ���콺�� ���Կ� �� �� �ߵ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(m_item != null)
            m_the_item_effect_database.ShowToolTip(m_item);
    }

    // ���콺�� ���Կ� �������� �� �ߵ�
    public void OnPointerExit(PointerEventData eventData)
    {
        m_the_item_effect_database.HideToolTip();
    }
}

