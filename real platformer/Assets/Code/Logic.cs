using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] houses;
    public int level;
    public string type = "";
    public bool reset = false;
    int chosen = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<bluement>().active = false;
        }
        players[0].GetComponent<bluement>().active = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            SceneManager.LoadScene("Level " + level + type, LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            players[chosen].GetComponent<bluement>().active = false;
            chosen = (chosen + 1) % players.Length;
            players[chosen].GetComponent<bluement>().active = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            bool check = true;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<bluement>().house)
                {
                    Debug.Log(players[i].GetComponent<bluement>().house == houses[i]);
                    Debug.Log(check);
                    check = (players[i].GetComponent<bluement>().house == houses[i]) && check;
                }
                else
                {
                    check = false;
                }
            }
            Debug.Log(check);
            if (check)
            {
                SceneManager.LoadScene("Level " + (level + 1) + type, LoadSceneMode.Single);
            }
        }
    }
}
