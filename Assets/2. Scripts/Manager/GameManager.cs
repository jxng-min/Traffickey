using UnityEngine;

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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Loading()
    {
        Current = GameEventType.Loading;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void Describing()
    {
        Current = GameEventType.Describing;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Setting()
    {
        Current = GameEventType.Setting;

        if(SettingManager.Instance.Setting.m_sound_setting.m_background_is_on)
        {
            SoundManager.Instance.BGM.Pause();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        EnemyManager.Instance.StopAllEnemy();
    }

    public void Dead()
    {
        Current = GameEventType.Dead;
        m_is_can_init = true;

        EnemyManager.Instance.StopAllEnemy();
    }

    public void Clear()
    {
        Current = GameEventType.Clear;
        m_is_can_init = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        EnemyManager.Instance.StopAllEnemy();
    }
}
