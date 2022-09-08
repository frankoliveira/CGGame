using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockElevator : MonoBehaviour
{
    private CharacterController controller;

    public float speed;
    public float gravity;
    private Vector3 moveDiection;
    private bool up = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (up)
        {
            moveDiection = Vector3.up * speed;
        }
        else
        {
            moveDiection = Vector3.down * speed;
        }

        if (controller.transform.position.y >= 16f)
        {
            up = false;
        }
        else if (controller.transform.position.y <= 6f)
        {

            up = true;
        }
        
        controller.Move(moveDiection * Time.deltaTime);
    }
}
