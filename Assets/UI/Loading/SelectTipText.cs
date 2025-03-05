using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectTipText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_tip_text;   // 연결된 Text 컴포넌트

    private void Start()
    {
        int select = Random.Range(0, 6);   // 0부터 5까지의 랜덤값 생성

        switch (select)
        {
            case 0:
                m_tip_text.text = "TIP. 심장 소리에 집중하는 것이 좋습니다.";
                break;
            case 1:
                m_tip_text.text = "TIP. 적을 마주한다면 숨을 곳을 찾으세요.";
                break;
            case 2:
                m_tip_text.text = "TIP. 열쇠는 방 어딘가에 존재합니다.";
                break;
            case 3:
                m_tip_text.text = "TIP. 탈출구는 중앙에 존재합니다.";
                break;
            case 4:
                m_tip_text.text = "TIP. 플레이어의 이동 속도와 적의 이동 속도는 같습니다.";
                break;
            case 5:
                m_tip_text.text = "TIP. 심장 소리가 겹치는 곳을 피하는 것이 좋습니다.";
                break;
        }
    }
}