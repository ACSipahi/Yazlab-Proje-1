using UnityEngine;

public class Door : MonoBehaviour
{
    public float openHeight = 3f;
    public float speed = 2f;
    bool open;
    Vector3 closedPos, openPos;

    void Start() { closedPos = transform.position; openPos = closedPos + Vector3.up * openHeight; }
    public void Open() { open = true; }
    void Update()
    {


        transform.position = Vector3.MoveTowards(transform.position, open ? openPos : closedPos, speed * Time.deltaTime);


    }
}