using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D hitbox;
    public Sprite[] sprites;

    public string color = "R";
    GameObject[] levers;
    public List<GameObject> on;
    public List<GameObject> off;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hi");
        levers = GameObject.FindGameObjectsWithTag("Lever");
          
        for (int i = 0; i < levers.Length; i++)
        {
            if (levers[i].GetComponent<LeverScript>().colorOn == color)
            {
                on.Add(levers[i]);
            }
            else if (levers[i].GetComponent<LeverScript>().colorOff == color)
            {
                off.Add(levers[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool enable = false;
        foreach (GameObject obj in on)
        {
            enable = enable || obj.GetComponent<LeverScript>().on;
        }
        foreach (GameObject obj in off)
        {
            enable = enable || (!obj.GetComponent<LeverScript>().on);
        }

        if (enable)
        {
            hitbox.enabled = true;
            spriteRenderer.sprite = sprites[0];
        }
        else
        {
            hitbox.enabled = false;
            spriteRenderer.sprite = sprites[1];
        }
        
    }
}
