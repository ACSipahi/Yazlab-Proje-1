using Unity.VisualScripting;
using UnityEngine;

public class CeilingTrapScript : MonoBehaviour
{
    [SerializeField] private GameObject trap;
    private bool trapTriggered = false;
    private Vector3 trapsStartingPosition;
    private float trapTriggerSpeed = 20f;
    private float trapReloadSpeed = 1f;
    private void Start()
    {
        trapsStartingPosition = trap.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        trapTriggered = true;
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        trapTriggered = false;
    }
    void Update()
    { if (trapTriggered)
        {
            trap.transform.position =
            Vector3.MoveTowards(trap.transform.position, trapsStartingPosition + Vector3.down * 2.161f, trapTriggerSpeed * Time.deltaTime);
        }
     if(trapTriggered == false || GlobalValues.isPlayerDead == true)
        {
            trap.transform.position =
            Vector3.MoveTowards(trap.transform.position, trapsStartingPosition, trapReloadSpeed * Time.deltaTime);
            trapTriggered = false;
        }   
    }
}
