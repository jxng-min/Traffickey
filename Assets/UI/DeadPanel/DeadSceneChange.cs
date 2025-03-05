using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadSceneChange : MonoBehaviour
{
    public string m_scene_to_load; // 이동할 씬의 이름

    public void ChangeScene()
    {
        SceneManager.LoadScene(m_scene_to_load); // 씬 로드 함수를 사용하여 씬 이동
    }
}
