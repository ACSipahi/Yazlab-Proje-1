using System;
using Unity.VisualScripting;
using UnityEngine;

public class TrapDoorScript : MonoBehaviour
{
 
    [SerializeField] private GameObject door;
    private Vector3 openDoorPosition;
    private BoxCollider trapHitBox;
    void Start()
    {
        openDoorPosition = door.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    { 
            if (other.CompareTag("Player"))
            {
                door.transform.position = Vector3.MoveTowards(openDoorPosition, openDoorPosition + new Vector3(0, -2.762f, 0), 2);
                gameObject.SetActive(false);
            }
    }
   }
            
            
            
            
        
    
        
        
        

