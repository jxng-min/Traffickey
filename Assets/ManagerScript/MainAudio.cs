using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudio : MonoBehaviour
{
    public AudioSource m_main_audio_source;
    public AudioClip m_main_audio_clip;
    void Start()
    {
        m_main_audio_source = GetComponent<AudioSource>();
        m_main_audio_source.clip = m_main_audio_clip;
        m_main_audio_source.loop = true;
        m_main_audio_source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
