using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class bluement : MonoBehaviour
{

    public Rigidbody2D bluemove;
    public Transform spawn;
    public Animator animator;

    public float jumpStregnth;
    public float speed;
    public float gravitydown;

    public string hat = "None";

    int level = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
    }

    void Die()
    {
        hat = "None";
        transform.position = spawn.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && collision.tag == "End")
        {
            Debug.Log("WOOOOOO");
            level++;
            SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && collision.tag == "Hat")
        {
            hat = "Jump";
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -5)
        {
            Die();
        }

        if (hat == "Jump" && bluemove.velocity.y < 0.01f && Input.GetKeyDown(KeyCode.UpArrow) && bluemove.velocity.y > -0.01f)
        {
            animator.SetBool("IsJumping", true);
            bluemove.velocity = Vector2.up * jumpStregnth;
        }

        
        bluemove.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, bluemove.velocity.y);

    }
}
