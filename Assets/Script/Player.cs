using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public TMPro.TextMeshProUGUI scoreText;

    public float speed;
    public float gravity;
    public float rotSpeed;
    public float jumpVelocity;
    public float jumpHeight;
    public float YRightRotation;
    public float YLeftRotation;
    
    private int score;
    private float rotation;
    private Vector3 moveDirection;

    private int _vidas = 1;
    [SerializeField] private MenuGameOverController _menuGameOverController;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetInteger("transition", 0);
        if(_menuGameOverController != null)
        {
            Debug.Log("menu game ok");
        }
        else
        {
            Debug.Log("menu game é null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (_vidas == 0)
        {
            _menuGameOverController.Setup(score);
            _vidas = -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            animator.SetInteger("transition", 2);
            //Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy2Legs"))
        {
            animator.SetInteger("transition", 2);
            //Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Gear"))
        {
            score++;
            scoreText.text = score.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            animator.SetInteger("transition", 3);
            //Destroy(gameObject);
            _vidas -= 1;
        }

        if (other.gameObject.CompareTag("Enemy2Legs"))
        {
            animator.SetInteger("transition", 3);
            Destroy(gameObject);
            _vidas -= 1;
        }

        if (other.gameObject.CompareTag("Gear"))
        {
            score++;
            scoreText.text = score.ToString();
        }

    }

    void Move()
    {
        if (controller.isGrounded)
        {
            animator.SetInteger("transition", 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetInteger("transition", 3);
                jumpVelocity = jumpHeight;
            }

            //walk right
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetInteger("transition", 1);
                moveDirection = Vector3.forward * speed;

            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetInteger("transition", 0);
                moveDirection = Vector3.zero;
            }

            //walk left
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetInteger("transition", 1);
                moveDirection = Vector3.forward * speed;

            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetInteger("transition", 0);
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            //walk right
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetInteger("transition", 1);
                moveDirection = Vector3.forward * 1.5f * speed;
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetInteger("transition", 0);
                moveDirection = Vector3.zero;
            }

            //walk left
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetInteger("transition", 1);
                moveDirection = Vector3.forward * speed;
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetInteger("transition", 0);
                moveDirection = Vector3.zero;
            }

            jumpVelocity -= gravity * Time.deltaTime;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            rotation = YRightRotation;
            transform.eulerAngles = new Vector3(0, rotation, 0);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rotation = YLeftRotation;
            transform.eulerAngles = new Vector3(0, rotation, 0);
        }

        //transform.eulerAngles = new Vector3(0, rotation, 0);

        moveDirection.y = jumpVelocity;
        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
