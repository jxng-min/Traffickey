using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float pushForce = 10f; // �б� ���� ũ��

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable")) // ��ü�� �±׸� �����Ͽ� ��ȣ�ۿ� ������ ��ü�� ����
        {
            Rigidbody objectRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
            {
                Vector3 pushDirection = collision.contacts[0].point - transform.position;
                pushDirection = pushDirection.normalized;

                objectRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.GetComponent<LadderInteraction>() != null)
                {
                    hit.collider.GetComponent<LadderInteraction>().Interact();
                }
            }
        }
    }
}