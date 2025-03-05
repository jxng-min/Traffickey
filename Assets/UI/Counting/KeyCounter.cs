using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyCounter : MonoBehaviour
{
    public static int m_key_counter = 0;
    public TextMeshProUGUI m_key_count_text;

    void Start()
    {

    }

    void Update()
    {
        UpdateKeyCountText();        
    }

    public void UpdateKeyCountText()
    {
        if (m_key_count_text != null)
            m_key_count_text.text = m_key_counter.ToString() + " / 4";
    }

    public static bool HasAllKeys()
    {
        return m_key_counter >= 4;
    }
}
