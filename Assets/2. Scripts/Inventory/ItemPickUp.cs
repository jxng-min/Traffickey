using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [Header("할당되는 아이템 오브젝트")]
    [SerializeField] private Item m_item;
    public Item Item
    {
        get { return m_item; }
    }
}
