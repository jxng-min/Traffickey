using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSoundManager : MonoBehaviour
{
    public AudioSource m_menu_select_sound;
    public AudioSource m_main_sound;


    public void SetSelectVolume(float volume)
    {
        m_menu_select_sound.volume = volume;
    }

    public void SetMainVolume(float volume)
    {
        m_main_sound.volume = volume;
    }

    public void StopAudio()
    {
        m_menu_select_sound.Stop();
    }

    public void StartMain()
    {
        m_main_sound.Play();
    }

    public void StopMain()
    {
        m_main_sound.Stop();
    }
}
