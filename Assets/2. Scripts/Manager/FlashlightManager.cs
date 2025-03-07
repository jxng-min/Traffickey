using UnityEngine;

public class FlashlightManager : Singleton<FlashlightManager>
{
    private static bool m_is_active = false;
    public static bool IsActive
    {
        get { return m_is_active; }
        private set { m_is_active = value; }
    }

    [Header("카메라 광원")]
    [SerializeField] private Light m_flashlight_light;

    public void ToggleFlashlight()
    {
        if(!IsActive)
        {
            m_flashlight_light.enabled = true;
            IsActive = true;
        }
        else
        {
            m_flashlight_light.enabled = false;
            IsActive = false;
        }
    }
}
