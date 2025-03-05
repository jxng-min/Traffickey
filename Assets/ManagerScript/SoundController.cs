using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource m_enemy_sound1;
    public AudioSource m_enemy_sound2;
    public AudioSource m_player_sound;
    public AudioSource m_object_sound;
    public AudioSource m_menu_select_sound;
    public AudioSource m_main_sound;

    public void SetEnemyVolume(float volume)
    {
        m_enemy_sound1.volume = volume;
        m_enemy_sound2.volume = volume;
    }

    public void SetPlayerVolume(float volume)
    {
        m_player_sound.volume = volume;
    }

    public void SetObjectVolume(float volume)
    {
        m_object_sound.volume = volume;
        m_menu_select_sound.volume = volume;
    }

    public void SetMainVolume(float volume)
    {
        m_main_sound.volume = volume;
    }

    public void StopAudio()
    {
        m_enemy_sound1.Stop();
        m_enemy_sound2.Stop();
        m_player_sound.Stop();
        m_object_sound.Stop();
        m_main_sound.Stop();
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
