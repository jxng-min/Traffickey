using UnityEngine;

public class EnemyIdleState : MonoBehaviour, IState<EnemyCtrl>
{
    private EnemyCtrl m_enemy_ctrl;
    private float m_idle_time;

    public void ExecuteEnter(EnemyCtrl sender)
    {
        if(m_enemy_ctrl is null)
        {
            m_enemy_ctrl = sender;
        }

        //_enemy_ctrl?.Animator.SetBool("IsPatrol", false);

        m_idle_time = UnityEngine.Random.Range(2f, 5f);
    }

    public void Execute()
    {
        m_enemy_ctrl.DetectPlayer();

        m_idle_time -= Time.deltaTime;
        if(m_idle_time <= 0f)
        {   
            m_enemy_ctrl.ChangeState(EnemyState.PATROL);
        }
    }
    public void ExecuteExit()
    {
        
    }
}
