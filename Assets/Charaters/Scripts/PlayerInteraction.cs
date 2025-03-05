using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float pushForce = 10f; // 밀기 힘의 크기

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable")) // 물체의 태그를 설정하여 상호작용 가능한 물체를 구분
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