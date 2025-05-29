using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bluement : MonoBehaviour
{

    public Rigidbody2D bluemove;
    public Animator animator;
    public BoxCollider2D hitbox;

    public float jumpStregnth;
    public float speed;
    public float gravitydown;

    public string hat = "None";
    public GameObject jumpHatObject;
    public float throwspeed;

    public bool active = true;
    public GameObject house;
    GameObject logic;

    bool grounded = false;
    GameObject thrown;
    Vector2 baseSize;
    Vector2 baseOffset;
    

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectsWithTag("Logic")[0];
        baseSize = hitbox.size;
        baseOffset = hitbox.offset;
        Debug.Log(baseSize);
    }

    void Die()
    {
        hat = "None";
        logic.GetComponent<Logic>().reset = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "End")
        {
            house = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "End")
        {
            house = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && collision.tag == "JumpHat" && active)
        {
            //check if player is not under a block
            RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) - new Vector2(hitbox.size.x / 5, 0), Vector2.up);
            RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) + new Vector2(hitbox.size.x / 5, 0), Vector2.up);
            if ((!hitLeft || Mathf.Abs(hitLeft.point.y - transform.position.y) > hitbox.size.y * 0.5f) && (!hitRight || Mathf.Abs(hitRight.point.y - transform.position.y) > hitbox.size.y * 0.5f))
            {
                hat = "Jump";
                thrown = collision.gameObject;
                collision.gameObject.GetComponent<Follow>().targetObj = transform;
                collision.gameObject.GetComponent<Follow>().player = this.gameObject;
            }
            
        }

        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && collision.tag == "Lever" && active)
        {
            collision.gameObject.GetComponent<LeverScript>().swap = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "JumpHat")
        {
            Debug.Log("WOOOOO");
            Physics2D.IgnoreCollision(hitbox, collision.gameObject.GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Killbricks")
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Death
        if (transform.position.y <= -5)
        {
            Die();
        }

        //Ground Check
        if (bluemove.velocity.y < 0.01f && bluemove.velocity.y > -0.01f)
        {
            animator.SetBool("IsJumping", false);
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (hat == "None")
        {
            hitbox.size = baseSize;
            hitbox.offset = baseOffset;
        }
        else
        {
            hitbox.size = new Vector2(baseSize.x, baseSize.y * 1.28f);
            hitbox.offset = new Vector2(baseOffset.x, -(baseOffset.y + (baseSize.y / 1.28f) + 0.16f));
        }

        //Can only do these actions if being controlled
        if (active)
        {
            //Hat Effects
            //Jumping
            if (hat == "Jump" && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded && active)
            {
                grounded = false;
                animator.SetBool("IsJumping", true);
                animator.SetBool("IsJumpin", true);
                bluemove.velocity = Vector2.up * jumpStregnth;
            }

            //Throw Hat
            if (hat != "None" && Input.GetKeyDown(KeyCode.X))
            {
                hat = "None";
                Destroy(thrown);
                thrown = Instantiate(jumpHatObject, transform.position, Quaternion.identity);
                thrown.GetComponent<Throw>().ignore = hitbox;
            }
            if (hat != "None" && Input.GetKeyDown(KeyCode.Z))
            {
                hat = "None";
                Destroy(thrown);
                thrown = Instantiate(jumpHatObject, transform.position, Quaternion.identity);
                thrown.GetComponent<Throw>().speed = thrown.GetComponent<Throw>().speed * -1;
                thrown.GetComponent<Throw>().ignore = hitbox;

            }

            //Reset
            if (Input.GetKeyDown(KeyCode.R))
            {
                Die();
            }

            //Horizontal Movement
            bluemove.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, bluemove.velocity.y);
        }
        else
        {
            if (grounded)
            { 
                bluemove.velocity = new Vector2(0, bluemove.velocity.y);
            }
        }
    }
}
