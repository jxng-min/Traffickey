using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    #region 상태 변수
    private PlayerStateContext m_player_state_context;
    private IState<PlayerCtrl> m_idle_state;
    private IState<PlayerCtrl> m_walk_state;
    private IState<PlayerCtrl> m_run_state;
    private IState<PlayerCtrl> m_hide_state;
    private IState<PlayerCtrl> m_dead_state;
    #endregion

    public Rigidbody Rigidbody { get; private set; }
    public Vector3 Direction { get; private set; }

    public bool IsHide { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();

        m_player_state_context = new PlayerStateContext(this);

        m_idle_state = gameObject.AddComponent<PlayerIdleState>();
        m_walk_state = gameObject.AddComponent<PlayerWalkState>();
        m_run_state = gameObject.AddComponent<PlayerRunState>();
        m_hide_state = gameObject.AddComponent<PlayerHideState>();
        m_dead_state = gameObject.AddComponent<PlayerDeadState>();

        ChangeState(PlayerState.IDLE);
    }

    private void Update()
    {
        Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        m_player_state_context.ExecuteUpdate();
    }

    public void Move(float speed)
    {
        Vector3 forward_direction = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        Vector3 right_direction = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);

        Vector3 final_direction = ((forward_direction * Direction.z) + (right_direction * Direction.x)).normalized;

        Vector3 velocity = final_direction * speed;

        transform.forward = final_direction;

        Vector3 new_position = Rigidbody.position + velocity * Time.deltaTime;
        Rigidbody.MovePosition(new_position);
    }

    public void ChangeState(PlayerState state)
    {
        switch(state)
        {
            case PlayerState.IDLE:
                m_player_state_context.Transition(m_idle_state);
                break;
            
            case PlayerState.WALK:
                m_player_state_context.Transition(m_walk_state);
                break;
            
            case PlayerState.RUN:
                m_player_state_context.Transition(m_run_state);
                break;

            case PlayerState.HIDE:
                m_player_state_context.Transition(m_hide_state);
                break;
            
            case PlayerState.DEAD:
                m_player_state_context.Transition(m_dead_state);
                break;
        }
    }
}
