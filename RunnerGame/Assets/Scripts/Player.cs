using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject firstObject;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject obstacle;

    //private Rigidbody playerRB;
    private Animator animator;
    private int lane;
    public int coinCounter;
    float lenghtOfPlatform = 22.8f;      
    float minHeight = 0.4f;
    float maxHeight = 1.5f;
 
    Vector3 lastPosition;
    Vector3 firstPosition;


   // private Vector3 moveDirection;
   // private CharacterController controller;



    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20f;
    private CharacterController controller;
   // private Animator anim;

    private bool isChangingLane = false;
    private Vector3 locationAfterChangingLane;
    //distance character will move sideways
    private Vector3 sidewaysMovementDistance = Vector3.right * 1f;

    public float SideWaysSpeed = 5.0f;

    public float JumpSpeed = 8.0f;
    public float Speed = 6.0f;
    //Max gameobject
    public Transform CharacterGO;


    void Start () {
        lane = 0;
        coinCounter = 0;
        animator = GetComponent<Animator>();
        //playerRB = this.GetComponent<Rigidbody>();  
        //playerRB.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, speed);
        firstPosition = firstObject.transform.position;





        moveDirection = transform.forward;
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= Speed;

        

      

       
       
       // controller = GetComponent();
    }
	
    private void Spawning()
    {
        GameObject _object = Instantiate(platform) as GameObject;
        _object.transform.position = firstPosition + new Vector3(0f, 0f, lenghtOfPlatform);  
        firstPosition = _object.transform.position;  
        lastPosition = firstPosition + new Vector3(0f, 0f, lenghtOfPlatform); 
   
        
        if (_object != null)
        {
            GameObject coinObject = Instantiate(coin) as GameObject;
            coinObject.transform.position = new Vector3(Random.Range(-1f, 1f), Random.Range(minHeight, maxHeight), Random.Range(firstPosition.z, lastPosition.z));

            GameObject obstacleObject = Instantiate(obstacle) as GameObject;
            obstacleObject.transform.position = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(firstPosition.z, lastPosition.z));
        }
        
       
    }
	
	void Update () {
       // Vector3 oldPos = playerRB.transform.position;
        
        float step = 30f;

        moveDirection = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //playerRB.AddForce(0f, 5f, 0f, ForceMode.Impulse);
            moveDirection.y = JumpSpeed;
            animator.Play("Jumping");
        }

        

      

        if (Input.GetKeyDown(KeyCode.A) && (lane > -1))
        {
            lane--;
            //animator.Play("Sliding");
            Debug.Log(transform.position);
            locationAfterChangingLane = transform.position - sidewaysMovementDistance;
            moveDirection.x = -SideWaysSpeed;
            // Vector3 newPos = new Vector3(- 1f, oldPos.y, oldPos.z);
            // transform.position = Vector3.MoveTowards(oldPos, newPos, step);
            // transform.position = Vector3.Slerp(oldPos, newPos, step);

            //transform.position = Vector3.Lerp(oldPos, newPos, 5f);
            animator.Play("Sliding");
            Debug.Log(transform.position);
        }
        if(Input.GetKeyDown(KeyCode.D) && (lane < 1) )
        {
            lane++;  
            animator.Play("Sliding");
            locationAfterChangingLane = transform.position + sidewaysMovementDistance;
            moveDirection.x = SideWaysSpeed;
            // Vector3 newPos = new Vector3(1f, oldPos.y, oldPos.z);
            //  transform.position = Vector3.Slerp(oldPos, newPos, step);
            // transform.position.move

            // transform.position = Vector3.Lerp(oldPos, newPos, 30f);
            animator.Play("Sliding");
            Debug.Log(transform.position);
        }

       
       // controller.Move(moveDirection * Time.deltaTime);



        // Vector3 newPos = transform.position;
        // newPos.x = lane;
        // transform.position = newPos;
        // transform.Rotate(Vector3.up, 0.0f);


    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TriggerPoint")
        {
            Spawning();
        }

        if (other.gameObject.tag == "addPoint")
        {
            Destroy(other.gameObject);
            coinCounter++;
        }
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "death")
        {
            Destroy(gameObject);
        }
    }
   
}


