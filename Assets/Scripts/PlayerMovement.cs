using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float runSpeed = 10;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float gravity = 10;
    [SerializeField] private float lookSens = 0.2f;
    [SerializeField] private float lookLimitAngle = 90f;
    [SerializeField] private float crouchSpeed = 3;

    private Camera mainCam;
    private InputAction crouchInput;

    private CharacterController characterController;
    private float currentSpeed = 0f;

    private Vector3 moveDirection = Vector3.zero;
    private float lookAngle = 0.0f;

    private InputAction moveInput;
    private InputAction jumpInput;

    private bool jumped = false;
    private InputAction runInput;

    Vector3 oldCamPosition;
    private bool isCrouching = false;
    private void Start()
    {

        mainCam = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentSpeed = walkSpeed;
        moveInput = InputSystem.actions.FindAction("Move");
        runInput = InputSystem.actions.FindAction("Run");
        jumpInput = InputSystem.actions.FindAction("Jump");
        jumpInput.started += Jumped;
        crouchInput = InputSystem.actions.FindAction("Crouch");

    }


    private void MovementHandler(Vector2 walkInput)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float oldY = moveDirection.y;

        Vector2 newSpeed = new Vector2(walkInput.y * currentSpeed, walkInput.x * currentSpeed);
        moveDirection = (forward * newSpeed.x) + (right * newSpeed.y);

        if (jumped && characterController.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y = oldY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;

        }
        characterController.Move(moveDirection * Time.deltaTime);
    }
    private void Update()
    {


        isCrouching = false;
        currentSpeed = walkSpeed;
        Vector2 walkInput = moveInput.ReadValue<Vector2>();
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());

        if (crouchInput.IsPressed())
        {
            isCrouching = true;
            currentSpeed = crouchSpeed;
        }

        if (!characterController.isGrounded)
        {
            jumped = false;
        }
        if (runInput.IsPressed())
        {
            currentSpeed = runSpeed;
        }
        MovementHandler(walkInput);
        CameraHandler(mouseDelta);
        Crouching(isCrouching);
    }
    private void Jumped(InputAction.CallbackContext _)
    { 
            jumped = true; 
    }
        
    private void CameraHandler(Vector2 mouseDelta)
    {
        lookAngle += -mouseDelta.y * lookSens;
        lookAngle = Mathf.Clamp(lookAngle, -lookLimitAngle, lookLimitAngle);
        mainCam.transform.localRotation = Quaternion.Euler(lookAngle, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * lookSens, 0);
    }
    private void Crouching(bool isCrouching)
    {
        

        if (isCrouching)
        {
            
            
        }
       
    }
}
