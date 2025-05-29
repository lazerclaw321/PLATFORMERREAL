using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatmove : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpHat"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpHat"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}