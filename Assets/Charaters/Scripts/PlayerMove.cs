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
        // WASD 이동 처리
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 마우스 회전 처리 (상하 회전은 카메라만)
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += horizontalInput * rotationSpeed;

        // 카메라 상하 회전 제어
        float currentXRotation = cameraTransform.localRotation.eulerAngles.x;
        currentXRotation -= mouseY;
        currentXRotation = Mathf.Clamp(currentXRotation, maxDownRotation, maxUpRotation);
        cameraTransform.localRotation = Quaternion.Euler(currentXRotation, 0, 0);

        // 캐릭터 이동 적용
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);
        transform.rotation = Quaternion.Euler(new Vector3(0, rotation.y, 0));
    }
}