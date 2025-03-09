using UnityEngine;

public class TitleCtrl : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayBGM("Title Background");    
    }
    
    public void BTN_StartNewGame()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        LoadingManager.Instance.LoadScene("Game");
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
