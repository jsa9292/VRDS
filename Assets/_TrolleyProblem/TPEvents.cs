using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPEvents : MonoBehaviour
{
	public bool Participant_inTrigger;
//	public bool AI_Car1_Move;
//	public bool AI_Car2_Move;
//	public bool AI_Car3_Move;
//	public bool AI_Cars_gone;
//	public bool Start_Trolley;

//	public GameObject StopGuy;
//	public GameObject ConstructionWorker;
//	public GameObject AICar1;
//	public GameObject AICar2;
//	public GameObject AICar3;

	public float timer;
//	public float accel;

//	public Transform AICartarget;



    // Start is called before the first frame update
    void Start()
    {

    }
	void OnTriggerEnter(Collider other)
	{
		Participant_inTrigger = true;
		Debug.Log ("Car has entered the trigger");
	}

	void OnTriggerStay(Collider other)
	{
		Participant_inTrigger = true;
	}

	void OnTriggerExit(Collider other)
	{
		Participant_inTrigger = false;
	}

    // Update is called once per frame
    void Update()
    {
		if (Participant_inTrigger)
			//start timer
		{
			timer += Time.deltaTime;
		}
//		if (Stop_Guy_Stops)
//		{
//			//play animation for stopping participant and confirm
//			StopGuyAnimate.Play("", 0, 0);
//		}
//		if (AI_Car1_Move)
//			//move Car1
//		{
//			AICar1.position += transform.forward * accel * Time.deltaTime;
//			transform.LookAt(AICarTarget);
//			distance = Vector3.Distance(transform.position, AICarTarget.position);
//		}
//		if (AI_Car2_Move)
//		{
//			//move Car2
//			AICar2.position += transform.forward * accel - 0.5f * Time.deltaTime;
//			transform.LookAt(AICarTarget);
//			distance = Vector3.Distance(transform.position, AICarTarget.position);
//		}
//		if (AI_Car3_Move)
//		{
//			//move Car3
//			AICar3.position += transform.forward * accel/2f * Time.deltaTime;
//			transform.LookAt(target);
//			distance = Vector3.Distance(transform.position, target.position);
//		}
//		if (AI_Cars_gone)
//		{
//			//Cars Gone
//		}
//		if (Start_Trolley)
//		{
//			//construction worker and the stop guy both moves
//		}
//
    }
}
