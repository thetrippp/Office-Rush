using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    SpriteRenderer sr;

    public Transform WayPoint1, WayPoint2;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    void Update()
    {
        if (WayPoint1.GetComponent<WayPointScript>().active == true && WayPoint2.GetComponent<WayPointScript>().active == true)
            sr.enabled = true;
        if (WayPoint1.GetComponent<WayPointScript>().active == false && WayPoint2.GetComponent<WayPointScript>().active == false)
            sr.enabled = false;
    }
}
