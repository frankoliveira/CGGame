using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Legs : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float speed;
    public float gravity;
    private float rotation = -90;
    public float rightWalkDistance;
    public float lefttWalkDistance;
    public bool directionRight = true;

    private float xPositionPlayer;
    private float originalOrientation = 90f;
    private float originalZPosition;
    private float leftLimiteX;
    private float rightLimiteX;
    private Vector3 moveDiection;


    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        var rb = GetComponent<Rigidbody>();
        leftLimiteX = controller.transform.position.x - lefttWalkDistance;
        rightLimiteX = controller.transform.position.x + rightWalkDistance;
        originalZPosition = controller.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if(directionRight)
        {
            transform.eulerAngles = new Vector3(0, originalOrientation, 0);
            moveDiection = Vector3.right * speed;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, rotation, 0);
            moveDiection = Vector3.left * speed;
            
        }

        xPositionPlayer = controller.transform.position.x;

        if (xPositionPlayer >= rightLimiteX)
        {
            directionRight = false;
        }
        else if (xPositionPlayer <= leftLimiteX)
        {
            directionRight = true;
        }

        if (controller.transform.position.y > 0)
        {
            moveDiection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDiection * Time.deltaTime);
    }
}
