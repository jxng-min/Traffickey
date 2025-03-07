using UnityEngine;

public class ItemActionManager : MonoBehaviour
{
    [Header("플레이어 오브젝트")]
    [SerializeField] private PlayerCtrl m_player_ctrl;

    private Inventory m_inventory;

    private void Awake()
    {
        m_inventory = GetComponent<Inventory>();   
    }

    public bool UseItem(Item item, Slot called_slot = null)
    {
        switch(item.Type)
        {
            case ItemType.Consumable:
                switch(item.ID)
                {
                    case (int)ItemCode.WATER:
                        StaminaManager.Instance.RegenStamina(50f);
                        break;

                    case (int)ItemCode.CAMERA:
                        break;
                }
                break;
            
            case ItemType.Equipment:
                switch(item.ID)
                {
                    case (int)ItemCode.FLASHLIGHT:
                        FlashlightManager.Instance.ToggleFlashlight();
                        return false;
                }
                break;
        }

        return true;
    }
}
