using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CompanionFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2.5f;  
    public float catchUpDistance = 8f;   
    public float walkSpeed = 3.5f;
    public float runSpeed = 6f;
    public Animator animator;          

    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = followDistance;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        agent.speed = dist > catchUpDistance ? runSpeed : walkSpeed;

        if (dist > followDistance)
            agent.SetDestination(player.position);
        else
            agent.ResetPath();

        if (animator)
            animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}