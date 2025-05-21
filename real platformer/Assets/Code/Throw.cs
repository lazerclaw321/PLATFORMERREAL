using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Rigidbody2D hatBody;
    public BoxCollider2D hitbox;
    public BoxCollider2D ignore;
    public float speed = 1;
    public float height = 1;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(hitbox, ignore);
        hatBody.velocity = Vector2.left * speed + Vector2.up * height;
        
        //StartCoroutine(launch());
    }

    IEnumerator launch()
    {
        if (!(speed == 0))
        {
            hitbox.enabled = false;
            hatBody.velocity = Vector2.left * speed + Vector2.up * height;
            yield return new WaitForSeconds(0.001f);
            hitbox.enabled = true;
        }
    }
}
