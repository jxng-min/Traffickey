using UnityEngine;
using UnityEngine.AI;

public class DoctorSoundDetect : MonoBehaviour
{
    public AudioClip detectionSound; // 소리 클립
    private AudioSource audioSource; // 오디오 소스

    private NavMeshAgent agent;
    private GameObject player;
    public float soundDetectionRadius = 40f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = detectionSound;
        audioSource.loop = true; // 소리가 끝나면 다시 시작
    }

    void SoundChecking()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, soundDetectionRadius);
        bool playerDetected = false;

        //foreach (Collider collider in colliders)
        //{
        //    if (collider.CompareTag("Player") && !CharacterMove.isHiding) // 플레이어가 숨지 않았을 때만 감지
        //    {
        //        Vector3 soundPosition = collider.transform.position;
        //        agent.SetDestination(soundPosition);
        //        playerDetected = true;
        //        break;
        //    }
        //}
        // 플레이어 감지시 음악 재생
        if (playerDetected && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        // 플레이어를 감지하지 못하면 음악 중지
        else if (!playerDetected && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void Update()
    {
        SoundChecking();
    }
}
