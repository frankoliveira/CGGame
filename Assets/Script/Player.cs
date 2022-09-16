using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI lifeText;

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

    public int _lives = 3;
    [SerializeField] private MenuGameOverController _menuGameOverController;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetInteger("transition", 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
        if (_lives == 0)
        {
            Destroy(gameObject);
            _menuGameOverController.Setup(score, _lives);
            //_vidas = -1;
        }
        
        //Debug.Log(controller.height);
        /* Estava tentando pegar a altura corrente do jogador para, se ela for menor do q 0, decrementar a vida,
        pois ele caiu da plataforma, mas esse controller.height não muda conforme ele vai andando no jogo, então
        acho q n é assim q pegamos o y atual */
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            animator.SetInteger("transition", 2);
            //Destroy(gameObject);
            lifeText.text = _vidas.ToString();
        }
        if (collision.gameObject.CompareTag("Enemy2Legs"))
        {
            animator.SetInteger("transition", 2);
            //Destroy(gameObject);
            lifeText.text = _vidas.ToString();
        }
        if (collision.gameObject.CompareTag("Gear"))
        {
            score++;
            scoreText.text = score.ToString();
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            animator.SetInteger("transition", 3);
            UpdatePlayerLives(-1);
            //_lives -= 1;
            Debug.Log("Colisão com spikes. Vidas - 1");
            lifeText.text = _lives.ToString();
        }

        if (other.gameObject.CompareTag("Enemy2Legs"))
        {
            animator.SetInteger("transition", 3);
            UpdatePlayerLives(-1);
            //_lives -= 1;
            Debug.Log("Colisão com Enemy2Legs. Vidas - 1");
            lifeText.text = _lives.ToString();
        }

        if (other.gameObject.CompareTag("Gear"))
        {
            UpdatePlayerScore(1);
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

    private void UpdatePlayerScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    private void UpdatePlayerLives(int live)
    {
        _lives += live;
    }
}
