using UnityEngine;
using UnityEngine.InputSystem;

public class KillPlane : MonoBehaviour
{
    private GameObject playerObject;
    private InputAction restartButton;
    
    void Start()
    {
        restartButton = InputSystem.actions.FindAction("Restart");
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider collision)
    {
        
        Debug.Log(GlobalValues.currentCheckPoint);
        playerObject = collision.gameObject;
        if (collision.gameObject.name == "Player")
        {
            
            playerObject.transform.position = GlobalValues.currentCheckPoint;
            GlobalValues.isPlayerDead = true;
            playerObject.gameObject.SetActive(false);

        }
    }


    void Update()
    {
        if (restartButton.IsPressed() && GlobalValues.isPlayerDead)
        { 
            playerObject.transform.gameObject.SetActive(true);
            GlobalValues.isPlayerDead = false;
            
            
        }
    }
}
