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

            // �÷��̾ �ɾ��ִ� ���, ������ ���� ��ġ�� ����ϴ�.
            if (playerCharacterMove != null && playerCharacterMove.m_is_crouching)
            {
                rayOrigin = transform.position - Vector3.up * 5.0f; // ���� ������ ���̷� ����
            }
            else
            {
                rayOrigin = transform.position - Vector3.up * 2.0f; // �⺻ ����
            }

            Ray ray = new Ray(rayOrigin, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("����");
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

    // �ٸ� ��ȣ �ۿ� ���� �Լ��� ����
}
