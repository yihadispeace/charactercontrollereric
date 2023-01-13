using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animatio;
    public Transform cam;
    public Transform LookAtTransform;

   
    public float speed = 5;
    public float jumpHeight = 1;
    public float gravity = -9.81f;

    public bool isGrounded;
    public Transform groundSensor;
    public float sensorRadius = 0.1f;
    public LayerMask ground;
    private Vector3 playerVelocity;

    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;


    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;    

 void Start()
    {
      
        controller = GetComponent<CharacterController>();
        animatio = GetComponentInChildren<Animator>();
        

        Cursor.lockState = CursorLockMode.Locked;
	
    }

 void Update()
    {
                
        Movement();
        Jump(); 

    }

void Movement()
    {
        
       
        float z = Input.GetAxisRaw("Vertical");        
        float x = Input.GetAxisRaw("Horizontal");   
        animatio.SetFloat("VelX", x);
        animatio.SetFloat("VelZ", z);

        
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        
        if(move != Vector3.zero)
        {
           
            
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;           
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);           
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;            
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
       }

void Jump()
    {
        
        
       
        isGrounded = Physics.CheckSphere(groundSensor.position, sensorRadius, ground);
        animatio.SetBool("Jump", !isGrounded);     

        
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

      
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
          
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); 
        }

        
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

}
