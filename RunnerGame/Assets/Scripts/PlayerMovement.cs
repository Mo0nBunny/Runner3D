using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] TextMeshProUGUI totalCoins;
    public float playerSpeed = 5f;
    public float jumpForce;
    private bool onGround;
    private SceneLoader sceneLoader;

    public static float timeTotal;
    public Transform deathObj;
    public AudioClip coinSound;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    public static int coinCounter;
    public  Spawning spawn;
    public SwpControl swpControl;
    float xMin = -1f;
    float xMax = 1f;


    private Animator animator;
    private Rigidbody playerRB;

  

    void Start () {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        coinCounter = 0;
       
        totalCoins.text = "Coins:" + PlayerMovement.coinCounter;
    }

    void Update()
    {    
        timeTotal += Time.deltaTime;
     
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

             if (Input.touchCount > 0)
     {
         Touch touch = Input.GetTouch(0);
         if (touch.position.x > (Screen.width / 2))
         {
                transform.position += Vector3.right * playerSpeed * Time.deltaTime;
            }
         if (touch.position.x < (Screen.width / 2))
         {
                transform.position += Vector3.left * playerSpeed * Time.deltaTime;
            }
     }

        if (Input.GetKeyDown(KeyCode.Space) && (onGround)) 
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AudioSource.PlayClipAtPoint(jumpSound, transform.position, 1f);
            Vector3 newPos = playerRB.transform.position;
            animator.Play("Jumping");
            onGround = false;
        }

        if(swpControl.SwipeTap && (onGround))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AudioSource.PlayClipAtPoint(jumpSound, transform.position, 1f);
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
            AudioSource.PlayClipAtPoint(coinSound, transform.position, 1f);
            Destroy(other.gameObject);
            coinCounter++;
            
            totalCoins.text = "Coins:" + PlayerMovement.coinCounter;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        onGround = true;
        if (collision.gameObject.tag == "death")
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, transform.position, 1f);
            Instantiate(deathObj, transform.position, Quaternion.identity);
         
            sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
            sceneLoader.loadGameOver();
        }
    }
}

