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
    [SerializeField] public float followDistance = 3f;
    [SerializeField] private float catchUpDistance = 7f;
    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController playerController;

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
        if (!animator) animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = followDistance;

        if (!playerController && player)
            playerController = player.GetComponent<CharacterController>();
    }

    // DoorZone buradan cagirir: en yakin basilmamis butonu bulur ve gider.
    // Takip durumunda olmasa da calisir (onceki gorevi birakir).
    public void FindAndPress(float radius)
    {
        if (!enabled) enabled = true;
        if (!EnsureOnNavMesh()) return;

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
            Debug.LogWarning("[Companion] Buton bulunamadi (CompanionButton yok / hepsi basilmis / yaricap kucuk)");
            return;
        }

        GoPress(best);
    }

    public void FindAndPress() { FindAndPress(searchRadius); }

    // Eski SearchTrigger scripti derlenmeye devam etsin diye birakildi
    public void StartSearch(Bounds area) { FindAndPress(Mathf.Max(searchRadius, area.extents.magnitude)); }
    public void StartSearch() { FindAndPress(searchRadius); }

    // NavMesh'te degilse en yakin NavMesh noktasina isinlar (sessiz)
    bool TryPlaceOnNavMesh()
    {
        if (agent.isOnNavMesh) return true;

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
            return agent.isOnNavMesh;
        }
        return false;
    }

    bool EnsureOnNavMesh()
    {
        if (TryPlaceOnNavMesh()) return true;

        Debug.LogWarning("[Companion] NavMesh'te degil ve yakinda NavMesh yok. Companion'i mavi alana tasi / yeniden bake et ve sahneyi kaydet");
        return false;
    }

    void GoPress(CompanionButton btn)
    {
        Vector3 p = btn.standPoint ? btn.standPoint.position : btn.transform.position;
        if (NavMesh.SamplePosition(p, out NavMeshHit nav, 3f, NavMesh.AllAreas))
            p = nav.position;

        agent.ResetPath();            // varsa onceki hedefi birak
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

        // NavMesh'te degilse agent komutlari hata verir, once yerlestirmeyi dene
        if (!TryPlaceOnNavMesh()) return;

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

        bool playerGrounded = !playerController || playerController.isGrounded;

        if (dist > followDistance && playerGrounded)
        {
            agent.SetDestination(player.position - new Vector3(0.25f, 0f, 0.25f));
            if (animator) animator.SetBool("Running", true);
        }
        else
        {
            agent.velocity = Vector3.zero;
            agent.ResetPath();
            if (animator) animator.SetBool("Running", false);
        }
    }

    void GoStep()
    {
        agent.speed = walkSpeed;
        agent.SetDestination(goal);
        if (animator) animator.SetBool("Running", true);
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