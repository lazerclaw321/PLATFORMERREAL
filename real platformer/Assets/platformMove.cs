using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMove : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed;
    private Vector3 nextPostition;

    // Start is called before the first frame update
    void Start()
    {
        nextPostition = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPostition, speed * Time.deltaTime);

        if (transform.position == nextPostition)
        {
            nextPostition = (nextPostition == pointA.position) ? pointB.position : pointA.position;
        }
    }


    //Move with object
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("MoveableObject") || collision.gameObject.CompareTag("JumpHat"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("MoveableObject") || collision.gameObject.CompareTag("JumpHat"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
