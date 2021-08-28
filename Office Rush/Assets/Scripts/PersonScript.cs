using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonScript : MonoBehaviour
{
    Transform objective;
    BoxCollider2D box;
    GameObject main;

    [HideInInspector]
    public DirectionFacing directionFacing;

    public Action action = Action.None;
    [HideInInspector]
    public float displacement;
    int direction;

    public Sprite target, source;

    private bool active;
    Color color;

    GameObject other;

    public float time;
    float divTime;
    float redTime = 6f;
    float yellowTime = 12f;
    float greenTime = 18f;

    void Start()
    {
        main = transform.parent.gameObject;
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        direction = (directionFacing == DirectionFacing.Up) ? 1 : -1;
        objective = transform.GetChild(0);
        objective.position = transform.position + (Vector3)(Vector2.up * direction * displacement);
        objective.gameObject.SetActive(false);
    }

    void Update()
    {
        if (active && !GameObject.Find("Player").GetComponent<PlayerScript>().stop)
        {
            time -= Time.deltaTime;
            box.enabled = true;
            objective.gameObject.SetActive(true);
            if (action == Action.Receiver)
            {
                objective.GetComponent<SpriteRenderer>().sprite = target;
                float value = time / divTime;
                objective.localScale = new Vector3(value, value, 1);

                if (time < 0)
                {
                    box.enabled = false;
                    objective.gameObject.SetActive(false);
                    main.GetComponent<MainScript>().ReturnReceiver(gameObject, 0);
                    active = false;
                    other = null;
                }
            }
            else if (action == Action.Sender)
            {
                objective.GetComponent<SpriteRenderer>().sprite = source;
                float value = time / divTime;
                objective.localScale = new Vector3(value, value, 1);
            }
            objective.GetComponent<SpriteRenderer>().color = color;
        }
        else
            objective.localScale = Vector3.one;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.CompareTag("Player"))
            {
                if (action == Action.Sender)
                {
                    box.enabled = false;
                    objective.gameObject.SetActive(false);
                    active = false;
                    main.GetComponent<MainScript>().ReturnSender(gameObject);
                    return;

                }
                if (action == Action.Receiver)
                {
                    if (other == null)
                    {
                        box.enabled = false;
                        objective.gameObject.SetActive(false);
                        main.GetComponent<MainScript>().ReturnReceiver(gameObject, 1);
                        active = false;
                        return;
                    }
                }
            }
        }
    }

    public void SetColor(int col)
    {
        if (col == 0)
        {
            color = new Color(255f, 0, 0);
            time = divTime = redTime;
        }
        else if (col == 1)
        {
            color = new Color(255f, 160f, 0);
            time = divTime = yellowTime;
        }
        else if (col == 2)
        {
            color = new Color(0, 255f, 0f);
            time = divTime = greenTime;
        }
    }

    public void SetActive(bool b)
    {
        active = b;
    }

    public void SetOther(GameObject g)
    {
        other = g;
    }

    public GameObject GetOther()
    {
        return other;
    }

}


public enum DirectionFacing
{
    Up,
    Down
}
public enum Action
{
    Sender,
    Receiver,
    None
}
