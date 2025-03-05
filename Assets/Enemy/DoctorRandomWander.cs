//using UnityEngine;
//using UnityEngine.AI;

//public class DocterRandomWander : MonoBehaviour
//{
//    private NavMeshAgent agent;
//    private GameObject player;
//    private float followRadius = 15.0f;
//    private float normalSpeed = 2.0f;
//    private float followSpeed = 5.0f;
//    private AudioSource audioSource;

//    private bool m_player_dead = false;

//    public AudioClip movementAudioClip;
//    public VideoPlayerController m_dead_panel;
//    public Color gizmoColor = Color.yellow;
//    public float soundDetectionRadius = 40f;

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = gizmoColor;
//        Gizmos.DrawWireSphere(transform.position, followRadius);
//        Gizmos.DrawWireSphere(transform.position, soundDetectionRadius);
//    }


//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        player = GameObject.FindGameObjectWithTag("Player");
//        audioSource = GetComponent<AudioSource>();
//        SetRandomDestination();
//    }

//    void SetRandomDestination()
//    {
//        if (player != null && !HideBox.isHidden)
//        {
//            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
//            if (distanceToPlayer <= followRadius)
//            {
//                Debug.Log("ÀÎ½ÄÇÔ");
//                agent.speed = followSpeed;
//                agent.SetDestination(player.transform.position);
//                return;
//            }
//        }
//        agent.speed = normalSpeed;
//        Vector3 randomPosition = RandomNavSphere(transform.position, 50f, -1);
//        agent.SetDestination(randomPosition);
//    }

//    void Update()
//    {
//        if (!GameManager.m_is_pause)
//        {
//            if (!agent.pathPending && agent.remainingDistance < 0.5f)
//            {
//                SetRandomDestination();
//                PlayMovementSound();
//            }

//            if (!m_player_dead)
//            {
//                Vector3 rayOrigin = transform.position - Vector3.up * 2.0f;
//                Ray ray = new Ray(rayOrigin, transform.forward);
//                RaycastHit hit;

//                if (Physics.Raycast(ray, out hit, 3f))
//                {
//                    if (hit.collider.CompareTag("Player"))
//                    {
//                        Debug.Log("´êÀ½");
//                        m_dead_panel.PlayVideo();
//                        m_player_dead = true;
//                        GameManager.m_game_state = GameManager.GAMESTATE.GAMEOVER;
//                    }

//                    if (hit.collider.CompareTag("Door"))
//                    {
//                        Animator doorAnimator = hit.collider.GetComponent<Animator>();
//                        doorAnimator.SetBool("open", true);
//                    }
//                }
//                Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red);
//            }
//            SoundChecking();
//        }
//    }

//    void PlayMovementSound()
//    {
//        if (!audioSource.isPlaying && movementAudioClip != null)
//        {
//            audioSource.clip = movementAudioClip;
//            audioSource.Play();
//        }
//    }

//    void SoundChecking()
//    {
//        Collider[] colliders = Physics.OverlapSphere(transform.position, soundDetectionRadius);
//        foreach (Collider collider in colliders)
//        {
//            if (collider.CompareTag("Player"))
//            {
//                Vector3 soundPosition = collider.transform.position;
//                agent.SetDestination(soundPosition);
//                break;
//            }
//        }
//    }

//    Vector3 RandomNavSphere(Vector3 origin, float distance, int areaMask)
//    {
//        Vector3 randomDirection = Random.insideUnitSphere * distance;

//        randomDirection += origin;

//        NavMeshHit navHit;
//        NavMesh.SamplePosition(randomDirection, out navHit, distance, areaMask);

//        return navHit.position;
//    }
//}