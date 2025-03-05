using UnityEngine;

public class EnterManager : MonoBehaviour
{
    public GameObject resultDoctor; // ResultDoctor ������Ʈ�� ���� ����
    public GameObject hunter;       // Hunter ������Ʈ�� ���� ����
    public GameObject rule;         // Rule ������Ʈ�� ���� ����
    public GameObject clipboard;    // Clipboard ������Ʈ�� ���� ����
    public MouseMove mouseMoveScript;

    // DoctorMovement�� HunterRandomWander1�� ���� ����
    private DocterMovement doctorMovement;
    private HunterRandomWander1 hunterMovement;

    private bool fogControlActive = true; // Fog ���� Ȱ��ȭ ����

    void Start()
    {
        // ���� ������ �� ResultDoctor�� Hunter ������Ʈ�� ��Ȱ��ȭ
        resultDoctor.SetActive(false);
        hunter.SetActive(false);
        if (mouseMoveScript != null)
        {
            mouseMoveScript.EnableMouseMovement(false); // ó���� ���콺 �Է� ó�� ��Ȱ��ȭ
        }

        // DoctorMovement�� HunterRandomWander1 ��ũ��Ʈ ���� ��������
        doctorMovement = resultDoctor.GetComponent<DocterMovement>();
        hunterMovement = hunter.GetComponent<HunterRandomWander1>();
    }

    void Update()
    {
        // 1, 2, 3 ��ư �Է� ó��
        if (fogControlActive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // 1�� ��ư�� ������ �� �ӵ� ����
                doctorMovement.normalSpeed = 4.2f;
                doctorMovement.followSpeed = 4.7f;
                hunterMovement.normalSpeed = 6.0f;
                hunterMovement.followSpeed = 3.2f;

                
                PerformButtonAction();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // 2�� ��ư�� ������ �� �ӵ� ����
                doctorMovement.normalSpeed = 4.5f;
                doctorMovement.followSpeed = 5.0f;
                hunterMovement.normalSpeed = 6.0f;
                hunterMovement.followSpeed = 3.5f;

                
                PerformButtonAction();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // 3�� ��ư�� ������ �� �ӵ� ����
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
        // Fog �е� �� ��Ÿ ����
        RenderSettings.fogDensity = (Input.GetKeyDown(KeyCode.Alpha1) ? 0.13f :
                                     Input.GetKeyDown(KeyCode.Alpha2) ? 0.15f : 0.17f);

        if (mouseMoveScript != null)
        {
            mouseMoveScript.EnableMouseMovement(true); // ���콺 �Է� ó�� Ȱ��ȭ
        }
        DisableFogControlAndObjects();
    }

    void DisableFogControlAndObjects()
    {
        fogControlActive = false;     // Fog ���� ��� ��Ȱ��ȭ
        rule.SetActive(false);        // Rule ������Ʈ ��Ȱ��ȭ
        clipboard.SetActive(false);   // Clipboard ������Ʈ ��Ȱ��ȭ
        resultDoctor.SetActive(true); // ResultDoctor ������Ʈ Ȱ��ȭ
        hunter.SetActive(true);       // Hunter ������Ʈ Ȱ��ȭ
    }
}
