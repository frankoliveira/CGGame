using UnityEngine;

public class EnemyHumanoide : MonoBehaviour
{
    //Enemy properties
    private CharacterController _controller;
    private Animator _animator;
    public float moveSpeed;
    public float gravity;
    public float originalRotation = 90f;
    public float inverseRotation = -90;
    public float rightWalkDistance;
    public float lefttWalkDistance;
    public bool directionRight = true;
    private float _xPositionPlayer;
    private float _leftLimiteX;
    private float _rightLimiteX;
    private Vector3 _moveDirection;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _leftLimiteX = _controller.transform.position.x - lefttWalkDistance;
        _rightLimiteX = _controller.transform.position.x + rightWalkDistance;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if(directionRight)
        {
            transform.eulerAngles = new Vector3(0, originalRotation, 0);
            _moveDirection = Vector3.right * moveSpeed;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, inverseRotation, 0);
            _moveDirection = Vector3.left * moveSpeed;
            
        }

        _xPositionPlayer = _controller.transform.position.x;

        if (_xPositionPlayer >= _rightLimiteX)
        {
            directionRight = false;
        }
        else if (_xPositionPlayer <= _leftLimiteX)
        {
            directionRight = true;
        }

        if (_controller.transform.position.y > 0)
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }

        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile2"))
        {
            Destroy(gameObject);
        }
    }
}
