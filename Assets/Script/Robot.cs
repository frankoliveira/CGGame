using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float speed;
    public float gravity;
    public float rotSpeed;
    private float rotation = 90f;
    public float jumpVelocity;
    public float jumpHeight;

    private Vector3 moveDiection;
   
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpVelocity = jumpHeight;
                animator.SetInteger("transition", 1);
            }

            //walk
            if (Input.GetKey(KeyCode.W))
            {
                moveDiection = Vector3.forward * speed;
                animator.SetInteger("transition", 1);

            }
            
            if (Input.GetKeyUp(KeyCode.W))
            {
                moveDiection = Vector3.zero;
                animator.SetInteger("transition", 0);
            }


            if (Input.GetKeyDown(KeyCode.M))
            {
                animator.SetInteger("transition", 9);
            }

            if (Input.GetKeyUp(KeyCode.M))
            {
                animator.SetInteger("transition", 0);
            }
        }
        else
        {
            //walk
            if (Input.GetKey(KeyCode.W))
            {
                moveDiection = Vector3.forward * 1.5f *  speed;
                animator.SetInteger("transition", 1);

            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                moveDiection = Vector3.zero;
                animator.SetInteger("transition", 0);
            }

            jumpVelocity -= gravity * Time.deltaTime;
        }

        //rotation += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

        if (Input.GetAxis("Horizontal") > 0)
        {
            rotation = 90f;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rotation = 270f;
        }

        transform.eulerAngles = new Vector3(0, rotation, 0);

        moveDiection.y = jumpVelocity;
        moveDiection = transform.TransformDirection(moveDiection);
        controller.Move(moveDiection * Time.deltaTime);

        /*
        if (controller.isGrounded)
        {
            //walk
            if (Input.GetKey(KeyCode.W))
            {
                //moveDiection = Vector3.forward * speed;
                animator.SetInteger("transition", 1);

            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                //moveDiection = Vector3.zero;

                //idle
                animator.SetInteger("transition", 0);
            }
            
            //jump
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetInteger("transition", 3);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                animator.SetInteger("transition", 0);
            }


            //Run
            if (Input.GetKey(KeyCode.O))
            {
                animator.SetInteger("transition", 5);
                //moveDiection = Vector3.forward * 2f * speed ;
            }
            else if (Input.GetKeyUp(KeyCode.O))
            {
                animator.SetInteger("transition", 0);
                moveDiection = Vector3.zero;
            }
            

        }

        //rot é float
        //Direção
        rotation += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);

        //moveDiection é vector3
        //gravidade puxando pra baixo
        moveDiection.y -= gravity * Time.deltaTime;
        moveDiection = transform.TransformDirection(moveDiection);
        controller.Move(moveDiection * Time.deltaTime);
        */
    }
}
