using UnityEngine;
using UnityEngine.AI;

public class DoctorSoundDetect : MonoBehaviour
{
    public AudioClip detectionSound; // �Ҹ� Ŭ��
    private AudioSource audioSource; // ����� �ҽ�

    private NavMeshAgent agent;
    private GameObject player;
    public float soundDetectionRadius = 40f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = detectionSound;
        audioSource.loop = true; // �Ҹ��� ������ �ٽ� ����
    }

    void SoundChecking()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, soundDetectionRadius);
        bool playerDetected = false;

        //foreach (Collider collider in colliders)
        //{
        //    if (collider.CompareTag("Player") && !CharacterMove.isHiding) // �÷��̾ ���� �ʾ��� ���� ����
        //    {
        //        Vector3 soundPosition = collider.transform.position;
        //        agent.SetDestination(soundPosition);
        //        playerDetected = true;
        //        break;
        //    }
        //}
        // �÷��̾� ������ ���� ���
        if (playerDetected && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        // �÷��̾ �������� ���ϸ� ���� ����
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
