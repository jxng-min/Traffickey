using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("닥터 오브젝트")]
    private GameObject m_doctor;
    private AudioSource m_doctor_audio_source;
    private float m_doctor_distance;

    [Header("헌터 오브젝트")]
    private GameObject m_hunter;
    private AudioSource m_hunter_audio_source;
    private float m_hunter_distance;

    [SerializeField] float m_max_distance = 30f;
    [SerializeField] float m_min_value = 0f;
    [SerializeField] float m_max_value = 1f;

    private void Update()
    {
        if(GameManager.Instance.Current == GameEventType.Playing)
        {
            m_doctor_distance = Vector3.Distance(m_doctor.transform.position, GameManager.Instance.Player.transform.position);
            m_hunter_distance = Vector3.Distance(m_hunter.transform.position, GameManager.Instance.Player.transform.position);

            m_doctor_audio_source.volume = Mathf.Clamp(1f - (m_doctor_distance / m_max_distance), m_min_value, m_max_value);
            m_hunter_audio_source.volume = Mathf.Clamp(1f - (m_hunter_distance / m_max_distance), m_min_value, m_max_value);

            m_doctor_audio_source.pitch = Mathf.Clamp(0.8f, 1.5f, 1 - (m_doctor_distance / m_max_distance));
            m_hunter_audio_source.pitch = Mathf.Clamp(0.8f, 1.5f, 1 - (m_hunter_distance / m_max_distance));
        }
    }

    public void Initialization()
    {
        m_doctor = GameObject.Find("Doctor");
        m_hunter = GameObject.Find("Hunter");

        m_doctor_audio_source = m_doctor.GetComponent<AudioSource>();
        m_hunter_audio_source = m_hunter.GetComponent<AudioSource>();

        m_doctor_audio_source.loop = true;
        m_hunter_audio_source.loop = true;

        m_doctor_audio_source.Play();
        m_hunter_audio_source.Play();
    }

    public void StopAllEnemy()
    {
        m_doctor.GetComponent<NavMeshAgent>().isStopped = true;
        m_hunter.GetComponent<NavMeshAgent>().isStopped = true;

        m_doctor_audio_source.Pause();
        m_hunter_audio_source.Pause();
    }

    public void ResumeAllEnemy()
    {
        m_doctor.GetComponent<NavMeshAgent>().isStopped = false;
        m_hunter.GetComponent<NavMeshAgent>().isStopped = false;   

        m_doctor_audio_source.Play();
        m_hunter_audio_source.Play();  
    }
}
