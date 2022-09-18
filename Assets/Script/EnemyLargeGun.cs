using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLargeGun : MonoBehaviour
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
    [SerializeField] private GunLeftEnemyLargeGunController _leftGun;
    [SerializeField] private GunRightEnemy2LegsController _rightGun;
    private float _fireRate;
    private float _fireRateRightGun;
    private float _fireRateDeltaLeftGun = 0f;
    private float _fireRateDeltaRightGun = 0f;
    [SerializeField] float _playerRange = 13.0f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _leftLimiteX = _controller.transform.position.x - lefttWalkDistance;
        _rightLimiteX = _controller.transform.position.x + rightWalkDistance;
        _playerTransform = FindObjectOfType<Player>().transform;
        _leftGun = GetComponentInChildren<GunLeftEnemyLargeGunController>();
        _rightGun = GetComponentInChildren<GunRightEnemy2LegsController>();
        _fireRate = _leftGun.getRateOfFire();
        _fireRateRightGun = _rightGun.getRateOfFire();
    }

    void Update()
    {
        Move();
        Vector3 playerGroundPos = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);

        _fireRateDeltaLeftGun -= Time.deltaTime;
        _fireRateDeltaRightGun -= Time.deltaTime;

        if (Vector3.Distance(transform.position, playerGroundPos) < _playerRange)
        {
            if (_fireRateDeltaLeftGun < 0)
            {
                _leftGun.Shoot();
                _fireRateDeltaLeftGun = _fireRate;
            }

            if(_fireRateDeltaRightGun < 0)
            {
                _rightGun.Shoot();
                _fireRateDeltaRightGun = _fireRateRightGun;
            }
        }
    }

    void Move()
    {
        if (directionRight)
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
