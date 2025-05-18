using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Rigidbody2D hatBody;
    public BoxCollider2D hitbox;
    public float speed = 1;
    public float height = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(launch());
    }

    IEnumerator launch()
    {
        if (!(speed == 0))
        {
            hitbox.enabled = false;
            hatBody.velocity = Vector2.left * speed + Vector2.up * height;
            yield return new WaitForSeconds(0.05f);
            hitbox.enabled = true;
        }
    }
}
