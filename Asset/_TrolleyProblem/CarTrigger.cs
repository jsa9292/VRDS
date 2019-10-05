using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
	public bool trigger = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.name == "TrafficAITrigger") trigger = true;
	}
}
