using System.Collections.Generic;
using UnityEngine;

public class ItemCoolManager : Singleton<ItemCoolManager>
{
    private Dictionary<int, float> m_cool_dict;
    private List<int> m_cool_list;
    private float m_temp_cool;

    private void Start()
    {
        m_cool_dict = new Dictionary<int, float>();
        m_cool_list = new List<int>();   
    }

    private void Update()
    {
        for(int i = m_cool_list.Count - 1; i >= 0; i--)
        {
            m_temp_cool = m_cool_dict[m_cool_list[i]] -= Time.deltaTime;

            if(m_temp_cool < 0f)
            {
                m_cool_list.RemoveAt(i);
            }
        }
    }

    public void AddCoolQueue(int item_id, float origin_cool)
    {
        m_cool_dict.TryAdd(item_id, origin_cool);
        m_cool_dict[item_id] = origin_cool;

        m_cool_list.Add(item_id);
    }

    public float GetCurrentCool(int item_id)
    {
        float cool;
        bool is_success = m_cool_dict.TryGetValue(item_id, out cool);

        if(is_success)
        {
            return cool;
        }
        else
        {
            return 0;
        }
    }
}
