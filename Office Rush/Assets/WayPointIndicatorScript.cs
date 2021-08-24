using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointIndicatorScript : MonoBehaviour
{
    public float size;
    public Color color;
    public GameObject[] circles;

    public bool change = false;

    void Start()
    {
        circles = GameObject.FindGameObjectsWithTag("Indicators");
    }

    private void Update()
    {
        if (change) Change();
    }

    private void Change()
    {
        for (int i = 0; i < circles.Length; i++)
        {
            circles[i].transform.localScale = new Vector3(size, size, 1f);
            circles[i].GetComponent<SpriteRenderer>().color = color;
        }
        change = false;
    }

}
