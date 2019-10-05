using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour {
    public bool debug;
    public float sensorLength = 5.0f;
    public float speed = 10.0f;
    public float directionValue = 1.0f;
    public float turnValue = 3.0f;
    public float turnSpeed = 50.0f;
    public bool front, back, left, right;
    Collider myCollider;
	// Use this for initialization
	void Start () {
        myCollider = transform.GetComponent<Collider>();
	}
	
    public int flag;
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        //Right Sensor
        if (right=Physics.Raycast(transform.position, transform.right + transform.forward * .5f, out hit, (sensorLength + transform.localScale.x*2)))
        {
            if (hit.collider.tag != "Obstacle" || hit.collider == myCollider)
            {
                return;
            }
            
            turnValue = .1f;
            flag++;
        }
        //Left Sensor
        if (left=Physics.Raycast(transform.position, -transform.right+transform.forward*.5f, out hit, (sensorLength + transform.localScale.x*2)))
        {
            if (hit.collider.tag != "Obstacle" || hit.collider == myCollider)
            {
                return;
            }
            
            turnValue += .1f;
            flag++;
        }
        //Front Sensor
        if (front=Physics.Raycast(transform.position, transform.forward, out hit, (sensorLength + transform.localScale.z)))
        {
            if (hit.collider.tag != "Obstacle" || hit.collider == myCollider)
            {
                return;
            }
            if (directionValue == 1.0f)
            {
                directionValue = -1;
            }
            flag++;
        }
        //Back Sensor
        if (back=Physics.Raycast(transform.position, -transform.forward, out hit, (sensorLength + transform.localScale.z)))
        {
            if (hit.collider.tag != "Obstacle" || hit.collider == myCollider)
            {
                return;
            }

            if (directionValue == -1.0f)
            {
                directionValue = 1;
            }
            flag++;
        }
        if (flag == 0)
        {
            turnValue = 0;
        }
        turnValue *= 1 - Time.deltaTime;

        transform.Rotate(Vector3.up * (turnSpeed * turnValue) * Time.deltaTime);
        transform.position += transform.forward * (speed*directionValue) * Time.deltaTime;

	}


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, transform.forward * (sensorLength + transform.localScale.z));
        Gizmos.DrawRay(transform.position, -transform.forward * (sensorLength + transform.localScale.z));
        Gizmos.DrawRay(transform.position, transform.right + transform.forward * .5f * (sensorLength + transform.localScale.x*2));
        Gizmos.DrawRay(transform.position, -transform.right + transform.forward * .5f * (sensorLength + transform.localScale.x*2));

    }
}
