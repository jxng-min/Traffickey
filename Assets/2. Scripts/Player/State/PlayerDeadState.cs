using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerDeadState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;
    private string m_enemy_name;

    private VideoPlayer[] m_dead_videos;
    private VideoPlayer m_current_video;

    private Image m_dead_panel;
    private bool m_is_panel_on = false;

    private Replayer m_replayer;

    public string Enemy
    {
        get { return m_enemy_name; }
        set { m_enemy_name = value; }
    }

    public void ExecuteEnter(PlayerCtrl sender)
    {
        if(m_player_ctrl is null)
        {
            m_player_ctrl = sender;
            m_dead_videos = GameObject.Find("Dead UI").GetComponentsInChildren<VideoPlayer>(true);
            m_dead_panel = GameObject.Find("Dead UI").GetComponentInChildren<Image>(true);
            m_replayer = FindAnyObjectByType<Replayer>();
        }

        SoundManager.Instance.BGM.Stop();
        SoundManager.Instance.PlayEffect("Dead");

        if(Enemy == "Doctor")
        {
            foreach(var video in m_dead_videos)
            {
                if(video.gameObject.name == "Doctor Dead Video")
                {
                    m_current_video = video;
                    video.gameObject.SetActive(true);
                    video.Play();
                }
            }
        }
        else
        {
            foreach(var video in m_dead_videos)
            {
                if(video.gameObject.name == "Hunter Dead Video")
                {
                    m_current_video = video;
                    video.gameObject.SetActive(true);
                    video.Play();

                }
            }
        }
    }

    public void Execute()
    {
        if(m_current_video.time >= m_current_video.length - 0.1f)
        {
            if(m_is_panel_on is false)
            {
                m_is_panel_on = true;
                m_dead_panel.gameObject.SetActive(m_is_panel_on);
                m_replayer.Setting();

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void ExecuteExit()
    {
        m_is_panel_on = false;
        m_dead_panel.gameObject.SetActive(m_is_panel_on);
    }
}
