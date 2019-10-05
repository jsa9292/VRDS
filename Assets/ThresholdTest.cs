using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThresholdTest : MonoBehaviour {
	public bool stop;
	public float roll;
	public float deltaRoll;
	public float rollStart;
	public float rollEnd;
	// Use this for initialization
	void Start () {
		
	}




	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			stop = !stop;
		}
		if ((Mathf.Abs (roll) >= Mathf.Abs (rollEnd)) || stop)
			return;
		else {
			roll += deltaRoll * Time.deltaTime;
		}
		QuPlaySimtools.QuSimtools_SendTelemetry(
			roll, //roll -32767 ~ 32767 car body rot
			0, //pitch -32767 ~ 32767 car body rot
			0, //heave
			0, //yaw
			0, //sway -32767 ~ 32767 accel
			0, //surge -32767 ~ 32767 accel
			0, //extra1
			0, //extra2
			0 //extra3
		);
		//alice tilting threshold: 500
		//jake tilting threshold: 800
	}
}
