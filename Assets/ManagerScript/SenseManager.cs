using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseManager : MonoBehaviour
{
    public MouseMove m_mouse;
    
    public void SetMouseSenseVolume(float volume)
    {
        m_mouse.m_sensitivity = volume * 1000;
    }
}
