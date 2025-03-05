using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyKD : MonoBehaviour
{
    public string m_key_name;
    public float m_range;
    public float m_use_delay;       // 공격 딜레이
    public float m_use_delay_a;     // 공격 활성화 시점
    public float m_use_delay_b;     // 공격 비활성화 시점
    public bool m_is_used = false;
}