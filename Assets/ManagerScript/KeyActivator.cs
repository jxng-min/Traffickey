using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyActivator : MonoBehaviour
{
    [SerializeField]
    private ItemPickUpKD[] m_key_arr;

    private int m_random_key_seed;
    // Start is called before the first frame update
    void Start()
    {
        m_key_arr = GetComponentsInChildren<ItemPickUpKD>();

        for(int i = 0; i < m_key_arr.Length; i++)
            m_key_arr[i].gameObject.SetActive(false);

        m_random_key_seed = Random.Range(0, m_key_arr.Length);
        m_key_arr[m_random_key_seed].gameObject.SetActive(true);
    }
}
