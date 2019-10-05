using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyProblemTrigger : MonoBehaviour
{
	public bool inTrigger = true;
	public GameObject car; 
	public List<Transform> AICars;
	public float accel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
	void OnTriggerEnter(Collider other)
    {
		Debug.Log("Car has entered the trigger");
		accel = 0; 
        //bring car to stop
		//traffic in opposite direction begins - 2 cars
		//
    }

	void OnTriggerStay(Collider other)
	{
		inTrigger = true;

		if (inTrigger == true)
		{
			
			//wait until 2 AI cars pass, now move stop guy to his waypoint
			//Construction guy starts walking to his waypoints? 

		}
	}

	void OnTriggerExit(Collider exit)
	{
		inTrigger = false;

		if (inTrigger == false)
		{
			//brakes stop working
			//Construction guy starts walking to his waypoints?
		}

	}

}
