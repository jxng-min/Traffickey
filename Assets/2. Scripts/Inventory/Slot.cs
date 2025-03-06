using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Item m_item;
    public Item Item
    {
        get { return m_item; }
        set { m_item = value; }
    }

    [Header("슬롯 타입 마스크")]
    [SerializeField] private ItemType m_slot_mask;
    public ItemType SlotMask
    {
        get { return m_slot_mask; }
        protected set { m_slot_mask = value; }
    }

    private int m_item_count;
    public int Count
    {
        get { return m_item_count; }
        protected set { m_item_count = value; }
    }

    [Space(50)]
    [Header("슬롯 UI 오브젝트 목록")]
    [SerializeField] private Image m_item_image;
    [SerializeField] private Image m_cool_time_image;
    [SerializeField] private TMP_Text m_count_label;

    private ItemActionManager m_item_action_manager;

    private void Awake()
    {
        m_item_action_manager = GameObject.Find("Item Action Manager").GetComponent<ItemActionManager>();
    }

    private void Update()
    {
        if(Item is not null)
        {
            if(ItemCoolManager.Instance.GetCurrentCool(Item.ID) == 0f)
            {
                m_cool_time_image.fillAmount = 0f;
            }
            else
            {
                m_cool_time_image.fillAmount = ItemCoolManager.Instance.GetCurrentCool(Item.ID) / Item.CoolTime;
            }
        }
        else
        {
            m_cool_time_image.fillAmount = 0f;
        }
    }

    private void SetColor(float alpha)
    {
        Color color = m_item_image.color;
        color.a = alpha;
        m_item_image.color = color;
    }

    public bool IsMask(Item item)
    {
        return ((int)item.Type & (int)m_slot_mask) == 0 ? false : true;
    }

    public void AddItem(Item item, int count = 1)
    {
        Item = item;
        Count = count;
        m_item_image.sprite = item.Image;

        if(Item.Type >= ItemType.Equipment)
        {
            m_count_label.text = "";
        }
        else
        {
            m_count_label.text = Count.ToString();
        }

        SetColor(1f);
    }

    public void UpdateSlotCount(int count)
    {
        m_item_count += count;
        m_count_label.text = m_item_count.ToString();

        if(m_item_count <= 0)
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        Item = null;
        m_item_count = 0;
        m_item_image.sprite = null;
        m_count_label.text = "";
        
        SetColor(0f);
    }

    public void UseItem()
    {
        if(Item is null)
        {
            return;
        }

        if(Item.Interactivity is false)
        {
            return;
        }

        if(ItemCoolManager.Instance.GetCurrentCool(Item.ID) > 0f)
        {
            return;
        }

        if(!m_item_action_manager.UseItem(Item, this))
        {
            return;
        }

        if(m_item.CoolTime > 0f)
        {
            ItemCoolManager.Instance.AddCoolQueue(Item.ID, Item.CoolTime);
        }

        if(Item is not null && Item.Consumable)
        {
            UpdateSlotCount(-1);
        }
    }
}
