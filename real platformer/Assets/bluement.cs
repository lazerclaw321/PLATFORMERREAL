using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluement : MonoBehaviour
{

    public Rigidbody2D bluemove;
    public float jumpStregnth;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            bluemove.velocity = Vector2.up * jumpStregnth;
        }
        bluemove.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, bluemove.velocity.y);
    }
}
