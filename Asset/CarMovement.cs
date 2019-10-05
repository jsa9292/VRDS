using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {
    
    public float accel;
    public float brake;
    public float steer;
    public Vector3 velocity;
    public float angle;
    public float speed;
    public Transform car;
    public Transform target;
    public float distance;
    public Animator animate;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        car.position += transform.forward * accel * Time.deltaTime;
       // float step = speed * Time.deltaTime;
        transform.LookAt(target);
        distance = Vector3.Distance(transform.position, target.position);
        //Debug.Log(target.position);
        //if (distance  < 5) 
        //{
            //accel = distance; 
        //}
        if (distance < 0.5f)
            accel = 0;

        animate.SetFloat("Speed", Mathf.Clamp(accel, 0f, 2f));


        //transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        //      RaycastHit hit;

        //      Vector3 p1 = transform.position + charCtrl.center;
        //      float distanceToObstacle = 0;
        //      if (Physics.SphereCast(p1, charCtrl.height / 2, transform.forward, out hit, 10))
        ////for (int i = 0; i<2; i++)
        //      {
        //          distanceToObstacle = hit.distance;
        //          //wheels[i].motorTorque += accel * Time.deltaTime;
        //          //wheels[i].brakeTorque += brake * Time.deltaTime;
        //          //wheels[i].steerAngle = steer;
        //          //Vector3 pos;
        //          //Quaternion quat;
        //          //wheels[i].GetWorldPose(out pos, out quat);

        //          //car.AddForce(Vector3.up * 1000f);
        //          //velocity = car.velocity;
        //          //velocity2 = velocity;
        //          //speed = velocity.magnitude;
        //          //angle = Vector3.SignedAngle(velocity, transform.forward, Vector3.up);
        //      }
    }
}
