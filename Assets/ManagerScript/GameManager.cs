using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool m_can_player_move = true;        // �÷��̾��� ������ ����
    public static bool m_is_inventory_open = false;     // �κ��丮 Ȱ��ȭ ����
    public static bool m_is_pause = false;              // ������ Ȱ��ȭ ����
    public static GAMESTATE m_game_state = GAMESTATE.RUNTIME;

    [SerializeField]
    private SoundController m_sound_controller;
    public MainAudio m_main_audio;

    public enum GAMESTATE
    {
        RUNTIME,
        GAMEOVER,
        GAMECLEAR
    }
    


    void Start()
    {
        ReSetGameState();
    }

    void Update()
    {
        if(m_is_inventory_open || m_is_pause)
        {
            SetMouseUnLock();
            m_can_player_move = false; 
        }
        else
        {
            SetMouseLock();
            m_can_player_move = true;
        }

        if (m_game_state == GAMESTATE.GAMECLEAR)
            GameClear();
        else if (m_game_state == GAMESTATE.GAMEOVER)
            GameOver();

        if(m_is_pause)
        {
            m_sound_controller.StopAudio();
            m_main_audio.gameObject.SetActive(false);
        }
        else
        {
            m_main_audio.gameObject.SetActive(true);
        }

        //if(m_game_state != GAMESTATE.RUNTIME)
        //{
        //    SetMouseUnLock();
        //}
    }

    private void GameClear()
    {
        SetMouseUnLock();
        m_game_state = GAMESTATE.GAMECLEAR;
        Debug.Log("���� Ŭ����");
        Time.timeScale = 0.0f;

    }

    private void GameOver()
    {
        SetMouseUnLock();
        m_game_state = GAMESTATE.GAMEOVER;
        Debug.Log("���� ����");
        Time.timeScale = 0.0f;
        m_sound_controller.StopAudio();
    }

    private void SetMouseLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SetMouseUnLock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    
    

    private static void ReSetGameState()
    {
        m_can_player_move = true;
        m_is_inventory_open = false;
        m_is_pause = false;
        m_game_state = GAMESTATE.RUNTIME;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        KeyCounter.m_key_counter = 0;
    }
}
