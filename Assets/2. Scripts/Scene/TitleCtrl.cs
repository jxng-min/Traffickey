using UnityEngine;

public class TitleCtrl : MonoBehaviour
{
    public void BTN_StartNewGame()
    {
        LoadingManager.Instance.LoadScene("Game");
    }

    public void BTN_Credit()
    {
        LoadingManager.Instance.LoadScene("Info");
    }

    public void BTN_Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
