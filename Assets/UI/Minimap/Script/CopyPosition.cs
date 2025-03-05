using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField]
    private bool m_xpos, m_ypos, m_zpos;

    [SerializeField]
    private Transform m_target;

    void Update()
    {
        if (!m_target) 
            return;

        transform.position = new Vector3(
            (m_xpos ? m_target.position.x : transform.position.x),
            (m_ypos ? m_target.position.y : transform.position.y),
            (m_zpos ? m_target.position.z : transform.position.z));
    }
}
