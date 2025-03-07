using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : MonoBehaviour, IState<EnemyCtrl>
{
    private EnemyCtrl m_enemy_ctrl;
    private NavMeshAgent m_agent;

    public void ExecuteEnter(EnemyCtrl sender)
    {
        if(m_enemy_ctrl is null)
        {
            m_enemy_ctrl = sender;
            m_agent = m_enemy_ctrl.Agent;
        }

        m_agent.stoppingDistance = 1f;

        Vector3 pos = RandomPos(m_enemy_ctrl.PatrolCenter.position, m_enemy_ctrl.PatrolRange);
        
        m_agent.SetDestination(pos);

        //m_enemy_ctrl.Animator.SetBool("IsPatrol", true);
    }

    public void Execute()
    {
        m_enemy_ctrl.DetectPlayer();

        if(m_agent.remainingDistance <= m_agent.stoppingDistance)
        {
            m_enemy_ctrl.ChangeState(EnemyState.IDLE);
        }
    }

    public void ExecuteExit()
    {
        m_agent.ResetPath();
        //m_enemy_ctrl.Animator.SetBool("IsPatrol", false);
    }

    private Vector3 RandomPos(Vector3 center, float range)
    {
        Vector2 circle_pos = Random.insideUnitCircle * range;
        Vector3 rand_pos = new Vector3(center.x + circle_pos.x, center.y, center.z + circle_pos.y);

        NavMeshHit pos;

        for(int i = 0; i < 100; i++)
        {
            if(NavMesh.SamplePosition(rand_pos, out pos, 60f, NavMesh.AllAreas))
            {
                return pos.position;
            }
        }

        return m_agent.destination;
    }

    private void OmosSelected()
    {
        if(m_enemy_ctrl is null)
        {
            return;
        }       

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(m_enemy_ctrl.PatrolCenter.position, m_enemy_ctrl.PatrolRange);
    }
}
