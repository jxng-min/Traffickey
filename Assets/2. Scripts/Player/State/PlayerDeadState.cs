using UnityEngine;

public class PlayerDeadState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;
    private string m_enemy_name;
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
        }
    }

    public void Execute()
    {

    }

    public void ExecuteExit()
    {

    }
}
