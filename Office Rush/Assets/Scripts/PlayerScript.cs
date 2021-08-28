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

    [HideInInspector]
    public bool stop;

    void Start()
    {
        moveSpeed = maxSpeed;
        stop = false;
    }

    void Update()
    {
        if (!stop)
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
                float xvalue, yvalue, zvalue = 0;
                xvalue = target.position.x - transform.position.x;
                yvalue = target.position.y - transform.position.y;

                if (xvalue > 0.02f) zvalue = 90;
                if (xvalue < 0.02f) zvalue = 0;
                if (yvalue > 0.02f) zvalue = 180;
                if (xvalue < -0.02f) zvalue = 270;

                transform.rotation = Quaternion.Euler(0, 0, zvalue);
            }
        }
    }


}
