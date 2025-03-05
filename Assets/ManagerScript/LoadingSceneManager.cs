using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider m_progress_bar;

    
    private void Start()
    {
        Time.timeScale = 1.0f;
        m_progress_bar.value = 0;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync("GameStage");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            if (m_progress_bar.value < 1f)
            {
                m_progress_bar.value = Mathf.MoveTowards(m_progress_bar.value, 1f, Time.deltaTime * 0.2f);
            }

            if (m_progress_bar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
