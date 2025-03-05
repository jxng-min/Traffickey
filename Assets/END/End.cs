using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class End : MonoBehaviour
{
    public Image brightPanel;
    public float fadeDuration = 1.5f;

    private void Start()
    {
        StartCoroutine(BrightFadeIn());
        UnlockCursor();
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



    IEnumerator BrightFadeIn()
    {
        // �ʱ⿡ ������ ���·� ����
        brightPanel.color = new Color(1f, 1f, 1f, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            brightPanel.color = new Color(1f, 1f, 1f, alpha); // ���� ����������
            yield return null;
        }

        // �ʿ��ϴٸ� ���⼭ �߰� �۾� ����
    }
}
