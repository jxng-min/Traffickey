using UnityEngine;

public class EnterManager : MonoBehaviour
{
    public GameObject resultDoctor; // ResultDoctor 오브젝트에 대한 참조
    public GameObject hunter;       // Hunter 오브젝트에 대한 참조
    public GameObject rule;         // Rule 오브젝트에 대한 참조
    public GameObject clipboard;    // Clipboard 오브젝트에 대한 참조
    public MouseMove mouseMoveScript;

    // DoctorMovement와 HunterRandomWander1에 대한 참조
    private DocterMovement doctorMovement;
    private HunterRandomWander1 hunterMovement;

    private bool fogControlActive = true; // Fog 조절 활성화 여부

    void Start()
    {
        // 씬이 시작할 때 ResultDoctor와 Hunter 오브젝트를 비활성화
        resultDoctor.SetActive(false);
        hunter.SetActive(false);
        if (mouseMoveScript != null)
        {
            mouseMoveScript.EnableMouseMovement(false); // 처음에 마우스 입력 처리 비활성화
        }

        // DoctorMovement와 HunterRandomWander1 스크립트 참조 가져오기
        doctorMovement = resultDoctor.GetComponent<DocterMovement>();
        hunterMovement = hunter.GetComponent<HunterRandomWander1>();
    }

    void Update()
    {
        // 1, 2, 3 버튼 입력 처리
        if (fogControlActive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // 1번 버튼을 눌렀을 때 속도 변경
                doctorMovement.normalSpeed = 4.2f;
                doctorMovement.followSpeed = 4.7f;
                hunterMovement.normalSpeed = 6.0f;
                hunterMovement.followSpeed = 3.2f;

                
                PerformButtonAction();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // 2번 버튼을 눌렀을 때 속도 변경
                doctorMovement.normalSpeed = 4.5f;
                doctorMovement.followSpeed = 5.0f;
                hunterMovement.normalSpeed = 6.0f;
                hunterMovement.followSpeed = 3.5f;

                
                PerformButtonAction();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // 3번 버튼을 눌렀을 때 속도 변경
                doctorMovement.normalSpeed = 5.0f;
                doctorMovement.followSpeed = 5.2f;
                hunterMovement.normalSpeed = 6.0f;
                hunterMovement.followSpeed = 3.8f;

                
                PerformButtonAction();
            }
        }
    }

    void PerformButtonAction()
    {
        // Fog 밀도 및 기타 설정
        RenderSettings.fogDensity = (Input.GetKeyDown(KeyCode.Alpha1) ? 0.13f :
                                     Input.GetKeyDown(KeyCode.Alpha2) ? 0.15f : 0.17f);

        if (mouseMoveScript != null)
        {
            mouseMoveScript.EnableMouseMovement(true); // 마우스 입력 처리 활성화
        }
        DisableFogControlAndObjects();
    }

    void DisableFogControlAndObjects()
    {
        fogControlActive = false;     // Fog 조절 기능 비활성화
        rule.SetActive(false);        // Rule 오브젝트 비활성화
        clipboard.SetActive(false);   // Clipboard 오브젝트 비활성화
        resultDoctor.SetActive(true); // ResultDoctor 오브젝트 활성화
        hunter.SetActive(true);       // Hunter 오브젝트 활성화
    }
}
