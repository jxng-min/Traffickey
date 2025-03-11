using UnityEngine;
using UnityEngine.UI;

public class Setter : MonoBehaviour
{
    [Header("UI 오브젝트의 애니메이터")]
    [SerializeField] private Animator m_animator;

    [Space(30)]
    #region 소리
    [Header("사운드 패널 토글")]
    [SerializeField] private Toggle m_sound_toggle;

    [Header("사운드 패널 UI 오브젝트")]
    [SerializeField] private GameObject m_sound_ui_object;

    [Header("배경음 출력 토글")]
    [SerializeField] private Toggle m_background_toggle;

    [Header("배경음 제어 슬라이더")]
    [SerializeField] private Slider m_background_slider;

    [Header("효과음 출력 토글")]
    [SerializeField] private Toggle m_effect_toggle;

    [Header("효과음 제어 슬라이더")]
    [SerializeField] private Slider m_effect_slider;
    #endregion

    [Space(30)]
    #region 게임
    [Header("게임 패널 토글")]
    [SerializeField] private Toggle m_game_toggle;

    [Header("게임 패널 UI 오브젝트")]
    [SerializeField] private GameObject m_game_ui_object;

    [Header("카메라 흔들림 제어 토글")]
    [SerializeField] Toggle m_camera_shaking_toggle;
    
    [Header("카메라 라이트 제어 토글")]
    [SerializeField] Toggle m_camera_light_toggle;
    #endregion

    [Header("설정 UI 오브젝트")]
    [SerializeField] private GameObject m_setting_ui_object;

    private void Awake()
    {
        m_background_toggle.isOn = SettingManager.Instance.Setting.m_sound_setting.m_background_is_on;
        m_background_slider.value = SettingManager.Instance.Setting.m_sound_setting.m_background_value;

        m_effect_toggle.isOn = SettingManager.Instance.Setting.m_sound_setting.m_effect_is_on;
        m_effect_slider.value = SettingManager.Instance.Setting.m_sound_setting.m_effect_value;

        m_camera_shaking_toggle.isOn = SettingManager.Instance.Setting.m_game_setting.m_camera_shaking_is_on;
        m_camera_light_toggle.isOn = SettingManager.Instance.Setting.m_game_setting.m_camera_light_is_on;   
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.Instance.Current == GameEventType.Playing)
            {
                GameEventBus.Publish(GameEventType.Setting);

                m_animator.SetBool("IsOpen", true);
            }
            else if(GameManager.Instance.Current == GameEventType.Setting)
            {
                GameEventBus.Publish(GameEventType.Playing);
                
                SoundManager.Instance.PlayEffect("Button Click");

                m_animator.SetBool("IsOpen", false);
            }
            
        }   
    }

    public void Toggle_BackgroundOn()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        SettingManager.Instance.Setting.m_sound_setting.m_background_is_on = m_background_toggle.isOn;
    }

    public void Slider_Background()
    {
        SettingManager.Instance.Setting.m_sound_setting.m_background_value = m_background_slider.value;
    }

    public void Toggle_EffectOn()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        SettingManager.Instance.Setting.m_sound_setting.m_effect_is_on = m_effect_toggle.isOn;
    }

    public void Slider_Effect()
    {
        SettingManager.Instance.Setting.m_sound_setting.m_effect_value = m_effect_slider.value;
    }

    public void Toggle_CameraShaking()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        SettingManager.Instance.Setting.m_game_setting.m_camera_shaking_is_on = m_camera_shaking_toggle.isOn;
    }

    public void Toggle_CameraLight()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        SettingManager.Instance.Setting.m_game_setting.m_camera_light_is_on = m_camera_light_toggle.isOn;
    }

    public void BTN_Title()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        SettingManager.Instance.SaveData();
        LoadingManager.Instance.LoadScene("Title");
    }

    public void BTN_Exit()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        SettingManager.Instance.SaveData();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Toggle_SoundPanel()
    {
        if(m_sound_toggle.isOn)
        {
            SoundManager.Instance.PlayEffect("Button Click");
            m_sound_ui_object.SetActive(true);
            m_game_toggle.isOn = false;
            m_game_ui_object.SetActive(false);
        }
    }

    public void Toggle_GamePanel()
    {
        if(m_game_toggle.isOn)
        {
            SoundManager.Instance.PlayEffect("Button Click");
            m_game_ui_object.SetActive(true);
            m_sound_toggle.isOn = false;
            m_sound_ui_object.SetActive(false);
        }
    }
}
