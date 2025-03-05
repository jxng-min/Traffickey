using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryControllerKD : MonoBehaviour
{
    [SerializeField]
    private DiaryKD m_current_diary;

    public bool m_is_use = false;

    public static bool m_can_toggle = false;

    [SerializeField]
    private GameObject m_diary_panel;

    private void Start()
    {
        ToolManagerKD.m_current_tool = m_current_diary.GetComponent<Transform>();
    }

    void Update()
    {
        if(m_can_toggle)
            TryUse();
    }

    private void TryUse()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (InventoryKD.m_is_inventory_active)
                {
                    if (hit.collider.CompareTag("Item"))
                    {
                        m_diary_panel.gameObject.SetActive(false);
                        m_is_use = false;
                    }
                    else
                    {
                        if (!m_is_use)
                        {
                            m_diary_panel.gameObject.SetActive(true);
                            Time.timeScale = 0.0f;
                            m_is_use = true;
                        }
                        else
                        {
                            m_diary_panel.gameObject.SetActive(false);
                            Time.timeScale = 1.0f;
                            m_is_use = false;
                        }
                    }
                }
            }
        }
    }

    public void DiaryChange(DiaryKD _diary)
    {
        if (ToolManagerKD.m_current_tool != null)
        {
            ToolManagerKD.m_current_tool.gameObject.SetActive(false);
        }

        m_current_diary = _diary;
        ToolManagerKD.m_current_tool = m_current_diary.GetComponent<Transform>();
        m_current_diary.gameObject.SetActive(true);
    }
}
