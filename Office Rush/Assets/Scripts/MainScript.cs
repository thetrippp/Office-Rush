using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public List<GameObject> people;

    public List<GameObject> senders, receivers;


    public bool select;
    public float maxSelectTime;
    public float selectTime;

    Color color;

    [HideInInspector]
    public int money = 0;

    public GameObject endscreen;
    public GameObject player;

    public Text moneyText;

    void Start()
    {
        selectTime = maxSelectTime;
        endscreen.SetActive(false);
        money = 0;
        moneyText.text = "$" + money.ToString();
    }

    void Update()
    {
        moneyText.text = "$" + money.ToString();
        if (money < 0)
            moneyText.color = new Color(255, 0, 0);
        else if(money >=0 && money <= 10)
            moneyText.color = new Color(255, 173, 0);
        else
            moneyText.color = new Color(0, 255, 0);
        if (!(money <= -15))
        {
            if (selectTime > 0 && select && !player.GetComponent<PlayerScript>().stop)
                selectTime -= Time.deltaTime;
            if (selectTime <= 0)
            {
                selectTime = maxSelectTime;
                if (people.Count > 2)
                {
                    int col = Random.Range(0, 3);

                    int num;
                    GameObject g;
                    num = Random.Range(0, people.Count - 1);
                    g = people[num];
                    g.GetComponent<PersonScript>().SetColor(col);
                    g.GetComponent<PersonScript>().SetActive(true);
                    g.GetComponent<PersonScript>().action = Action.Sender;
                    senders.Add(g);
                    people.RemoveAt(num);

                    GameObject t = g;
                    g = people[num];
                    g.GetComponent<PersonScript>().SetColor(col);
                    g.GetComponent<PersonScript>().SetActive(true);
                    g.GetComponent<PersonScript>().action = Action.Receiver;
                    g.GetComponent<PersonScript>().SetOther(t);
                    if (col == 0) g.GetComponent<PersonScript>().time = 6f;
                    if (col == 1) g.GetComponent<PersonScript>().time = 12f;
                    if (col == 2) g.GetComponent<PersonScript>().time = 18f;
                    receivers.Add(g);
                    people.RemoveAt(num);
                }
            }
        }
        else
        {
            player.GetComponent<PlayerScript>().stop = true;
            endscreen.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && money >- 15)
            player.GetComponent<PlayerScript>().stop = !player.GetComponent<PlayerScript>().stop;

        if (Input.GetKeyDown(KeyCode.R))
            RestartScene();

    }

    public void ReturnSender(GameObject G)
    {
        foreach(GameObject g in receivers)
        {
            if (g.GetComponent<PersonScript>().GetOther() == G)
                g.GetComponent<PersonScript>().SetOther(null);
        }
    }
    public void ReturnReceiver(GameObject g, int s = 0)
    {
        int index = 0;
        for(int i = 0; i < receivers.Count; i++)
        {
            if (receivers[i] == g)
            {
                index = i;
                break;
            }
        }
        people.Add(senders[index]);
        senders.RemoveAt(index);
        people.Add(receivers[index]);
        receivers.RemoveAt(index);

        if (s == 1) money++;
        if (s == 0) money--;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
