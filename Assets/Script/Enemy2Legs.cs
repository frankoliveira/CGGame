using UnityEngine;

public class Enemy2Legs : MonoBehaviour
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

    //Shooting properties
    private Transform _playerTransform;
    private GunEnemy2LegsController _currentGun;
    private float _fireRate;
    private float _fireRateDelta = 0f;
    [SerializeField] float _playerRange = 13.0f;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _leftLimiteX = _controller.transform.position.x - lefttWalkDistance;
        _rightLimiteX = _controller.transform.position.x + rightWalkDistance;
        _playerTransform = FindObjectOfType<Player>().transform;
        _currentGun = GetComponentInChildren<GunEnemy2LegsController>();
        _fireRate = _currentGun.getRateOfFire();
    }

    void Update()
    {
        Move();
        Vector3 playerGroundPos = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);
        _fireRateDelta -= Time.deltaTime;

        if(_fireRateDelta < 0)
        {
            if (Vector3.Distance(transform.position, playerGroundPos) < _playerRange)
            {
                _currentGun.Shoot();
                _fireRateDelta = _fireRate;
            }
        }
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
}
