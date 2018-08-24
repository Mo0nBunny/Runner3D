using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float playerSpeed = 5f;
    public float jumpForce;
    private bool onGround;

    public static int coinCounter;
    public  Spawning spawn;
    float xMin = -1f;
    float xMax = 1f;
   // float yMin = 0f;
   // float yMax = 1.2f;

    private Animator animator;
    private Rigidbody playerRB;
  //  private int lane;
  

    void Start () {
        //lane = 0;
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        //float posY = transform.position.y;
    }

    void Update()
    {

        float newPosY = playerRB.velocity.y;
        Vector3 newVel = new Vector3(0f, 0f, playerSpeed);
        newVel.y = newPosY;
        playerRB.velocity = newVel;

        float newPosX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

       
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
            animator.Play("Sliding");
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
            animator.Play("Sliding");
        }
    
        if (Input.GetKeyDown(KeyCode.Space) && (onGround)) 
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Vector3 newPos = playerRB.transform.position;
            animator.Play("Jumping");
            onGround = false;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TriggerPoint")
        {
            spawn.GetSpawn();
        }

        if (other.gameObject.tag == "addPoint")
        {
            Destroy(other.gameObject);
            coinCounter++;
        }
    }


    void OnCollisionEnter(Collision collision)
    {

        onGround = true;
        if (collision.gameObject.tag == "death")
        {
            Destroy(gameObject);
        }
    }

}

