using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public string colorOn = "R";
    public string colorOff = "N";
    public bool on = false;
    public bool swap = false;

    private float prev;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (swap)
        {
            if (Time.time - prev > 1.0f)
            {
                on = !on;
                prev = Time.time;
            }
            swap = false;
        }

        if (on)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else
        {
            spriteRenderer.sprite = sprites[0];
        }
    }
}
