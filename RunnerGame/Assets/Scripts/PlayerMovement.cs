using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float playerSpeed = 5f;
    float xMin = -1f;
    float xMax = 1f;
    float yMin = 0f;
    float yMax = 1.2f;

    private Animator animator;
    private Rigidbody playerRB;
    private bool actionIsActive = false;
    private int lane;
  

  


    // Use this for initialization
    void Start () {
        lane = 0;
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        playerRB.velocity = new Vector3(0f, 0f, playerSpeed);
        float posY = transform.position.y;
        Debug.Log(transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //playerRB.velocity = new Vector3(0f, 0f, playerSpeed);
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
            animator.Play("Sliding");
            Debug.Log(transform.position);
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
            animator.Play("Sliding");
        }

        if (Input.GetKeyDown(KeyCode.Space) && (!actionIsActive))
        {
            actionIsActive = true;
            playerRB.AddForce(0f, 5f, 0f, ForceMode.Impulse);
            animator.Play("Jumping");
        }
        Debug.Log(transform.position.y);
        if (transform.position.y <= 0.1f)
        {
            Debug.Log(transform.position.y);
            actionIsActive = false;
        }

        float newPosX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

    }

}

