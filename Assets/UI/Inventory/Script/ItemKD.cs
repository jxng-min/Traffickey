using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class ItemKD : ScriptableObject
{
    public enum ITEMTYPE
    {
        EQUIPMENT,
        USED
    }

    public ITEMTYPE m_item_type;        // 아이템의 유형
    public string m_item_name;          // 아이템의 이름
    public Sprite m_item_image;         // 아이템의 이미지
    public GameObject m_item_prefab;    // 아이템의 프리펩

    public string m_tool_type;          // 도구 유형
}
