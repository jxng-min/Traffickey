using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteraction : MonoBehaviour
{
    [Header("카메라 플래시 UI")]
    [SerializeField] private Image m_flash_image;

    public void Use()
    {
        SoundManager.Instance.PlayEffect("Camera Shutter");
        
        StunEnemy();
        if(SettingManager.Instance.Setting.m_game_setting.m_camera_light_is_on)
        {
            StartCoroutine(Fade());
        }
    }

    private IEnumerator Fade()
    {
        float elapsed_time = 0f;
        float in_target_time = 0.3f;
        float out_target_time = 10f;

        Color color = new Color(m_flash_image.color.r, m_flash_image.color.g, m_flash_image.color.b, 0f);

        while(elapsed_time <= in_target_time)
        {
            elapsed_time += Time.deltaTime;

            float t = elapsed_time / in_target_time;
            
            color.a = Mathf.Lerp(color.a, 1f, t);

            m_flash_image.color = color;

            yield return null;
        }

        elapsed_time = 0f;

        while(elapsed_time <= out_target_time)
        {
            elapsed_time += Time.deltaTime;

            float t = elapsed_time / out_target_time;
            
            color.a = Mathf.Lerp(color.a, 0f, t);

            m_flash_image.color = color;

            yield return null;
        }
    }

    public void StunEnemy()
    {
        int ray_count = 9;
        Vector3 y_offset = new Vector3(0f, 1f, 0f);
        float stun_angle = 45f;

        float start_angle = -20f;
        float offset_angle = stun_angle / ray_count;

        for(int i = 0; i < ray_count; i++)
        {
            float angle = start_angle + offset_angle * i;
            
            Vector3 ray_direction = Quaternion.Euler(0f, angle, 0f) * Camera.main.transform.forward;

            var ray = new Ray(transform.position + y_offset, ray_direction);
            if(Physics.Raycast(ray, out RaycastHit hit, 25f))
            {
                if(hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<EnemyCtrl>().GetStun(5f);
                }
            }
        }
    }
}
