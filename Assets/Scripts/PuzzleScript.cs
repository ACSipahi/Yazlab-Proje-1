using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PuzzleScript : MonoBehaviour
{
    private bool button1Solved = false;
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
    private GameObject button1;
    private GameObject button2;
    private GameObject button3;
    private bool button3Solved;
    private bool button2Solved;
    void Start()
    {
        solution[0] = Random.Range(1,4);
        solution[1] = Random.Range(1,4);
        solution[2] = Random.Range(1,4);
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
            button1 = other.gameObject;
            
            }
       
        if (other.gameObject.name == "PuzzleButton2")
            {
            button2Colliding = true;
            button2 = other.gameObject;
            }
        if (other.gameObject.name == "PuzzleButton3")
            {
                button3Colliding =true;
            button3 = other.gameObject;
            }
    }
        
    private void Update()
    {
        if (playerAnswers[0] == solution[0] && button1Solved != true)
        {
            button1.gameObject.transform.position =
            Vector3.MoveTowards(button1.gameObject.transform.position, button1.gameObject.transform.position - Vector3.back * 0.1f, 0.1f);
            button1Solved = true;
        }
        else
        {
            if (Keyboard.current.eKey.wasPressedThisFrame && button1Colliding && button1Solved != true)
            {

                {
                    if (buttonIndex1 == 3)
                    {
                        buttonIndex1 = 1;
                        playerAnswers[0] = buttonIndex1;

                    }
                    else
                    {
                        buttonIndex1++;
                        playerAnswers[0] = buttonIndex1;
                    }
                }
                Debug.Log(buttonIndex1);
            }
        }
        if (playerAnswers[1] == solution[1] && button2Solved != true)
        {
            button2.gameObject.transform.position =
            Vector3.MoveTowards(button2.gameObject.transform.position, button2.gameObject.transform.position - Vector3.back * 0.1f, 0.1f);
            button2Solved = true;
        }
        else
        {
            if (Keyboard.current.eKey.wasPressedThisFrame && button2Colliding && button2Solved != true)
            {

                if (buttonIndex2 == 3)
                {
                    buttonIndex2 = 1;
                    playerAnswers[1] = buttonIndex2;

                }
                else
                {
                    buttonIndex2++;
                    playerAnswers[1] = buttonIndex2;
                }
                Debug.Log(buttonIndex2);
            }
        }
        if (playerAnswers[2] == solution[2] && button3Solved != true)
        {
            button3.gameObject.transform.position =
            Vector3.MoveTowards(button3.gameObject.transform.position, button3.gameObject.transform.position - Vector3.back * 0.1f, 0.1f);
            button3Solved = true;
        }
        else
        {
            if (Keyboard.current.eKey.wasPressedThisFrame && button3Colliding && button3Solved != true)
            {

                if (buttonIndex3 == 3)
                {
                    buttonIndex3 = 1;
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
        }
        if (playerAnswers[0] == solution[0] && playerAnswers[1] == solution[1] && playerAnswers[2] == solution[2])
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorClosedPosition + Vector3.up * 2.5f, 2f*Time.deltaTime);
            Debug.Log("Puzzle çözüldü");
        }
        
    }
}



