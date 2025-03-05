using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMove : MonoBehaviour
{
    public float m_sensitivity = 50.0f;
    private float m_rotation_x;
    public MouseMove m_player;
    
    [SerializeField]
    private Camera mainCamera;

    private void Start()
    {
    }

    private void Update()
    {
        if (!InventoryKD.m_is_inventory_active)
        {
            float mouse_move_x = Input.GetAxis("Mouse X");

            m_rotation_x += mouse_move_x * m_player.m_sensitivity * Time.deltaTime;


            mainCamera.transform.rotation = Quaternion.Euler(90, m_rotation_x, 0f);
        }
    }

}
