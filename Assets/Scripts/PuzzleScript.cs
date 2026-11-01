using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PuzzleScript : MonoBehaviour
{
   
    [SerializeField]private float[] solution = new float[3];
    private int buttonIndex1 = 0;
    private int buttonIndex2 = 0;
    private int buttonIndex3 = 0;
    
    [SerializeField] private GameObject door;
    private Vector3 doorClosedPosition;
    [SerializeField]private float[] playerAnswers = new float[3];
    private bool button1Colliding = false;
    private bool button2Colliding = false;
    private bool button3Colliding = false;
    private bool buttonpressed = false;
    
    void Start()
    {
        solution[0] = Random.Range(0,3);
        solution[1] = Random.Range(0,3);
        solution[2] = Random.Range(0,3);
        doorClosedPosition = door.transform.position;
        
        
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PuzzleButton1")
        {
            button1Colliding = false;
        }
        if (other.gameObject.name == "PuzzleButton2")
        {
            button2Colliding = false;
        }
        if (other.gameObject.name == "PuzzleButton3")
        {
            button3Colliding = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name == "PuzzleButton1")
            {
            button1Colliding = true;
            }
       
        if (other.gameObject.name == "PuzzleButton2")
            {
            button2Colliding = true;
            }
        if (other.gameObject.name == "PuzzleButton3")
            {
                button3Colliding =true;
            }
    }
        
    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && button1Colliding)
        {
            
            if (buttonIndex1 == 2)
            {
                buttonIndex1 = 0;
                playerAnswers[0] = buttonIndex1;
                
            }
            else
            {
                buttonIndex1++;
                playerAnswers[0] = buttonIndex1;
            }
            Debug.Log(buttonIndex1);
        }
        if (Keyboard.current.eKey.wasPressedThisFrame && button2Colliding)
        {
            
            if (buttonIndex2 == 2)
            {
                buttonIndex2 = 0;
                playerAnswers[1] = buttonIndex2;
                
            }
            else
            {
                buttonIndex2++;
                playerAnswers[1] = buttonIndex2;
            }
            Debug.Log(buttonIndex2);
        }
        if(Keyboard.current.eKey.wasPressedThisFrame && button3Colliding) 
        {
            
            if (buttonIndex3 == 2)
            {
                buttonIndex3 = 0;
                playerAnswers[2] = buttonIndex3;
                Debug.Log(buttonIndex3);
            }
            else
            {
                buttonIndex3++;
                playerAnswers[2] = buttonIndex3;
            }
            Debug.Log(buttonIndex3);
        }

        if (playerAnswers[0] == solution[0] && playerAnswers[1] == solution[1] && playerAnswers[2] == solution[2])
        {
            door.transform.position = Vector3.MoveTowards(doorClosedPosition, doorClosedPosition + Vector3.up * 2.5f, 2.5f);
            Debug.Log("Puzzle çözüldü");
        }
        
    }
}



