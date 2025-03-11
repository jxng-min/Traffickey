using UnityEngine;

public class PlayerHideState : MonoBehaviour, IState<PlayerCtrl>
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
        if(m_player_ctrl.IsHide)
        {
            return;
        }

        CheckMove();
    }

    public void ExecuteExit()
    {

    }

    private void CheckMove()
    {
        if(m_player_ctrl.Direction.magnitude > 0f)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                m_player_ctrl.Move(8f);
            }
            else
            {
                m_player_ctrl.ChangeState(PlayerState.WALK);
            }
        }
        else
        {
            m_player_ctrl.ChangeState(PlayerState.IDLE);
        }
    }
}
