using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ItemInfo
{
    public int m_item_id;
    public string m_item_name;
    public string m_item_description;
}

[System.Serializable]
public class ItemInfos
{
    public ItemInfo[] m_item_infos;
}

public class ItemDataManager : Singleton<ItemDataManager>
{
    private string m_item_data_path;
    private Dictionary<int, string> m_item_name_dict;
    private Dictionary<int, string> m_item_description_dict;

    [SerializeField] private Item[] m_item_object_data;
    public Item[] ItemObject
    {
        get { return m_item_object_data; }
    }

    private new void Awake()
    {
        base.Awake();

        m_item_data_path = Path.Combine(Application.persistentDataPath, "ItemData.json");

        m_item_name_dict = new Dictionary<int, string>();
        m_item_description_dict = new Dictionary<int, string>();

        LoadData();
    }

    private void LoadData()
    {
        if(File.Exists(m_item_data_path))
        {
            var json_data = File.ReadAllText(m_item_data_path);
            var item_infos = JsonUtility.FromJson<ItemInfos>(json_data);

            foreach(var info in item_infos.m_item_infos)
            {
                m_item_name_dict.Add(info.m_item_id, info.m_item_name);
                m_item_description_dict.Add(info.m_item_id, info.m_item_description);
            }
        }
    }

    public string GetName(int item_id)
    {
        return m_item_name_dict.ContainsKey(item_id) ? m_item_name_dict[item_id] : null;
    }

    public string GetDescription(int item_id)
    {
        return m_item_description_dict.ContainsKey(item_id) ? m_item_description_dict[item_id] : null;
    }
}
