using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluement : MonoBehaviour
{

    public Rigidbody2D bluemove;
    public float jumpStregnth;
    public float speed;
    public float gravitydown;
    public Transform spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "End")
        {
            Debug.Log("WOOOOOO");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -5)
        {
            transform.position = spawn.position;
        }

        if (bluemove.velocity.y < 0.01f && Input.GetKeyDown(KeyCode.UpArrow) && bluemove.velocity.y > -0.01f)
        {
            bluemove.velocity = Vector2.up * jumpStregnth;
        }

        
        bluemove.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, bluemove.velocity.y);

    }
}
