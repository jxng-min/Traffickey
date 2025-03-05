using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerKD : MonoBehaviour
{
    // ���� ������ Hand�� Ÿ�� ����
    [SerializeField]
    private HandKD m_current_hand;

    // ��� ��?
    private bool m_is_use = false;
    private bool m_is_swing = false;

    private RaycastHit m_hit_info;

    private void Start()
    {
        ToolManagerKD.m_current_tool = m_current_hand.GetComponent<Transform>();
    }

    void Update()
    {
        TryUse();
    }

    private void TryUse()
    {
        if(Input.GetButton("Fire1"))
        {
            if(!m_is_use)
            {
                StartCoroutine(UseCoroutine());
            }
        }
    }

    IEnumerator UseCoroutine()
    {
        m_is_use = true;
        //�ִϸ��̼��̳� ���� ��� ��� ����Ѱ� �ۼ�

        yield return new WaitForSeconds(m_current_hand.m_use_delay_a);
        m_is_swing = true;

        // ���� ��� Ȱ��ȭ ����
        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(m_current_hand.m_use_delay_b);
        m_is_swing = false;

        yield return new WaitForSeconds(m_current_hand.m_use_delay - m_current_hand.m_use_delay_a - m_current_hand.m_use_delay_b);
        m_is_use = false;
    }

    IEnumerator HitCoroutine()
    {
        while(m_is_swing)
        {
            if(CheckObject())
            {
                m_is_swing = false;
                Debug.Log(m_hit_info.transform.name);
            }
            yield return null;
        }
    }

    private bool CheckObject()
    {
        if(Physics.Raycast(transform.position, transform.forward, out m_hit_info, m_current_hand.m_range))
        {
            return true;
        }
        return false;
    }

    public void HandChange(HandKD _hand)
    {
        if (ToolManagerKD.m_current_tool != null)
        {
            ToolManagerKD.m_current_tool.gameObject.SetActive(false);
        }

        m_current_hand = _hand;
        ToolManagerKD.m_current_tool = m_current_hand.GetComponent<Transform>();

        // m_current_flashlight.transform.localPosition = Vector3.zero;
        m_current_hand.gameObject.SetActive(true);
    }
}
