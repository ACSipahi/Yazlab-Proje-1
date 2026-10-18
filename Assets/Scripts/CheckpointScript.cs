using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckpointScript : MonoBehaviour
{
    
    private InputAction restartButton;
    
    void Start()
    {
        restartButton = InputSystem.actions.FindAction("Restart");
         GlobalValues.currentCheckPoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        
            
            
        if (other.gameObject.name == "CheckPoint")
        {
            GlobalValues.currentCheckPoint = other.GetComponent<BoxCollider>().transform.localPosition;
        other.gameObject.SetActive(false);
        }
        
    }
   

}
