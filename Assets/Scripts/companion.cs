using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CompanionFollow : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public float followDistance = 2.5f;
    [SerializeField] public float catchUpDistance = 8f;
    [SerializeField] public float walkSpeed = 3.5f;
    [SerializeField] public float runSpeed = 6f;
    [SerializeField] public Animator animator;          

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
            agent.SetDestination(player.position-new Vector3(1.25f,0,1.25f));
        else
            agent.ResetPath();

        if (animator)
            animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}