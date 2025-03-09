using UnityEngine;
using UnityEngine.Video;

public class PlayerDeadState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;
    private string m_enemy_name;
    private VideoPlayer[] m_dead_videos;

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
        }

        Debug.Log(Enemy);

        if(Enemy == "Doctor")
        {
            foreach(var video in m_dead_videos)
            {
                if(video.gameObject.name == "Doctor Dead Video")
                {
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
                    video.gameObject.SetActive(true);
                    video.Play();
                }
            }
        }

        // TODO: 게임 상태 죽음 변경
    }

    public void Execute()
    {

    }

    public void ExecuteExit()
    {

    }
}
