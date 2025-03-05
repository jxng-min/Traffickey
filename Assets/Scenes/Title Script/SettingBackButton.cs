using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingBackButton : MonoBehaviour
{
    [SerializeField]
    private Button m_back_button;

    public AudioSource m_audio_source;
    public AudioClip m_audio_clip;
    private bool isAudioPlaying = false;

    private void Start()
    {
        m_audio_source = GetComponent<AudioSource>();
        m_audio_source.clip = m_audio_clip;
    }

    public void BackSceneLoad()
    {
        if (!isAudioPlaying)
        {
            m_audio_source.Play();
            isAudioPlaying = true;
        }
        transform.gameObject.SetActive(false);
    }
}
