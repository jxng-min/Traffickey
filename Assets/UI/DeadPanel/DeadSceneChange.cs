using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadSceneChange : MonoBehaviour
{
    public string m_scene_to_load; // �̵��� ���� �̸�

    public void ChangeScene()
    {
        SceneManager.LoadScene(m_scene_to_load); // �� �ε� �Լ��� ����Ͽ� �� �̵�
    }
}
