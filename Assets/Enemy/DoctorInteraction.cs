using UnityEngine;

public class DoctorInteraction : MonoBehaviour
{
    private bool m_player_dead = false;
    public VideoPlayerController m_dead_panel;

    void Update()
    {
        if (!GameManager.m_is_pause)
        {
            CharacterMove playerCharacterMove = FindObjectOfType<CharacterMove>();
            Vector3 rayOrigin;

            // 플레이어가 앉아있는 경우, 레이의 시작 위치를 낮춥니다.
            if (playerCharacterMove != null && playerCharacterMove.m_is_crouching)
            {
                rayOrigin = transform.position - Vector3.up * 5.0f; // 앉은 상태의 높이로 조정
            }
            else
            {
                rayOrigin = transform.position - Vector3.up * 2.0f; // 기본 높이
            }

            Ray ray = new Ray(rayOrigin, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("닿음");
                    m_dead_panel.PlayVideo();
                    m_player_dead = true;
                    // GameManager.m_game_state = GameManager.GAMESTATE.GAMEOVER;
                }

                if (hit.collider.CompareTag("Door"))
                {
                    Animator doorAnimator = hit.collider.GetComponent<Animator>();
                    doorAnimator.SetBool("open", true);
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red);
        }
    }

    // 다른 상호 작용 관련 함수와 변수
}
