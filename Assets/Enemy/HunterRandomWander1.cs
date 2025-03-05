using UnityEngine;
using UnityEngine.AI;

public class HunterRandomWander1 : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private AudioSource audioSource;
    public AudioClip movementAudioClip; // 이동 사운드 클립
    public VideoPlayerController m_dead_panel;
    private bool m_player_dead = false;
    public float normalSpeed = 2.0f;
    public float followSpeed = 2.0f; // 플레이어를 추적할 때의 속도
    public float playerProximityThreshold = 10f; // 플레이어와의 최소 거리 임계값

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.m_is_pause && !m_player_dead)
        {
            if (CharacterMove.isHiding)
            {
                // 플레이어가 숨어 있으면 랜덤 위치로 이동
                agent.speed = normalSpeed;
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    SetRandomDestination();
                }
            }
            else
            {
                // 플레이어가 숨어 있지 않으면 플레이어를 추적
                agent.speed = followSpeed;
                SetDestination(target.position);
                CheckForPlayerCollision();
                
            }

            float distanceToPlayer = Vector3.Distance(transform.position, target.position);
            if (distanceToPlayer <= playerProximityThreshold)
            {
                PlayMovementSound();
            }
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomPosition = RandomNavSphere(transform.position, 20f, -1);
        agent.SetDestination(randomPosition);
    }

    void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    void CheckForPlayerCollision()
    {
        CharacterMove playerCharacterMove = FindObjectOfType<CharacterMove>();
        Vector3 rayOrigin;

        // 플레이어가 앉아있는 경우, 레이의 시작 위치를 낮춥니다.
        if (playerCharacterMove != null && playerCharacterMove.m_is_crouching)
        {
            rayOrigin = transform.position - Vector3.up * 3.0f; // 앉은 상태의 높이로 조정
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
                //GameManager.m_game_state = GameManager.GAMESTATE.GAMEOVER;
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red);
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, int areaMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, areaMask);
        return navHit.position;
    }

    void PlayMovementSound()
    {
        if (!audioSource.isPlaying && movementAudioClip != null)
        {
            audioSource.clip = movementAudioClip;
            audioSource.Play();
        }
    }






}
