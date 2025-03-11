using UnityEngine;

public class PlayerIdleState : MonoBehaviour, IState<PlayerCtrl>
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
        CheckMove();

        StaminaManager.Instance.RegenStamina();
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
                if(StaminaManager.Instance.Current > 0f)
                {
                    m_player_ctrl.ChangeState(PlayerState.RUN);
                }
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
