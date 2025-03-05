using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectTipText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_tip_text;   // ����� Text ������Ʈ

    private void Start()
    {
        int select = Random.Range(0, 6);   // 0���� 5������ ������ ����

        switch (select)
        {
            case 0:
                m_tip_text.text = "TIP. ���� �Ҹ��� �����ϴ� ���� �����ϴ�.";
                break;
            case 1:
                m_tip_text.text = "TIP. ���� �����Ѵٸ� ���� ���� ã������.";
                break;
            case 2:
                m_tip_text.text = "TIP. ����� �� ��򰡿� �����մϴ�.";
                break;
            case 3:
                m_tip_text.text = "TIP. Ż�ⱸ�� �߾ӿ� �����մϴ�.";
                break;
            case 4:
                m_tip_text.text = "TIP. �÷��̾��� �̵� �ӵ��� ���� �̵� �ӵ��� �����ϴ�.";
                break;
            case 5:
                m_tip_text.text = "TIP. ���� �Ҹ��� ��ġ�� ���� ���ϴ� ���� �����ϴ�.";
                break;
        }
    }
}