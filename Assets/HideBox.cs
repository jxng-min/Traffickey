using UnityEngine;

public class HideBox : MonoBehaviour
{
    private Vector3 originalPosition;
    private Collider playerCollider;

    private void Start()
    {
        playerCollider = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CharacterMove.isHiding) // ����: isHidden -> CharacterMove.isHiding
            {
                CharacterMove.isHiding = false; // ����: isHidden -> CharacterMove.isHiding
                transform.position = originalPosition;
                playerCollider.enabled = true;
            }
            else
            {
                RaycastHit hit;
                Vector3 raycastDirection = Camera.main.transform.forward;
                Ray ray = new Ray(transform.position, raycastDirection);

                float rayDistance = 5f;

                if (Physics.Raycast(ray, out hit, rayDistance) && hit.collider.CompareTag("Hide"))
                {
                    CharacterMove.isHiding = true; // ����: isHidden -> CharacterMove.isHiding
                    originalPosition = transform.position;
                    transform.position = hit.collider.gameObject.transform.position;
                    playerCollider.enabled = false;
                }

                Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
            }
        }
    }
}



//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class HideBox : MonoBehaviour
//{
//    private Vector3 originalPosition;
//    public static bool isHidden = false;
//    private Collider playerCollider;

//    public bool is_near_hide = false;
//    public TextMeshProUGUI m_hide_text;

//    private float rayDistance = 5f;

//    private void Start()
//    {
//        playerCollider = GetComponentInChildren<Collider>();
//    }

//    private void Update()
//    {
//        RaycastHit hit;
//        Vector3 raycastDirection = Camera.main.transform.forward;
//        Ray ray = new Ray(transform.position, raycastDirection);

//        if (isHidden)
//        {
//            isHidden = false;
//            transform.position = originalPosition;
//            playerCollider.enabled = true;
//            is_near_hide = false; // ����ĳ��Ʈ�� �ƹ� �͵� �������� ���� ���·� �ʱ�ȭ
//        }
//        else
//        {
//            if (Physics.Raycast(ray, out hit, rayDistance) && hit.collider.CompareTag("Hide"))
//            {
//                is_near_hide = true;
//                if (Input.GetKeyDown(KeyCode.E))
//                {
//                    isHidden = true;
//                    originalPosition = transform.position;
//                    transform.position = hit.collider.gameObject.transform.position;
//                    playerCollider.enabled = false;
//                }
//            }
//        }

//        if (is_near_hide && !isHidden)
//        {
//            m_hide_text.text = "�������� E�� ��������.";
//            m_hide_text.enabled = true;
//        }
//        else
//            m_hide_text.enabled = false;
//    }
//}

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class HideBox : MonoBehaviour
//{
//    private Vector3 originalPosition;
//    public static bool isHidden = false;
//    private Collider playerCollider;

//    private float rayDistance = 5f;

//    public TextMeshProUGUI m_hide_text;

//    private void Start()
//    {
//        playerCollider = GetComponentInChildren<Collider>();
//    }

//    private void Update()
//    {
//        RaycastHit hit;
//        Vector3 raycastDirection = transform.forward; // �÷��̾��� ���� �������� ���� ����
//        Ray ray = new Ray(transform.position, raycastDirection);

//        if (isHidden)
//        {
//            isHidden = false;
//            transform.position = originalPosition;
//            playerCollider.enabled = true;
//        }
//        else
//        {
//            if (Physics.Raycast(ray, out hit, rayDistance) && hit.collider.CompareTag("Hide"))
//            {
//                // �������� ���� ���¿��� ���̿� Hide �±׸� ���� ��ü�� ����� ��
//                m_hide_text.text = "�������� E�� ��������.";
//                m_hide_text.enabled = true;

//                if (Input.GetKeyDown(KeyCode.E))
//                {
//                    isHidden = true;
//                    originalPosition = transform.position;
//                    transform.position = hit.collider.gameObject.transform.position;
//                    playerCollider.enabled = false;
//                    m_hide_text.enabled = false;
//                }
//            }
//            else
//            {
//                // �������� ���� ���¿��� �ٸ� ��ü�� ���̰� ����� ��
//                m_hide_text.enabled = false;
//            }
//        }
//    }
//}