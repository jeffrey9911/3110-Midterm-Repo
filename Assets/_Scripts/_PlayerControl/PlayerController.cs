using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    // Player movement
    public PlayerAction inputAction;
    Vector2 move;
    Vector2 rotate;
    private float walkSpeed = 5.0f;
    public Camera playerCamera;
    Vector3 cameraRotation;
    
    // Player jump
    Rigidbody rb;
    private float distanceGround;
    private bool isGrounded = true;
    public float jump = 5.0f;

    public float distanceToGround;

    // Player animation
    Animator playerAnimator;
    private bool isWalking = false;

    // Projectile bullets
    public GameObject bullet;
    public Transform projectilePos;
    /*
    private void OnEnable()
    {
        inputAction.Player.Enable();
    }

    private void OnDisable()
    {
        inputAction.Player.Disable();
    }*/

    void Start()
    {
        if(!instance)
        {
            instance = this;
        }

        //inputAction = new PlayerAction();
        // Using controller from PlayerInputController
        inputAction = PlayerInputController.controller.inputAction;


        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        inputAction.Player.Jump.performed += cntxt => Jump();
        inputAction.Player.Shoot.performed += cntxt => Shoot();


        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        distanceGround = GetComponent<Collider>().bounds.extents.y;
    }

    public void Shoot()
    {
        if(!EditorManager.Instance.editorMode)
        {
            Rigidbody bulletRb = Instantiate(bullet, projectilePos.position, Quaternion.identity).GetComponent<Rigidbody>();

            bulletRb.AddForce(transform.forward * 32.0f, ForceMode.Impulse);

            bulletRb.AddForce(transform.up * 5.0f, ForceMode.Impulse);
        }
    }
    

    private void Jump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * move.y * Time.deltaTime * walkSpeed, Space.Self);
        transform.Translate(Vector3.right * move.x * Time.deltaTime * walkSpeed, Space.Self);

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);
    }
}
