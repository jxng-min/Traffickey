using UnityEngine;

public class PlayerWalkState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;
    private CameraShaker m_camera_shaker;

    public void ExecuteEnter(PlayerCtrl sender)
    {
        if(m_player_ctrl is null)
        {
            m_player_ctrl = sender;
            m_camera_shaker = Camera.main.GetComponent<CameraShaker>();
        }

        PlayEffect();
    }

    public void Execute()
    {
        CheckMove();

        StaminaManager.Instance.RegenStamina();
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
                    m_player_ctrl.ChangeState(PlayerState.RUN);
                }
            }
            else
            {
                m_player_ctrl.Move(5f);
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
            SoundManager.Instance.PlayEffect("Walk");
            Invoke("PlayEffect", 0.8f);
        }
    }
}
