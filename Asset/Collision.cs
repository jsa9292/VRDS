using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter (Collider other)
    {
        Debug.Log("Car entered the trigger");
	}
	
	// Update is called once per frame
	void OnTriggerStay (Collider other)
    {
        Debug.Log("Car is within trigger");
        //other.transform.name;     
	}

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Object Exited the trigger");
    }
}
