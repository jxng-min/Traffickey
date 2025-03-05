using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float maxUpRotation = 50.0f;
    public float maxDownRotation = -50.0f;

    public Transform cameraTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // WASD �̵� ó��
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // ���콺 ȸ�� ó�� (���� ȸ���� ī�޶�)
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += horizontalInput * rotationSpeed;

        // ī�޶� ���� ȸ�� ����
        float currentXRotation = cameraTransform.localRotation.eulerAngles.x;
        currentXRotation -= mouseY;
        currentXRotation = Mathf.Clamp(currentXRotation, maxDownRotation, maxUpRotation);
        cameraTransform.localRotation = Quaternion.Euler(currentXRotation, 0, 0);

        // ĳ���� �̵� ����
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);
        transform.rotation = Quaternion.Euler(new Vector3(0, rotation.y, 0));
    }
}