using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundPlayer : MonoBehaviour
{
    public AudioSource m_audio_source;
    public AudioClip m_clip;
    // Start is called before the first frame update
    void Start()
    {
        m_audio_source = GetComponent<AudioSource>();
        m_audio_source.clip = m_clip;
        m_audio_source.loop = true; // ������� ���� �ݺ� ����ϵ��� ����
        m_audio_source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
