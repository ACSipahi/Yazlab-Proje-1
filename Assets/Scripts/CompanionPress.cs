using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CompanionPress : MonoBehaviour
{
    public float playerDetectRadius = 3f;  // oyuncu butona bu kadar yaklaşınca tetiklenir
    public LayerMask buttonLayer;          // butonlara "Button" layer'ı ver
    public CompanionFollow follow;
    public Animator animator;              // opsiyonel

    NavMeshAgent agent;
    bool busy;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!follow) follow = GetComponent<CompanionFollow>();
    }

    void Update()
    {
        if (busy || !follow || !follow.player) return;

        foreach (var hit in Physics.OverlapSphere(follow.player.position, playerDetectRadius, buttonLayer))
        {
            var b = hit.GetComponentInParent<CompanionButton>();
            if (b && !b.pressed)
            {
                StartCoroutine(PressRoutine(b));
                break;
            }
        }
    }

    IEnumerator PressRoutine(CompanionButton b)
    {
        busy = true;
        follow.enabled = false;

        Vector3 target = b.standPoint ? b.standPoint.position : b.transform.position;
        agent.stoppingDistance = 0.3f;
        agent.SetDestination(target);

        float timeout = 10f;
        while (timeout > 0f && (agent.pathPending || agent.remainingDistance > agent.stoppingDistance + 0.1f))
        {
            timeout -= Time.deltaTime;
            yield return null;
        }

        if (animator) animator.SetTrigger("Press");
        yield return new WaitForSeconds(0.5f);
        b.Press();
        yield return new WaitForSeconds(0.5f);

        agent.stoppingDistance = follow.followDistance;
        follow.enabled = true;
        busy = false;
    }
}