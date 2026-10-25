using Unity.VisualScripting;
using UnityEngine;

public class DoorCloseScript : MonoBehaviour
{
    [SerializeField] private GameObject door;
    
    [SerializeField] private Vector3 openDoorPosition;
    private bool hasClosed = false;
    
    void Start()
    {
        
        
    }
   /* public void Close()
    {

        openDoorPosition = door.transform.position;
        door.transform.position = openDoorPosition-new Vector3(0,3,0);
        hasClosed = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && hasClosed == false)
        {
            Close();
            gameObject.SetActive(false);
        }
    }


    */
}
