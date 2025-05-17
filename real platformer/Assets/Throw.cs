using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Rigidbody2D hatBody;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(hatBody.velocity.y < 0.01f && hatBody.velocity.y > -0.01f))
        {
            transform.position = new Vector3(transform.position.x + speed/100, transform.position.y, transform.position.z);
            //hatBody.velocity = Vector2.left * speed;
        }
    }
}
