using System.Collections;
using System.Collections.Generic;

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

    public int level;

    bool grounded = false;
    GameObject thrown;
    Vector2 baseSize;
    Vector2 baseOffset;
    

    // Start is called before the first frame update
    void Start()
    {
        baseSize = hitbox.size;
        baseOffset = hitbox.offset;
        Debug.Log(baseSize);
        //SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
    }

    void Die()
    {
        hat = "None";
        SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && collision.tag == "End")
        {
            Debug.Log("WOOOOOO");
            SceneManager.LoadScene("Level " + (level + 1), LoadSceneMode.Single);
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && collision.tag == "JumpHat")
        {
            hat = "Jump";
            thrown = collision.gameObject;
            collision.gameObject.GetComponent<Follow>().targetObj = transform;
            collision.gameObject.GetComponent<Follow>().player = this.gameObject;
            //Destroy(collision.gameObject);
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

        if (hat == "None")
        {
            hitbox.size = baseSize;
            hitbox.offset = baseOffset;
        }
        else
        {
            hitbox.size = new Vector2(baseSize.x, baseSize.y * 1.75f);
            hitbox.offset = new Vector2(baseOffset.x, -(baseOffset.y + (baseSize.y / 1.75f) + 0.48f));
        }

        //Hat Effects
        //Jumping
        if (hat == "Jump" && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded)
        {
            animator.SetBool("IsJumping", true);
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

        //Ground Check
        if (bluemove.velocity.y < 0.2f && bluemove.velocity.y > -0.2f)
        {
            animator.SetBool("IsJumping", false);
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        //Reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }

        //Horizontal Movement
        bluemove.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, bluemove.velocity.y);

    }
}
