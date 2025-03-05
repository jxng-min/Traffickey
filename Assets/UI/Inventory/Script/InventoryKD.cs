using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryKD : MonoBehaviour
{
    public static bool m_is_inventory_active = false;

    [SerializeField]
    private ItemEffectDatabase m_the_item_effect_database;

    // ÇÊ¿äÇÑ ÄÄÆ÷³ÍÆ®
    [SerializeField]
    private GameObject m_go_inventory_base;

    [SerializeField]
    private GameObject m_go_slot_parent;

    // ½½·Ôµé
    private SlotKD[] slots;

    public SlotKD[] GetSlots()
    {
        return slots;
    }

    [SerializeField]
    private ItemKD[] m_items;

    [SerializeField]
    private DiaryControllerKD m_diary_controller;

    public void LoadToInven(int _arrayNum, string _itemName)











    {
        for(int i = 0; i < m_items.Length; i++)
        {
            if (m_items[i].m_item_name == _itemName)
            {
                slots[_arrayNum].AddItem(m_items[i]);
            }
        }
    }

    void Start()
    {
        m_the_item_effect_database.GetComponent<ItemEffectDatabase>();
        slots = m_go_slot_parent.GetComponentsInChildren<SlotKD>();
    }

    void Update()
    {
        //TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!m_diary_controller.m_is_use)
            {
                m_is_inventory_active = !m_is_inventory_active;

                if (m_is_inventory_active)
                {

                    OpenInventory();
                }
                else
                {
                    CloseInventory();
                }
            }
        }
    }

    private void OpenInventory()
    {
        GameManager.m_is_inventory_open = true;
        m_go_inventory_base.SetActive(true);
    }

    private void CloseInventory()
    {
        GameManager.m_is_inventory_open = false;
        m_the_item_effect_database.HideToolTip();
        m_go_inventory_base.SetActive(false);
    }

    public void AcquireItem(ItemKD _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //if (slots[i].m_item != null)
            //{
            if (slots[i].m_item == null)
            {
                slots[i].AddItem(_item);
                return;
            }
            //}
        }
    }
}