using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float speed = 6f;

    Vector3 movement;

   Rigidbody playerRigidBody;

    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
    }

    // Use this for initialization
    void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        transform.localEulerAngles = new Vector3(-Input.mousePosition.y, Input.mousePosition.x, 0);
        //Debug.Log(h);
        //Turning();
        if (Input.GetKeyDown(KeyCode.Space)) playerRigidBody.AddForce(Vector3.up * speed*100f);
    }

    void Move (float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;

        transform.position+= (transform.forward * v + transform.right * h)*speed * Time.deltaTime;

    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point;// - transform.position;
            
            //playerToMouse.y = 0f;
            transform.LookAt(playerToMouse);
            //transform.LookAt(floorHit.transform);
            //Quaternion newRotation = Quaternion.LookRotation(Vector3.playerToMouse);
        }
    }
 }
