using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFollower : MonoBehaviour {
    
    public bool deactivate; //check to deactivate
    public bool detect; //detects colliders inside a sphere when true; then changes back to false;
    public bool isFollowing;
    public GameObject follow;
    public float radius; //radius of detection;
    public float projection; //was used for the spherecast
    public GameObject CurrentNode; // currentNode head
    public Transform[] Destinations;
    public Transform DestinationCurrent;
    public int currentDest;
    public float DestThres;
    public Rigidbody rb;
    public float currentSpeed;
    public bool lastNode;
    public bool stop;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (deactivate) return;
        
        //detect node
        if (detect) {

            //int layerMask = 1 << 8;
            Collider[] detected = Physics.OverlapBox(transform.position,Vector3.one*.5f,transform.rotation); //, layerMask, QueryTriggerInteraction.Collide)) 
                {
                Collider chosen;
                if (detected.Length > 0)
                {
                    chosen = detected[Random.Range(0, detected.Length)];
                    if (chosen.transform.GetComponent<Node>() != null)
                    {
                        CurrentNode = chosen.transform.gameObject;
                        Destinations = CurrentNode.GetComponentsInChildren<Transform>();
                        detect = false;
                        currentDest = 2;
                    }
                }
                
            }
        }


        //Gizmos.color = Color.white;
        //Debug.DrawLine(transform.position, transform.position + transform.forward * 3f);
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit, 4f))
        //{
        //    Gizmos.color = Color.yellow;
        //    Debug.DrawLine(transform.position, hit.point);
        //    return;
        //}
        
        if (isFollowing)
        {
            transform.position = follow.transform.position;
            return;
        }
        if (CurrentNode == null)
        {
            currentSpeed = .1f;
            transform.position += transform.forward * currentSpeed;
            return;
        }
        if (CurrentNode.GetComponent<Node>().stop) return;
        if (stop) return;
        //index check
        if (lastNode = currentDest == Destinations.Length)
        {
            //detect = true;
            return;
        };
        //move check
        if ((transform.position - Destinations[currentDest].position).magnitude < DestThres)
        {
            currentDest++;//index increment
            return;
        }

        //change in target speed
        if ((Destinations[Destinations.Length - 1].position - Destinations[0].position).magnitude > 20f)// if it is long distance drive
        {
            currentSpeed = .1f; 
            if ((Destinations[Destinations.Length - 1].position - transform.position).magnitude < 5f) currentSpeed = .05f; // stop at the end 
        }
        else currentSpeed = .05f; //if it is short distance drive like corners and intersections
        
        //move
        DestinationCurrent = Destinations[currentDest];
        transform.LookAt(DestinationCurrent,Vector3.up);
        transform.position += transform.forward * currentSpeed;
        
        
    }

}
