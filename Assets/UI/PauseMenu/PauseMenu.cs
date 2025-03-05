using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_go_base_ui;

    [SerializeField]
    private GameObject m_setting_base_ui;

    public AudioSource m_menu_select_audio;
    public AudioClip m_menu_select_clip;

    private void Start()
    {
        m_menu_select_audio = GetComponent<AudioSource>();
        m_menu_select_audio.clip = m_menu_select_clip;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.m_is_pause)
                CallMenu();
            else
                CloseMenu();
        }
        
    }

    private void CallMenu()
    {
        m_menu_select_audio.Play();
        GameManager.m_is_pause = true;
        m_go_base_ui.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void CloseMenu()
    {
        m_menu_select_audio.Play();
        GameManager.m_is_pause = false;
        m_setting_base_ui.SetActive(false);
        m_go_base_ui.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ClickResume()
    {
        m_menu_select_audio.Play();
        m_setting_base_ui.SetActive(false);
        CloseMenu();
    }

    public void ClickSetting()
    {
        m_menu_select_audio.Play();
        m_setting_base_ui.SetActive(true);
    }

    public void ClickMain()
    {
        m_menu_select_audio.Play();

        m_setting_base_ui.SetActive(false);
        SceneManager.LoadScene("GameLoadingT");
    }
}
