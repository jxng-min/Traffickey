using UnityEngine;

public class EnemyStateContext
{
    private readonly EnemyCtrl m_enemy_ctrl;

    public IState<EnemyCtrl> Current { get; set; }

    public EnemyStateContext(EnemyCtrl enemy_ctrl)
    {
        m_enemy_ctrl = enemy_ctrl;
    }

    public void Transition(IState<EnemyCtrl> state)
    {
        Debug.Log(state);
        if(Current == state)
        {
            return;
        }

        Current?.ExecuteExit();
        Current = state;
        Current?.ExecuteEnter(m_enemy_ctrl);
    }

    public void ExecuteUpdate()
    {
        if(GameManager.Instance.Current == GameEventType.Playing)
        {
            if(m_enemy_ctrl is null)
            {
                return;
            }

            Current?.Execute();
        }
    }
}
