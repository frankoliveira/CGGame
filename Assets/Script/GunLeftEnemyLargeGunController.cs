using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLeftEnemyLargeGunController : MonoBehaviour
{
    [SerializeField] GameObject _proctile1;
    [SerializeField] float _rateOfFire = 0.5f;
    public float getRateOfFire()
    {
        return _rateOfFire;
    }

    public void Shoot()
    {
        Instantiate(_proctile1, transform.position, transform.rotation);
    }
}
