using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlayerController : MonoBehaviour
{
    [SerializeField] GameObject _proctile2;
    [SerializeField] float _rateOfFire = 0.4f;
    public float getRateOfFire()
    {
        return _rateOfFire;
    }

    public void Shoot()
    {
        Instantiate(_proctile2, transform.position, transform.rotation);
    }
}
