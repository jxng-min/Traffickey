using TMPro;
using UnityEngine;

public class Replayer : MonoBehaviour
{
    [Header("수집한 열쇠 개수")]
    [SerializeField] private TMP_Text m_key_label;
    public TMP_Text KeyLabel
    {
        get { return m_key_label; }
    }

    [Header("플레이 타임")]
    [SerializeField] private TMP_Text m_play_time_label;
    private float m_play_time;

    private void Update()
    {
        if(GameManager.Instance.Current == GameEventType.Playing)
        {
            m_play_time += Time.deltaTime;
        }
    }

    public void BTN_Retry()
    {
        SettingManager.Instance.SaveData();
        LoadingManager.Instance.LoadScene("Game");
    }

    public void BTN_Title()
    {
        SettingManager.Instance.SaveData();
        LoadingManager.Instance.LoadScene("Title");
    }

    public void Setting()
    {
        KeyLabel.text = "수집한 열쇠의 개수: " + GameObject.Find("Inventory Manager").GetComponent<Inventory>().GetItemCount(ItemCode.KEY).ToString();
        m_play_time_label.text = $"생존 시간:              {(int)m_play_time / 60}:{(int)m_play_time % 60}";
    }
}
