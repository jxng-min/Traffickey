using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemRaycast : MonoBehaviour
{
    private RaycastHit m_ray_hit;
    private bool m_is_pick_up_active = false;
    private ItemPickUp m_current_item;

    [Header("레이어 마스크")]
    [SerializeField] private LayerMask m_item_layer_mask;

    [Header("레이 캐스팅 거리")]
    [SerializeField] private float m_ray_distance;

    [Header("레이를 발사할 카메라")]
    [SerializeField] private Camera m_ray_camera;

    [Header("인벤토리 UI")]
    [SerializeField] private Inventory m_inventory;

    [Header("아이템 인디케이터")]
    [SerializeField] private TMP_Text m_indicator_label;

    [Header("추가/삭제할 머티리얼")]
    [SerializeField] private Material m_material;
    private bool m_material_exist = false;

    private void Update()
    {
        CheckItem();

        if(m_is_pick_up_active)
        {
            TryPickUpItem();
        }
    }

    private void TryPickUpItem()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(m_current_item.Item.Type <= ItemType.Equipment)
            {
                var all_items = m_inventory.Slots;

                int count;
                for(count = 0; count < all_items.Length; count++)
                {
                    if(all_items[count].Item is null)
                    {
                        break;
                    }

                    if(all_items[count].Item.ID == m_current_item.Item.ID && all_items[count].Item.Overlap)
                    {
                        break;
                    }
                }

                if(count == all_items.Length)
                {
                    return;
                }

                TryPickUp();
                ItemInfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        Debug.DrawRay(m_ray_camera.transform.position, m_ray_camera.transform.forward * m_ray_distance, Color.yellow);
        if(Physics.Raycast(m_ray_camera.transform.position, m_ray_camera.transform.forward, out m_ray_hit, m_ray_distance, m_item_layer_mask))
        {
            if(m_ray_hit.transform.CompareTag("Item"))
            {
                ItemPickUp raycasted_item = m_ray_hit.transform.GetComponent<ItemPickUp>();

                if(m_current_item == raycasted_item)
                {
                    return;
                }

                if(m_current_item is not null)
                {
                    RemoveMaterial();
                }

                m_current_item = raycasted_item;

                m_indicator_label.gameObject.SetActive(true);
                m_indicator_label.text = ItemDataManager.Instance.GetName(m_current_item.Item.ID);

                Vector3 final_position = new Vector3(Screen.width / 2f, Screen.height / 2f + m_current_item.IndicatorHeight, 0f);
                m_indicator_label.GetComponent<RectTransform>().position = final_position;

                m_is_pick_up_active = true;
                
                AddMaterial();

                return;
            }
            else
            {
                ItemInfoDisappear();
            }
        }
        else
        {
            if(m_current_item is not null)
            {
                RemoveMaterial();
            }

            ItemInfoDisappear();
        }
    }

    private void ItemInfoDisappear()
    {
        m_is_pick_up_active = false;
        m_indicator_label.gameObject.SetActive(false);
        m_current_item = null;
    }

    private void TryPickUp()
    {
        if(m_is_pick_up_active)
        {
            m_inventory.ToggleUI(true);
            m_inventory.AcquireItem(m_current_item.Item);
            
            Destroy(m_current_item.gameObject);

            // TODO: 아이템 획득 사운드 추가

            ItemInfoDisappear();
        }
    }

    private void AddMaterial()
    {
        foreach(var renderer in m_current_item.GetComponentsInChildren<Renderer>(true))
        {
            Material[] materials = renderer.sharedMaterials;
            m_material_exist = false;

            foreach(var material in materials)
            {
                if(material == m_material)
                {
                    m_material_exist = true;
                    break;
                }
            }

            if(!m_material_exist)
            {
                ArrayUtility.Add(ref materials, m_material);
                renderer.sharedMaterials = materials;
            }
        }
    }

    private void RemoveMaterial()
    {
        foreach(var renderer in m_current_item.GetComponentsInChildren<Renderer>(true))
        {
            Material[] materials = renderer.sharedMaterials;

            for(int i = 0; i < materials.Length; i++)
            {
                if(materials[i] == m_material)
                {
                    ArrayUtility.RemoveAt(ref materials, i);
                    renderer.sharedMaterials = materials;

                    break;
                }
            }
        }
    }
}
