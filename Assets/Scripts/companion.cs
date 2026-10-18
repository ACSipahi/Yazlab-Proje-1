using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CompanionFollow : MonoBehaviour
{
    private Vector3 currentGoal;
    [SerializeField]private LayerMask layerMask;
    [SerializeField]private List<GameObject> currentHitObjects = new List<GameObject>();
    [SerializeField] public Transform player;
    [SerializeField] public float followDistance = 2.5f;
    [SerializeField] public float catchUpDistance = 8f;
    [SerializeField] public float walkSpeed = 3.5f;
    [SerializeField] public float runSpeed = 6f;
    [SerializeField] public Animator animator;
    private int RaycastCoolDown;
    NavMeshAgent agent;
    private bool isProgressing = false;
    private Vector3 progressPosition;
    [SerializeField] private GameObject progressionObject;
    private float currentHitDistance;
    private GameObject foundObject;
    [SerializeField] private float maxDistance=5;
    [SerializeField] private float radiusOfSearch = 5;
    private int searchTimes = 0;
    private bool searchStart = false;
    void Awake()
    {
        currentHitDistance = maxDistance;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = followDistance;
    }

    public void Searching(bool isProgressing,List<GameObject> list) 
    { if (isProgressing == true) 
        { for (int j = 10; j == 0; j--)
            {
                transform.Rotate(0, 0.1f, 0, Space.Self);
                if (list.Contains(progressionObject))
                { searchTimes = 0; break; }
                
            }
            if (list.Contains(progressionObject) == false &&  searchTimes == 10) 
            { 
              isProgressing = false;
                searchTimes = 0;            
            }
        }
    
    
    }
    private void FixedUpdate()
    {
        Searching(isProgressing, currentHitObjects);
        Debug.Log(progressionObject);
        currentHitDistance = maxDistance;
        currentHitObjects.Clear();
        RaycastHit[] hits=Physics.SphereCastAll(transform.position+new Vector3(0,1,0), radiusOfSearch, transform.forward, maxDistance, layerMask);
        foreach (RaycastHit hit in hits)
        {
            
            currentHitObjects.Add(hit.transform.gameObject);
            
            currentHitDistance = hit.distance;
            for (int i = 0; i < currentHitObjects.Count; i = i + 1) 
            {
                if (currentHitObjects[i].name.Contains("ProgressObject"))
                {
                    
                   
                    progressPosition = currentHitObjects[i].transform.position;
                    if (progressPosition != null)
                    {
                        currentGoal = progressPosition;
                    }
                    
                    isProgressing = true;
                    currentHitObjects[i].SetActive(false);
                    Debug.Log(isProgressing);
                    
                    
                }
                

            }
        }
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position+new Vector3(0,1,0), transform.position + transform.forward * currentHitDistance);
        Gizmos.DrawWireSphere((transform.position+ new Vector3(0, 1, 0)) + transform.forward * currentHitDistance, radiusOfSearch);

    }
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        
        agent.speed = dist > catchUpDistance ? runSpeed : walkSpeed;

        if (isProgressing)
        { 
            
                agent.SetDestination(currentGoal);
            
        }
        else if (dist > followDistance)
        {
            agent.SetDestination(player.position - new Vector3(1.25f, 0, 1.25f));
        }
        else
            agent.ResetPath();

        if (animator)
            animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}