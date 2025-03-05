using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyCount : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Update()
    {
        text.text = "수집한 열쇠의 개수 : " + KeyCounter.m_key_counter.ToString();
    }
}
