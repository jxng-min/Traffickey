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

    public ITEMTYPE m_item_type;        // �������� ����
    public string m_item_name;          // �������� �̸�
    public Sprite m_item_image;         // �������� �̹���
    public GameObject m_item_prefab;    // �������� ������

    public string m_tool_type;          // ���� ����
}
