using System;
using Unity.VisualScripting;
using UnityEngine;

public class TrapDoorScript : MonoBehaviour
{
    private bool playerHasEntered = false;
    private float speed = 2f;
    [SerializeField] private GameObject trapDark;
    private Vector3 openDoorPosition;
    private BoxCollider trapHitBox;
    void Start()
    {
        openDoorPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    { 
            if (other.CompareTag("Player"))
            {
                playerHasEntered = true;
                
            }
    }
    private void Update()
    {if (playerHasEntered)
        {
            transform.position = Vector3.MoveTowards(transform.position, openDoorPosition + Vector3.down * 3, speed*Time.deltaTime);
            trapDark.SetActive(false);
        }
    }
}
  
            
            
            
        
    
        
        
        

