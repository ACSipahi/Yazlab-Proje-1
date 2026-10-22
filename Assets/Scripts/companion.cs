using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class CompanionFollow : MonoBehaviour
{
    enum State { Follow, Go }

    [Header("Takip")]
    [SerializeField] public Transform player;
    [SerializeField] public float followDistance = 2.5f;
    [SerializeField] public float catchUpDistance = 8f;
    [SerializeField] public float walkSpeed = 3.5f;
    [SerializeField] public float runSpeed = 6f;
    [SerializeField] public Animator animator;

    [Header("Buton arama")]
    [SerializeField] private float goTimeout = 15f;
    [SerializeField] private float searchRadius = 30f;
    [SerializeField] private bool useTestKey = true;   // F tusu ile test

    NavMeshAgent agent;
    State state = State.Follow;
    CompanionButton target;
    Vector3 goal;
    float goTimer;
    readonly HashSet<CompanionButton> pressed = new HashSet<CompanionButton>();

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = followDistance;
    }

    // DoorZone buradan cagirir: en yakin basilmamis butonu bulur ve gider
    public void FindAndPress(float radius)
    {
        if (state != State.Follow) return;

        CompanionButton best = null;
        float bestDist = radius;

        foreach (var b in FindObjectsByType<CompanionButton>(FindObjectsSortMode.None))
        {
            if (b.pressed || pressed.Contains(b)) continue;
            float d = Vector3.Distance(transform.position, b.transform.position);
            if (d < bestDist) { bestDist = d; best = b; }
        }

        if (best == null)
        {
            Debug.LogWarning("[Companion] Buton bulunamadi (CompanionButton yok / yaricap kucuk / zaten basilmis)");
            return;
        }

        GoPress(best);
    }

    public void FindAndPress() { FindAndPress(searchRadius); }

    // Eski SearchTrigger scripti derlenmeye devam etsin diye birakildi
    public void StartSearch(Bounds area) { FindAndPress(Mathf.Max(searchRadius, area.extents.magnitude)); }
    public void StartSearch() { FindAndPress(searchRadius); }

    void GoPress(CompanionButton btn)
    {
        Vector3 p = btn.standPoint ? btn.standPoint.position : btn.transform.position;
        if (NavMesh.SamplePosition(p, out NavMeshHit nav, 3f, NavMesh.AllAreas))
            p = nav.position;

        target = btn;
        goal = p;
        goTimer = goTimeout;
        agent.stoppingDistance = 0.3f;
        state = State.Go;
        Debug.Log("[Companion] Butona gidiyor: " + btn.name);
    }

    void Update()
    {
        if (useTestKey && Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
            FindAndPress(searchRadius);

        switch (state)
        {
            case State.Follow: FollowPlayer(); break;
            case State.Go: GoStep(); break;
        }

        if (animator)
            animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void FollowPlayer()
    {
        if (!player) return;

        float dist = Vector3.Distance(transform.position, player.position);
        agent.speed = dist > catchUpDistance ? runSpeed : walkSpeed;

        if (dist > followDistance)
            agent.SetDestination(player.position - new Vector3(1.25f, 0, 1.25f));
        else
            agent.ResetPath();
    }

    void GoStep()
    {
        agent.speed = walkSpeed;
        agent.SetDestination(goal);
        goTimer -= Time.deltaTime;

        Vector3 d = goal - transform.position;
        d.y = 0f;
        bool arrived = d.magnitude < 0.8f ||
                       (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f);

        if (arrived || goTimer <= 0f)
        {
            if (arrived && target)
            {
                pressed.Add(target);
                target.Press();
            }
            else
            {
                Debug.LogWarning("[Companion] Butona ulasilamadi (NavMesh / standPoint kontrol et)");
            }

            target = null;
            agent.stoppingDistance = followDistance;
            state = State.Follow;
        }
    }
}