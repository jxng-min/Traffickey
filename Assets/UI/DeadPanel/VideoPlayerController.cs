using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro; // �� ���� �߰�

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
        videoPlayer.Stop(); // ���� ����
        videoPanel.SetActive(false); // �г� ��Ȱ��ȭ
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        //dead_panel.gameObject.SetActive(true);
        audio_source.Play();
        GameManager.m_game_state = GameManager.GAMESTATE.GAMEOVER;
        float elapsedTime = FindObjectOfType<Timer>().GetElapsedTime();
        PlayerPrefs.SetFloat("GameOverTime", elapsedTime);

        

        // ���⿡ 'GameOver' ������ �Ѿ�� �ڵ� �߰�
        SceneManager.LoadScene("GameOver"); // "GameOver"�� �ε��Ϸ��� ���� �̸��Դϴ�.
    }
}
