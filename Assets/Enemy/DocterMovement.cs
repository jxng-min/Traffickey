using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DocterMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private float followRadius = 15.0f;
    public float normalSpeed = 2.2f;
    public float followSpeed = 2.7f;
    private bool isChangingIntensity = false;
    //private bool playerDetected = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        SetRandomDestination();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= followRadius && !CharacterMove.isHiding)
        {           
                FollowPlayer();          
        }
        else if (isChangingIntensity)
        {
            StopBlinking();
            SetRandomDestination();
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    void FollowPlayer()
    {
        if (CharacterMove.isHiding)
        {
            SetRandomDestination();
        }
        else
        {
            agent.speed = followSpeed;
            agent.SetDestination(player.transform.position);

            if (!isChangingIntensity)
            {
                isChangingIntensity = true;
                StartCoroutine(ChangeEnvironmentLighting());
            }
        }       
    }


    void StopBlinking()
    {
        isChangingIntensity = false;
        StopCoroutine(ChangeEnvironmentLighting());
        RenderSettings.ambientIntensity = 1f;
    }

    void SetRandomDestination()
    {
        agent.speed = normalSpeed;
        Vector3 randomPosition = RandomNavSphere(transform.position, 50f, -1);
        agent.SetDestination(randomPosition);
    }

    IEnumerator ChangeEnvironmentLighting()
    {
        while (isChangingIntensity)
        {
            RenderSettings.ambientIntensity = 4f;
            yield return new WaitForSeconds(0.5f);
            RenderSettings.ambientIntensity = 0f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, int areaMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, areaMask);
        return navHit.position;
    }
}