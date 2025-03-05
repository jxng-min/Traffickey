using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameOverPanel : MonoBehaviour
{
    public GameObject dead_panel;
    public AudioSource audio_source;
    private void OnVideoEnd(VideoPlayer vp)
    {
        dead_panel.gameObject.SetActive(true);
        audio_source.Play();
        GameManager.m_game_state = GameManager.GAMESTATE.GAMEOVER;

        
    }
}
