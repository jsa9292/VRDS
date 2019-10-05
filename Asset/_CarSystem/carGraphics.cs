using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carGraphics : MonoBehaviour {

    public Transform target;

    float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(target.position);
        transform.Translate(0f, 0f, speed * Time.deltaTime);
	}
}
