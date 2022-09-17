using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI lifeText;
    public TMPro.TextMeshProUGUI auxiliarText;
    public GameObject key;
    public bool getKey = false;

    public float speed;
    public float gravity;
    public float rotSpeed;
    public float jumpVelocity;
    public float jumpHeight;
    public float YRightRotation;
    public float YLeftRotation;

    private int score = 0;
    private float rotation;
    private Vector3 moveDirection;
    private Vector3 lastPosition;

    public int _lives = 3;
    [SerializeField] private MenuGameOverController _menuGameOverController;
    [SerializeField] private MenuVictoryController _menuVictoryController;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetInteger("transition", 0);
        lifeText.text = _lives.ToString();
        scoreText.text = score.ToString();
        key.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null)
        {
            Move();
            
            if (_lives <= 0)
            {
                gameObject.SetActive(false);
                //Destroy(gameObject);
                _menuGameOverController.Setup(score);
            }
            
            // Caiu
            if(controller.transform.position.y < -10)
            {
                Debug.Log("Caímos");
                UpdatePlayerLives(-1);
                SetPlayerPosition(new Vector3(24.23f, 13.83f, 0f)); //voltando para o início
                //SetPlayerPosition(lastPosition);
            }
        }
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

    private void SetPlayerPosition(Vector3 position)
    {
        controller.transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            //animator.SetInteger("transition", 3);
            UpdatePlayerLives(-1);
            Debug.Log("Colisão com spikes. Vidas - 1");
        }

        if (other.gameObject.CompareTag("Enemy2Legs"))
        {
            UpdatePlayerLives(-1);
            Debug.Log("Colisão com Enemy2Legs. Vidas - 1");
        }

        if (other.gameObject.CompareTag("EnemyHumanoide"))
        {
            UpdatePlayerLives(-1);
            Debug.Log("Colisão com EnemyHumanoide. Vidas - 1");
        }
        
        if (other.gameObject.CompareTag("Projectile1"))
        {
            UpdatePlayerLives(-1);
            Debug.Log("Colisão com projétil. Vidas - 1");
        }

        if (other.gameObject.CompareTag("Gear"))
        {
            UpdatePlayerScore(1);
        }

        if (other.gameObject.CompareTag("Key"))
        {
            getKey = true;
            key.SetActive(true);
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            if(getKey)
            {
                _menuVictoryController.Setup(score);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
            else
            {
                auxiliarText.text = "Vc ainda n tem a chave para o portal!";
            }
            
        }
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            lastPosition = controller.transform.position;
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
        if(_lives >= 0)
        {
            lifeText.text = _lives.ToString();
        }
    }
}
