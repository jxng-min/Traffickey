using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static bool m_is_ui_active = false;
    public static bool IsActive
    {
        get { return m_is_ui_active; }
        private set { m_is_ui_active = value;}
    }

    [Header("인벤토리 UI 오브젝트")]
    [SerializeField] private GameObject m_inventory_ui_object;

    [Header("슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("화살표들의 부모 트랜스폼")]
    [SerializeField] private Transform m_arrow_root;

    [Header("인벤토리 애니메이터")]
    [SerializeField] private Animator m_animator;

    [Header("인벤토리가 열려있는 시간")]
    [SerializeField] private float m_open_time;
    private float m_elapsed_time;

    [Space(10)]
    [Header("열쇠 카운터")]
    [SerializeField] private TMP_Text m_key_counter;

    [Space(10)]
    [Header("플레이어의 팔")]
    [SerializeField] private GameObject m_players_arm;

    [Header("플레이어의 플래시라이트 팔")]
    [SerializeField] private GameObject m_players_flashlight_arm;

    private Slot[] m_slots;

    public Slot[] Slots
    {
        get { return m_slots; }
    }

    private GameObject[] m_arrows;
    private int m_arrow_index = 0;

    private void Awake()
    {
        if(m_inventory_ui_object.activeSelf)
        {
            m_animator.SetBool("Inventory", false);
        }

        m_slots = m_slot_root.GetComponentsInChildren<Slot>();
        
        List<GameObject> arrow_list = new List<GameObject>();
        foreach(Transform child in m_arrow_root.transform)
        {
            arrow_list.Add(child.gameObject);
        }
        m_arrows = arrow_list.ToArray();

        m_key_counter.text = $"0 / 5";
    }

    private void Update()
    {
        CheckOpenTime();

        if(GameManager.Instance.Current == GameEventType.Playing)
        {
            if(Input.GetAxis("Mouse ScrollWheel") is not 0f)
            {
                SoundManager.Instance.PlayEffect("Button Click");
                
                m_arrows[m_arrow_index].SetActive(true);

                m_elapsed_time = 0f;
                ToggleUI(true);

                if(IsActive)
                {
                    m_arrows[m_arrow_index].SetActive(false);

                    if(Input.GetAxis("Mouse ScrollWheel") > 0f)
                    {
                        m_arrow_index = PrevArrowIndex();
                        m_arrows[m_arrow_index].SetActive(true);
                    }
                    else
                    {
                        m_arrow_index = NextArrowIndex();
                        m_arrows[m_arrow_index].SetActive(true);
                    }
                }
            }

            if(IsActive)
            {
                if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if(m_slots[m_arrow_index].Item is not null)
                    {
                        m_slots[m_arrow_index].UseItem();
                    }
                }
            }
        }
    }

    private int NextArrowIndex()
    {
        if(m_arrow_index == m_arrows.Length - 1)
        {
            return 0;
        }

        return m_arrow_index + 1;
    }

    private int PrevArrowIndex()
    {
        if(m_arrow_index == 0)
        {
            return m_arrows.Length - 1;
        }

        return m_arrow_index - 1;
    }

    public void ToggleUI(bool flag)
    {
        IsActive = flag;
        m_animator.SetBool("Inventory", flag);

        foreach(var arrow in m_arrows)
        {
            arrow.SetActive(false);
        }
    }

    private void CheckOpenTime()
    {
        if(IsActive)
        {
            m_elapsed_time += Time.deltaTime;

            if(m_elapsed_time >= m_open_time)
            {
                ToggleUI(false);
            }
        }
        else
        {
            m_elapsed_time = 0f;
        }
    }

    public void AcquireItem(Item item, Slot target_slot, int count = 1)
    {
        if(item.Overlap)
        {
            if(target_slot.Item is not null && target_slot.IsMask(item))
            {
                if(target_slot.Item.ID == item.ID)
                {
                    target_slot.UpdateSlotCount(count);
                }
            }
        }
        else
        {
            target_slot.AddItem(item, count);
        }
    }

    public void AcquireItem(Item item, int count = 1)
    {
        for(int i = 0; i < m_slots.Length; i++)
        {
            if(m_slots[i].Item is not null && m_slots[i].IsMask(item))
            {
                if(item.Overlap && m_slots[i].Item.ID == item.ID)
                {
                    m_slots[i].UpdateSlotCount(count);

                    if(item.ID == (int)ItemCode.KEY)
                    {
                        m_key_counter.text = $"{GetItemCount(ItemCode.KEY)} / 5";

                        if(GetItemCount(ItemCode.KEY) >= 5)
                        {
                            LoadingManager.Instance.LoadScene("Epilogue");
                        }
                    }

                    return;
                }
            }

            if(m_slots[i].Item is null && m_slots[i].IsMask(item))
            {
                m_slots[i].AddItem(item, count);

                if(item.ID == (int)ItemCode.KEY)
                {
                    m_key_counter.text = $"{GetItemCount(ItemCode.KEY)} / 5";

                    if(GetItemCount(ItemCode.KEY) >= 5)
                    {
                        LoadingManager.Instance.LoadScene("Epilogue");
                    }
                }
                else if(item.ID == (int)ItemCode.FLASHLIGHT)
                {
                    m_players_arm.SetActive(false);
                    m_players_flashlight_arm.SetActive(true);
                }

                return;
            }
        }
    }

    public Slot GetEmptySlot(Item item)
    {
        foreach(var slot in m_slots)
        {
            if(item.Overlap && slot.Item.ID == item.ID)
            {
                return slot;
            }

            if(slot.Item is null)
            {
                return slot;
            }
        }

        return null;
    }

    public int GetItemCount(ItemCode item_code)
    {
        int count = 0;
        foreach(var slot in m_slots)
        {
            if(slot.Item is null)
            {
                continue;
            }

            if(slot.Item.ID == (int)item_code)
            {
                count += slot.Count;
            }
        }

        return count;
    }
}