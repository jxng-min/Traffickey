using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
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
        StartCoroutine(PlayAudioAndLoadScene("GameLoadingT"));
    }

    private IEnumerator PlayAudioAndLoadScene(string sceneName)
    {
        if (!isAudioPlaying)
        {
            isAudioPlaying = true;
            m_audio_source.Play();
            yield return new WaitForSeconds(m_audio_clip.length);
            SceneManager.LoadScene(sceneName);
        }
    }
}
