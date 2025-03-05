using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro; // 이 라인 추가

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPanel;
    public AudioSource audio_source;
    public AudioClip dead_clip;

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
        videoPlayer.Stop();
        videoPanel.SetActive(false);
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    public void PlayVideo()
    {
        audio_source.clip = dead_clip;
        videoPanel.SetActive(true);
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop(); // 비디오 중지
        videoPanel.SetActive(false); // 패널 비활성화
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        //dead_panel.gameObject.SetActive(true);
        audio_source.Play();
        GameManager.m_game_state = GameManager.GAMESTATE.GAMEOVER;
        float elapsedTime = FindObjectOfType<Timer>().GetElapsedTime();
        PlayerPrefs.SetFloat("GameOverTime", elapsedTime);

        

        // 여기에 'GameOver' 씬으로 넘어가는 코드 추가
        SceneManager.LoadScene("GameOver"); // "GameOver"는 로드하려는 씬의 이름입니다.
    }
}
