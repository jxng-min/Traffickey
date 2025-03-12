using UnityEngine;
using UnityEngine.AI;

public class EnemyTraceState : MonoBehaviour, IState<EnemyCtrl>
{
    private EnemyCtrl m_enemy_ctrl;
    private GameObject m_player;
    private NavMeshAgent m_agent;

    public void ExecuteEnter(EnemyCtrl sender)
    {
        if(m_enemy_ctrl is null)
        {
            m_enemy_ctrl = sender;
            m_agent = m_enemy_ctrl.Agent;
            m_player = m_enemy_ctrl.Player;
        }

        m_agent.speed = 6f;
        m_enemy_ctrl.Animator.SetBool("IsPatrol", true);
        m_agent.stoppingDistance = 0.5f;

        SoundManager.Instance.PlayEffect("Warning");
    }

    public void Execute()
    {
        DistanceCheck();

        if(m_agent.velocity.magnitude > 0f && m_agent.remainingDistance <= m_agent.stoppingDistance)
        {
            Debug.Log("여기2");
            m_enemy_ctrl.ChangeState(EnemyState.IDLE);
        }
    }

    public void ExecuteExit()
    {
        m_enemy_ctrl.Animator.SetBool("IsPatrol", false);
        m_agent.stoppingDistance = 1f;
        m_agent.ResetPath();
    }

    private void DistanceCheck()
    {
        if(Vector3.Distance(transform.position, m_player.transform.position) <= m_enemy_ctrl.FollowRange)
        {
            m_agent.SetDestination(m_player.transform.position);
        }
        else
        {
            m_enemy_ctrl.ChangeState(EnemyState.IDLE);
        }
    }

    private void OnDrawGizmos()
    {
        if(m_enemy_ctrl is null)
        {
            return;
        }      

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_enemy_ctrl.FollowRange);
    }
}
