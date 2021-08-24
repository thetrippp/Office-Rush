using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public List<Transform> wayPoints;

    int index;

    LineRenderer lr;

    public Transform toAdd;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        toAdd = null;
    }

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Transform t = wayPoints[0];
            wayPoints.Clear();
            wayPoints.Add(t);
        }


        if(toAdd != null)
        {
            if (!wayPoints.Contains(toAdd))
            {
                if (wayPoints.Count == 0)
                {
                    wayPoints.Add(toAdd);
                    toAdd.GetComponent<WayPointScript>().active = true;
                    toAdd = null;
                }
                else if (wayPoints.Count == 1)
                {
                    if (wayPoints[0].GetComponent<WayPointScript>().CheckWayPointAdjescent(toAdd))
                    {
                        wayPoints.Add(toAdd);
                        toAdd.GetComponent<WayPointScript>().active = true;
                    }
                    else if (wayPoints[wayPoints.Count - 1].GetComponent<WayPointScript>().CommonAdjescentWayPoint(toAdd) != null)
                    {
                        Transform temp = wayPoints[wayPoints.Count - 1].GetComponent<WayPointScript>().CommonAdjescentWayPoint(toAdd);
                        wayPoints.Add(temp);
                        temp.GetComponent<WayPointScript>().active = true;
                        wayPoints.Add(toAdd);
                        toAdd.GetComponent<WayPointScript>().active = true;
                    }
                    toAdd = null;
                }
                else if (wayPoints.Count >= 2)
                {
                    if (wayPoints[wayPoints.Count - 1].GetComponent<WayPointScript>().CheckWayPointAdjescent(toAdd))
                    {
                        wayPoints.Add(toAdd);
                        toAdd.GetComponent<WayPointScript>().active = true;
                        return;
                    }
                    if(wayPoints[wayPoints.Count - 1].GetComponent<WayPointScript>().CommonAdjescentWayPoint(toAdd) != null)
                    {
                        Transform temp = wayPoints[wayPoints.Count - 1].GetComponent<WayPointScript>().CommonAdjescentWayPoint(toAdd);
                        if (!wayPoints.Contains(temp))
                        {
                            wayPoints.Add(temp);
                            temp.GetComponent<WayPointScript>().active = true;
                        }
                        else if(wayPoints.Contains(temp) && temp == wayPoints[wayPoints.Count - 2] && wayPoints[wayPoints.Count - 2].GetComponent<WayPointScript>().CheckWayPointAdjescent(toAdd))
                        {
                            wayPoints[wayPoints.Count - 1].GetComponent<WayPointScript>().active = false;
                            wayPoints.RemoveAt(wayPoints.Count - 1);
                        }
                        wayPoints.Add(toAdd);
                        toAdd.GetComponent<WayPointScript>().active = true;
                    }
                    toAdd = null;
                }
            }
            else if (wayPoints.Contains(toAdd))
            {
                for (int i = 0; i < wayPoints.Count; i++)
                    if (wayPoints[i] == toAdd)
                        index = i;
                for (int i = index + 1; i < wayPoints.Count; i++)
                    wayPoints[i].GetComponent<WayPointScript>().active = false;
                wayPoints.RemoveRange(index + 1, wayPoints.Count - 1 - index);
                toAdd = null;
            }
        }
    }
    
    public void RemoveFirst()
    {
        wayPoints[0].GetComponent<WayPointScript>().active = false;
        wayPoints.RemoveAt(0);
    }

    public Transform ReturnWaypoint()
    {
        if (wayPoints.Count > 0)
            return wayPoints[0];
        else
            return null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WayPoint") && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0)))
        {
            if (wayPoints.Count < 1)
            {
                if (collision.GetComponent<WayPointScript>().CheckAdjescentPlayer())
                    toAdd = collision.transform;
            }
            else
                toAdd = collision.transform;
        }
        if(collision.CompareTag("WayPoint") && Input.GetKeyDown(KeyCode.Mouse1))
        {
            toAdd = null;
            wayPoints.Clear();
        }
    }

}