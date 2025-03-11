using UnityEngine;

public class TitleCtrl : MonoBehaviour
{
    private void Start()
    {
        GameEventBus.Publish(GameEventType.None);
        
        if(SettingManager.Instance.Setting.m_sound_setting.m_background_is_on)
        {
            if(SoundManager.Instance.BGM.clip is null)
            {
                SoundManager.Instance.PlayBGM("Title Background");
                return;
            }

            if(SoundManager.Instance.BGM.clip.name != "Title Background")
            {
                SoundManager.Instance.PlayBGM("Title Background");
            }
        }    
    }
    
    public void BTN_StartNewGame()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        LoadingManager.Instance.LoadScene("Prologue");
    }

    public void BTN_Credit()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        LoadingManager.Instance.LoadScene("Info");
    }

    public void BTN_Exit()
    {
        SoundManager.Instance.PlayEffect("Button Click");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
