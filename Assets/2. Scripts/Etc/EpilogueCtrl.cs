using System.Collections;
using TMPro;
using UnityEngine;

public class EpilogueCtrl : MonoBehaviour
{
    [Header("프롤로그 텍스트를 작성할 라벨")]
    [SerializeField] private TMP_Text m_epilogue_label;

    [Space(30)]
    [Header("프롤로그 라벨에 들어갈 텍스트 목록")]
    [SerializeField][TextArea] private string[] m_epilogue_texts;

    private int m_current_index = 0;

    private void Start()
    {
        GameEventBus.Publish(GameEventType.Clear);

        SoundManager.Instance.PlayBGM("Prologue Background");

        StartCoroutine(ShowPrologue());
    }

    private IEnumerator ShowPrologue()
    {
        m_epilogue_label.text = m_epilogue_texts[m_current_index];

        yield return new WaitForSeconds(1f);

        float elapsed_time = 0f;
        float target_time = 2f;

        Color color = m_epilogue_label.color;

        while(elapsed_time <= target_time)
        {
            elapsed_time += Time.deltaTime;
            yield return null;

            float t = elapsed_time / target_time;

            color.a = Mathf.Lerp(0f, 1f, t);
            m_epilogue_label.color = color;
        }

        color.a = 1f;
        m_epilogue_label.color = color;

        elapsed_time = 0f;

        while(elapsed_time <= target_time)
        {
            elapsed_time += Time.deltaTime;
            yield return null;

            float t = elapsed_time / target_time;

            color.a = Mathf.Lerp(1f, 0f, t);
            m_epilogue_label.color = color;
        }

        if(m_current_index < m_epilogue_texts.Length - 1)
        {
            m_current_index++;
            yield return StartCoroutine(ShowPrologue());
        }
        else
        {
            yield return new WaitForSeconds(1f);
            LoadingManager.Instance.LoadScene("Title");
        }
    }
}
