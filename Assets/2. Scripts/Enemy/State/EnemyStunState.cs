using UnityEngine;

public class EnemyStunState : MonoBehaviour, IState<EnemyCtrl>
{
    private EnemyCtrl m_enemy_ctrl;
    private float m_stun_time;
    public float StunTime
    {
        get { return m_stun_time; }
        set { m_stun_time = value; }
    }

    public void ExecuteEnter(EnemyCtrl sender)
    {
        if(m_enemy_ctrl is null)
        {
            m_enemy_ctrl = sender;
        }

        Invoke("ChangeToIdle", m_stun_time);
    }

    public void Execute()
    {

    }

    public void ExecuteExit()
    {

    }

    public void ChangeToIdle()
    {
        m_enemy_ctrl.ChangeState(EnemyState.IDLE);
    }
}
