public class GameManager : Singleton<GameManager>
{
    private GameEventType m_game_state;
    public GameEventType Current
    {
        get { return m_game_state; }
        private set { m_game_state = value; }
    }

    private PlayerCtrl m_player_ctrl;
    public PlayerCtrl Player
    {
        get { return m_player_ctrl; }
        private set { m_player_ctrl = value; }
    }

    private bool m_is_can_init = true;

    private new void Awake()
    {
        base.Awake();

        GameEventBus.Subscribe(GameEventType.None, None);
        GameEventBus.Subscribe(GameEventType.Loading, Loading);
        GameEventBus.Subscribe(GameEventType.Describing, Describing);
    }

    public void None()
    {
        Current = GameEventType.None;
        m_is_can_init = true;

        if(SettingManager.Instance.Setting.m_sound_setting.m_background_is_on)
        {
            if(SoundManager.Instance.BGM.clip is null || SoundManager.Instance.BGM.clip.name is not "Title Background")
            {            
                SoundManager.Instance.PlayBGM("Title Background");
            }
        }
    }

    public void Loading()
    {
        Current = GameEventType.Loading;
    }


    public void Describing()
    {
        Current = GameEventType.Describing;
    }

    public void Playing()
    {
        Current = GameEventType.Playing;

        if(m_is_can_init)
        {
            m_is_can_init = false;

            if(SettingManager.Instance.Setting.m_sound_setting.m_background_is_on)
            {
                SoundManager.Instance.PlayBGM("Game Background");
            }

            Player = FindAnyObjectByType<PlayerCtrl>();

            EnemyManager.Instance.Initialization();
            StaminaManager.Instance.Initialization();
        }
        else
        {
            SoundManager.Instance.BGM.Play();
            EnemyManager.Instance.ResumeAllEnemy();
        }
    }

    public void Setting()
    {
        Current = GameEventType.Setting;

        if(SettingManager.Instance.Setting.m_sound_setting.m_background_is_on)
        {
            SoundManager.Instance.BGM.Pause();
        }

        EnemyManager.Instance.StopAllEnemy();
    }

    public void Dead()
    {
        Current = GameEventType.Dead;

        EnemyManager.Instance.StopAllEnemy();
    }

    public void Clear()
    {
        Current = GameEventType.Clear;

        EnemyManager.Instance.StopAllEnemy();
    }
}
