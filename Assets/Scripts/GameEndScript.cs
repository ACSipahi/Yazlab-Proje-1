using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameEndScript : MonoBehaviour
{
    private bool isPlayerNear = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            isPlayerNear = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            isPlayerNear = false;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Keyboard.current.eKey.wasPressedThisFrame) 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
