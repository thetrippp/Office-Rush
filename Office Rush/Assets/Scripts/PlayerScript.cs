using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;

    public GameObject controller;

    public float maxSpeed;
    float moveSpeed;

    int chances = 3;

    void Start()
    {
        moveSpeed = maxSpeed;
    }

    void Update()
    {
        if (chances > 0)
        {
            if (Input.GetKey(KeyCode.Space))
                moveSpeed = maxSpeed * 2.25f;
            else
                moveSpeed = maxSpeed;

            target = controller.GetComponent<ControllerScript>().ReturnWaypoint();
            if (target != null)
            {
                if (Vector2.Distance(target.position, transform.position) > 0.01f)
                    transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                else
                {
                    transform.position = target.position;
                    controller.GetComponent<ControllerScript>().RemoveFirst();
                }
            }
        }
        else
        {
            //GAME OVER.
        }
    }

    public void ReduceChances()
    {
        chances--;
        if (chances < 0)
            chances = 0;
    }

}
