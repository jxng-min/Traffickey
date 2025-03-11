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

        PlayEffect();
    }

    public void Execute()
    {
        CheckMove();

        StaminaManager.Instance.UseStamina();
    }

    public void ExecuteExit()
    {
        CancelInvoke("PlayEffect");
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
    
    private void PlayEffect()
    {
        if(GameManager.Instance.Current == GameEventType.Playing)
        {
            SoundManager.Instance.PlayEffect("Run");
            Invoke("PlayEffect", 0.4f);
        }
    }
}
