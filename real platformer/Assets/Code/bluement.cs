using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class bluement : MonoBehaviour
{

    public Rigidbody2D bluemove;
    public Transform spawn;
    public Animator animator;
    public BoxCollider2D hitbox;

    public float jumpStregnth;
    public float speed;
    public float gravitydown;

    public string hat = "None";

    public int level;

    bool grounded = false;
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
        if (Input.GetKey(KeyCode.DownArrow) && collision.tag == "End")
        {
            Debug.Log("WOOOOOO");
            level++;
            SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
        }
        if (Input.GetKey(KeyCode.DownArrow) && collision.tag == "JumpHat")
        {
            animator.SetBool("JumpHat", true);
            hat = "Jump";
            Destroy(collision.gameObject);
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
            Debug.Log(hitbox.size);
        }

        //Hat Effects
        //Jumping
        if (hat == "Jump" && Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            animator.SetBool("IsJumping", true);
            bluemove.velocity = Vector2.up * jumpStregnth;
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
        

        //Horizontal Movement
        bluemove.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, bluemove.velocity.y);

    }
}
