using UnityEngine;

public class PlayerDeadState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;

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
