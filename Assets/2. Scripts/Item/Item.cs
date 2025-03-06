using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Create Item")]
public class Item : ScriptableObject
{
    [Header("아이템의 고유 ID")]
    [SerializeField] private int m_item_id;
    public int ID
    {
        get { return m_item_id; }
    }

    [Header("아이템의 중첩 유무")]
    [SerializeField] private bool m_can_overlap;
    public bool Overlap
    {
        get { return m_can_overlap; }
    }

    [Header("사용 아이템의 유무")]
    [SerializeField] private bool m_is_interactivity;
    public bool Interactivity
    {
        get { return m_is_interactivity; }
    }

    [Header("소비 아이템의 유무")]
    [SerializeField] private bool m_is_consumable;
    public bool Consumable
    {
        get { return m_is_consumable; }
    }

    [Header("아이템의 쿨타임")]
    [SerializeField] private float m_item_cool_time = -1f;
    public float CoolTime
    {
        get { return m_item_cool_time; }
    }

    [Header("아이템의 타입")]
    [SerializeField] private ItemType m_item_type;
    public ItemType Type
    {
        get { return m_item_type; }
    }

    [Header("아이템의 이미지")]
    [SerializeField] private Sprite m_item_image;
    public Sprite Image
    {
        get { return m_item_image; }
    }
}
