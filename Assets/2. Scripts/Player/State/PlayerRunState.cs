using UnityEngine;

public class PlayerRunState : MonoBehaviour, IState<PlayerCtrl>
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
        CheckDead();

        StaminaManager.Instance.UseStamina();
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
                    m_player_ctrl.Move(8f);
                }
                else
                {
                    m_player_ctrl.Move(5f);
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

    private void CheckDead()
    {
        // TODO: Dead 상태로 연결되는 조건 추가
    }
}
