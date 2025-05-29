using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movewith : MonoBehaviour
{
// Move with object
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
