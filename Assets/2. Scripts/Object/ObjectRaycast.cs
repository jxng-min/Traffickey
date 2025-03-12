using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectRaycast : MonoBehaviour
{
    private RaycastHit m_ray_hit;
    private GameObject m_current_object;
    private bool m_can_use;

    [Header("레이를 발사할 카메라")]
    [SerializeField] private Transform m_ray_camera;

    [Header("레이의 길이")]
    [SerializeField] private float m_ray_distance;

    [Header("레이가 감지할 레이어 마스크")]
    [SerializeField] private LayerMask m_ray_layer_mask;

    [Header("오브젝트 인디케이터")]
    [SerializeField] private TMP_Text m_object_indicator;

    [Header("환풍구 UI 오브젝트")]
    [SerializeField] private Image m_vent_ui_object;

    private bool m_is_vent_using = false;
    private void Update()
    {
        if(GameManager.Instance.Current == GameEventType.Playing)
        {
            CheckObject();

            if(m_can_use)
            {
                TryInteraction();
            }
        }
    }

    private void TryInteraction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            switch(m_current_object?.tag)
            {
                case "Door":
                    DoorInteraction();
                    break;
                
                case "Vent1":
                    Vent1Interaction();
                    break;
                
                case "Vent2":
                    Vent2Interaction();
                    break;
            }
        }
    }

    private void CheckObject()
    {
        Debug.DrawRay(transform.position, Camera.main.transform.forward * m_ray_distance, Color.red);
        if(Physics.Raycast(transform.position, Camera.main.transform.forward, out m_ray_hit, m_ray_distance, m_ray_layer_mask))
        {
            if(m_current_object == m_ray_hit.transform.gameObject)
            {
                return;
            }

            m_current_object = m_ray_hit.transform.gameObject;

            m_object_indicator.gameObject.SetActive(true);

            if(m_current_object.CompareTag("Untagged"))
            {
                ItemInfoDisappear();
            }
            else
            {
                switch(m_current_object.tag)
                {
                    case "Item":
                        m_object_indicator.text = "획득 (E)";
                        break;

                    default:
                        m_object_indicator.text = "상호작용 (E)";
                        break;

                }
                
                m_can_use = true;
            }
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    private void ItemInfoDisappear()
    {
        m_can_use = false;
        m_object_indicator.gameObject.SetActive(false);
        m_current_object = null;
    }

    private void DoorInteraction()
    {
        var door_anime = m_current_object.GetComponent<Animator>();

        if(!door_anime.GetBool("IsOpen"))
        {
            SoundManager.Instance.PlayEffect("Door Open");
        }
        else
        {
            SoundManager.Instance.PlayEffect("Door Close");
        }

        door_anime.SetBool("IsOpen", !door_anime.GetBool("IsOpen"));
    }

    private void Vent1Interaction()
    {
        if(m_is_vent_using)
        {
            return;
        }

        SoundManager.Instance.PlayEffect("Vent");
        m_is_vent_using = true;
        StartCoroutine(Fade(new Vector3(17f, -0.41f, -47f), Quaternion.Euler(0f, -90f, 0f)));
    }

    private void Vent2Interaction()
    {
        if(m_is_vent_using)
        {
            return;
        }

        SoundManager.Instance.PlayEffect("Vent");
        m_is_vent_using = true;
        StartCoroutine(Fade(new Vector3(-60f, -0.41f, 12f), Quaternion.Euler(0f, -180f, 0f)));
    }

    private IEnumerator Fade(Vector3 position, Quaternion rotation)
    {
        float elapsed_time = 0f;
        float target_time = 1f;

        Color color = new Color(m_vent_ui_object.color.r, m_vent_ui_object.color.g, m_vent_ui_object.color.b, 0f);

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;

            float f = elapsed_time / target_time;

            color.a = Mathf.Lerp(color.a, 1f, f);
            m_vent_ui_object.color = color;

            yield return null;
        }

        color.a = 1f;
        m_vent_ui_object.color = color;

        GameManager.Instance.Player.transform.position = position;
        Camera.main.transform.rotation = rotation;

        yield return new WaitForSeconds(0.5f);
        
        elapsed_time = 0f;
        target_time = 3f;

        while(elapsed_time < target_time)
        {
            elapsed_time += Time.deltaTime;
            float f = elapsed_time / target_time;

            color.a = Mathf.Lerp(color.a, 0f, f);
            m_vent_ui_object.color = color;

            yield return null;
        }

        color.a = 0f;
        m_vent_ui_object.color = color;

        m_is_vent_using = false;
    }
}
