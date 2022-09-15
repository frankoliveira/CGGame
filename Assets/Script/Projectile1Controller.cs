using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1Controller : MonoBehaviour
{
    [SerializeField] float _projectileSpeed = 15.0f;

    void Update()
    {
        transform.Translate(new Vector3(0f, 0f, _projectileSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy2Legs"))
        {
            Destroy(gameObject);
        }
    }
}
