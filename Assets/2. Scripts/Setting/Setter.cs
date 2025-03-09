using UnityEngine;
using UnityEngine.UI;

public class Setter : MonoBehaviour
{
    private bool m_is_ui_active = false;
    public bool IsActive
    {
        get { return m_is_ui_active; }
        private set { m_is_ui_active = value; }
    }

    #region 소리
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
    [Header("카메라 흔들림 제어 토글")]
    [SerializeField] Toggle m_camera_shaking_toggle;
    
    [Header("카메라 라이트 제어 토글")]
    [SerializeField] Toggle m_camera_light_toggle;
    #endregion

    [Header("설정 UI 오브젝트")]
    [SerializeField] private GameObject m_setting_ui_object;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!IsActive)
            {
                IsActive = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                IsActive = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;               
            }
            m_setting_ui_object.SetActive(IsActive);
        }   
    }
}
