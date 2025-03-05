using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public AudioSource m_audio_source;
    public AudioClip m_audio_clip;
    public string m_scene_name = "GameStage";
    public GameObject m_setting_object;

    private bool isAudioPlaying = false; // 오디오 재생 여부를 저장하는 변수 추가

    private void Start()
    {
        m_audio_source = GetComponent<AudioSource>();
        m_audio_source.clip = m_audio_clip;
    }

    public void ClickStart()
    {
        StartCoroutine(PlayAudioAndLoadScene("GameLoading"));
    }



    public void ClickInfo()
    {
        StartCoroutine(PlayAudioAndLoadScene("GameLoadingC"));
    }

    public void ClickSetting()
    {
        m_audio_source.Play();
        m_setting_object.gameObject.SetActive(true);
    }

    public void ClickExit()
    {
        StartCoroutine(PlayAudioAndQuit());
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

    private IEnumerator PlayAudioAndQuit()
    {
        if (!isAudioPlaying)
        {
            isAudioPlaying = true;
            m_audio_source.Play();
            yield return new WaitForSeconds(m_audio_clip.length);
            Application.Quit();
        }
    }
}