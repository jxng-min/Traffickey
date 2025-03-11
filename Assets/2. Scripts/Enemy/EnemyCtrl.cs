using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{
    #region 상태 변수
    private IState<EnemyCtrl> m_idle_state;
    private IState<EnemyCtrl> m_patrol_state;
    private IState<EnemyCtrl> m_trace_state;
    private IState<EnemyCtrl> m_stun_state;
    #endregion

    [Header("플레이어 오브젝트")]
    [SerializeField] private GameObject m_player;
    public GameObject Player
    {
        get { return m_player; }
    }

    [Header("배회 중앙 트랜스폼")]
    [SerializeField] private Transform m_patrol_center;
    public Transform PatrolCenter
    {
        get { return m_patrol_center; }
    }


    public EnemyStateContext StateContext { get; private set; }
    public Animator Animator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public float PatrolRange { get; set; } = 60f;
    public float DetectRange { get; set; } = 20f;
    public float FollowRange { get; set; } = 20f;

    private void Awake()
    {
        StateContext = new EnemyStateContext(this);

        m_idle_state = gameObject.AddComponent<EnemyIdleState>();
        m_patrol_state = gameObject.AddComponent<EnemyPatrolState>();
        m_trace_state = gameObject.AddComponent<EnemyTraceState>();
        m_stun_state = gameObject.AddComponent<EnemyStunState>();

        ChangeState(EnemyState.IDLE);

        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        StateContext.ExecuteUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        switch(state)
        {
            case EnemyState.IDLE:
                StateContext.Transition(m_idle_state);
                break;
            
            case EnemyState.PATROL:
                StateContext.Transition(m_patrol_state);
                break;

            case EnemyState.TRACE:
                StateContext.Transition(m_trace_state);
                break;
            
            case EnemyState.STUN:
                StateContext.Transition(m_stun_state);
                break;
        }
    }

    public void DetectPlayer()
    {
        // if(Player.GetComponent<PlayerCtrl>().StateContext.Current is PlayerDeadState)
        // {
        //     return;
        // }

        int ray_count = 36;
        Vector3 y_offset = new Vector3(0f, -3.5f, 0f);
        float detect_angle = 360f;

        float start_angle = 0f;
        float offset_angle = detect_angle / ray_count;

        for(int i = 0; i < ray_count; i++)
        {
            float angle = start_angle + offset_angle * i;
            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * transform.forward;

            var ray = new Ray(transform.position + y_offset, direction);
            if(Physics.Raycast(ray, out RaycastHit hit, DetectRange))
            {
                if(hit.transform.CompareTag("Player"))
                {
                    Debug.DrawRay(transform.position + y_offset, direction * DetectRange, Color.red);
                    ChangeState(EnemyState.TRACE);
                }
                else
                {
                    Debug.DrawRay(transform.position + y_offset, direction * DetectRange, Color.green);
                }
            }
            else
            {
                Debug.DrawRay(transform.position + y_offset, direction * DetectRange, Color.green);
            }
        }
    }

    public void GetStun(float stun_time)
    {
        (m_stun_state as EnemyStunState).StunTime = stun_time;
        ChangeState(EnemyState.STUN);
    }
}
