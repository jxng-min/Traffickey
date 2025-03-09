using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private Transform m_player;

    public Vector2 Delta { get; set; }
    public bool Inversal { get; set; } = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;   
    }

    private void Update()
    {
        if(!Setter.IsActive)
        {
            Rotation();
            Position();
        }
    }

    private void Rotation()
    {
        Delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Vector3 direction = transform.rotation.eulerAngles;
        float final_x = Inversal ? (direction.x - Delta.y) : (direction.x + Delta.y);
        if(final_x < 180f)
        {
            final_x = Mathf.Clamp(final_x, -1f, 60f);
        }
        else
        {
            final_x = Mathf.Clamp(final_x, 340, 361f);
        }

        transform.rotation = Quaternion.Euler(final_x, direction.y + Delta.x, direction.z);
    }

    private void Position()
    {
        transform.position = m_player.position + Vector3.up * 2.75f;
    }
}
