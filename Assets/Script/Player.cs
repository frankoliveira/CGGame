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
    public GameObject keyText;
    public GameObject keyObject;
    public bool getKey = false;

    public float auxiliarTextTime = 1.5f;
    public bool auxiliarTextDisplayed = false;
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

    //Shooting properties
    //private Transform _playerTransform;
    private GunPlayerController _currentGun;
    private float _fireRate;
    private float _fireRateDelta = 0f;
    //[SerializeField] float _playerRange = 13.0f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetInteger("transition", 0);
        lifeText.text = _lives.ToString();
        scoreText.text = score.ToString();
        keyText.SetActive(false);
        _currentGun = GetComponentInChildren<GunPlayerController>();
        _fireRate = _currentGun.getRateOfFire();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null)
        {
            Move();
            Shoot();
            
            if (_lives <= 0)
            {
                gameObject.SetActive(false);
                //Destroy(gameObject);
                _menuGameOverController.Setup(score);
            }
            
            // Caiu
            if(controller.transform.position.y < -10)
            {
                UpdatePlayerLives(-1);
                SetPlayerPosition(new Vector3(24.23f, 13.83f, 0f)); //voltando para o in??cio
                //SetPlayerPosition(lastPosition);
            }
        }
        // estamos mostrando auxiliarText
        if(auxiliarTextDisplayed == true)
        {
            auxiliarTextTime -= Time.deltaTime;

            if(auxiliarTextTime <= 0)
            {
                auxiliarText.text = "";
                auxiliarTextTime = 1.5f;
                auxiliarTextDisplayed = false;
            }
        }
    }

    private void SetPlayerPosition(Vector3 position)
    {
        controller.transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            UpdatePlayerLives(-1);
        }

        if (other.gameObject.CompareTag("Enemy2Legs"))
        {
            UpdatePlayerLives(-1);
        }

        if (other.gameObject.CompareTag("EnemyHumanoide"))
        {
            UpdatePlayerLives(-1);
        }

        if (other.gameObject.CompareTag("EnemyLargeGun"))
        {
            UpdatePlayerLives(-1);
        }

        if (other.gameObject.CompareTag("Projectile1"))
        {
            UpdatePlayerLives(-1);
        }

        if (other.gameObject.CompareTag("Gear"))
        {
            UpdatePlayerScore(1);
        }

        if (other.gameObject.CompareTag("Key"))
        {
            getKey = true;
            keyObject.SetActive(false);
            auxiliarText.text = "Use a chave no portal!";
            auxiliarTextDisplayed = true;
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            if(getKey)
            {
                _menuVictoryController.Setup(score);
                gameObject.SetActive(false);
            }
            else
            {
                auxiliarText.text = "Pegue a chave!";
                auxiliarTextDisplayed = true;
            }
            
        }

        if (other.gameObject.CompareTag("Life"))
        {
            UpdatePlayerLives(1);
            other.gameObject.SetActive(false);
        }
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            //lastPosition = controller.transform.position;
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

    void Shoot()
    {
        if (Input.GetKey(KeyCode.F))
        {
            //Shooting Part
            _fireRateDelta -= Time.deltaTime;

            if(_fireRateDelta < 0)
            {
                _currentGun.Shoot();
                _fireRateDelta = _fireRate;
            }
            // End of Shooting Part
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            animator.SetInteger("transition", 0);
            //moveDirection = Vector3.zero;
        }
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
