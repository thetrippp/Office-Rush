using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointScript : MonoBehaviour
{

    public float size = 1f;

    public bool ShowGizmo = false;
    [HideInInspector]
    public bool active = false;
    public bool player = false;

    public GameObject[] adjescent;

    GameObject circle;
    int n, m;

    void Start()
    {
        circle = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            active = false;

        circle.SetActive(active);

    }

    private void OnDrawGizmos()
    {
        if (ShowGizmo)
        {
            if (!active)
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, size);
        }
    }

    public bool CheckWayPointAdjescent(Transform t)
    {
        for (int i = 0; i < adjescent.Length; i++)
        {
            if (adjescent[i].transform.name == t.name)
                return true;
        }
        return false;
    }

    public bool CheckAdjescentPlayer()
    {
        for(int i = 0; i < adjescent.Length; i++)
        {
            if (adjescent[i].GetComponent<WayPointScript>().player)
                return true;
        }
        return false;
    }

    public Transform CommonAdjescentWayPoint(Transform t)
    {
        for (int i = 0; i < adjescent.Length; i++)
        {
            for (int j = 0; j < t.GetComponent<WayPointScript>().adjescent.Length; j++)
            {
                if (adjescent[i] == t.GetComponent<WayPointScript>().adjescent[j])
                {
                    Transform temp = adjescent[i].transform;
                    return temp;
                }
            }
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            player = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            player = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            player = false;
    }
}
