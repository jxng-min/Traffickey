using UnityEngine;

public class FlashlightInteraction : MonoBehaviour
{
    private bool m_is_active = false;
    public bool IsActive
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
            SoundManager.Instance.PlayEffect("Flashlight On");

            m_flashlight_light.enabled = true;
            IsActive = true;
        }
        else
        {
            SoundManager.Instance.PlayEffect("Flashlight Off");

            m_flashlight_light.enabled = false;
            IsActive = false;
        }
    }
}
