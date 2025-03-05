using UnityEngine;
using TMPro;

public class GameOverSceneController : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    void Start()
    {
        float gameOverTime = PlayerPrefs.GetFloat("GameOverTime", 0f);
        int minutes = Mathf.FloorToInt(gameOverTime / 60f);
        int seconds = Mathf.FloorToInt(gameOverTime % 60f);
        int milliseconds = Mathf.FloorToInt((gameOverTime * 100f) % 100f);
        timerText.text = "플레이 타임 : " + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        
    }
}
