using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCar : MonoBehaviour {

    public NavMeshAgent nma;
    public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        nma.SetDestination(target.position);
		
	}
}
