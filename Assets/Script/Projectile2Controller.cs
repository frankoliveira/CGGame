using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2Controller : MonoBehaviour
{
    [SerializeField] float _projectileSpeed = 15.0f;
    private float elapsedTime = 1.5f;

    void Update()
    {
        transform.Translate(new Vector3(0f, 0f, _projectileSpeed * Time.deltaTime));
        elapsedTime -= Time.deltaTime;

        if(elapsedTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Gear"))
        {
            Destroy(gameObject);
        }
    }
}
