//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MouseMove : MonoBehaviour
//{
//    public float m_sesitivity = 200.0f;
//    private float m_rotation_x;
//    private float m_rotation_y;

//    private void Start()
//    {

//    }


//    private void Update()
//    {
//        float mouse_move_x = Input.GetAxis("Mouse X");
//        float mouse_move_y = Input.GetAxis("Mouse Y");

//        m_rotation_x += mouse_move_x * m_sesitivity * Time.deltaTime;
//        m_rotation_y -= mouse_move_y * m_sesitivity * Time.deltaTime;

//        m_rotation_y = Mathf.Clamp(m_rotation_y, -50f, 50f);

//        transform.eulerAngles = new Vector3(m_rotation_y, m_rotation_x, 0f);
//        //transform.Translate(new Vector3(m_rotation_x, m_rotation_y, 0));
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float m_sensitivity = 50.0f;
    private float m_rotation_x;
    private float m_rotation_y;
    public bool IsEnabled { get; private set; } = true;

    [SerializeField]
    private Camera mainCamera;
    public void EnableMouseMovement(bool enable)
    {
        IsEnabled = enable;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (!InventoryKD.m_is_inventory_active && IsEnabled)
        {
            float mouse_move_x = Input.GetAxis("Mouse X");
            float mouse_move_y = Input.GetAxis("Mouse Y");

            m_rotation_x += mouse_move_x * m_sensitivity * Time.deltaTime;
            m_rotation_y -= mouse_move_y * m_sensitivity * Time.deltaTime;

            m_rotation_y = Mathf.Clamp(m_rotation_y, -50f, 50f);

            mainCamera.transform.rotation = Quaternion.Euler(m_rotation_y, m_rotation_x, 0f);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
        }

    }

}