using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FlashControllerKD : MonoBehaviour
{
    [SerializeField]
    private FlashLightKD m_current_flashlight;

    public AudioSource m_audio_source;
    public bool m_is_flashlight_on = false;
    public Light m_flashlight;
    public AudioClip m_flashlight_on_clip;
    public AudioClip m_flashlight_off_clip;
    public bool m_can_toggle = false;
    public void Start()
    {
        m_flashlight = GetComponent<Light>();
        m_audio_source = GetComponent<AudioSource>();
        ToolManagerKD.m_current_tool = m_current_flashlight.GetComponent<Transform>();
    }

    private void Update()
    {
        if(m_can_toggle)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleFlashlight();
            }
        }
    }

    private void ToggleFlashlight()
    {
    m_is_flashlight_on = !m_is_flashlight_on;

        if (m_is_flashlight_on)
        {
            m_flashlight.enabled = true;
            m_audio_source.clip = m_flashlight_on_clip;
        }
        else
        {
            m_flashlight.enabled = false;
            m_audio_source.clip = m_flashlight_off_clip;
        }
        m_audio_source.Play();
    }

    public void FlashlightOff()
    {
        m_flashlight.enabled = false;
        m_is_flashlight_on = false;
    }

    public void FlashlightChange(FlashLightKD _flahslight)
    {
        if (ToolManagerKD.m_current_tool != null)
        {
            ToolManagerKD.m_current_tool.gameObject.SetActive(false);
        }

        m_current_flashlight = _flahslight;
        ToolManagerKD.m_current_tool = m_current_flashlight.GetComponent<Transform>();

        m_current_flashlight.transform.localPosition = Vector3.zero;
        m_current_flashlight.gameObject.SetActive(true);
    }
}
