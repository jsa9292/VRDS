using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour {
    public float radius;
    public bool stop;
    public bool rightturn;
    public bool leftturn;
    public bool straight;
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void OnDrawGizmos () {
        Gizmos.color = Color.magenta;
        for (int i = 0; i < transform.childCount; i++) {
            Gizmos.DrawSphere(transform.GetChild(i).position, radius-i/20f);
            if (i+1 == transform.childCount) return;
            Vector3 position1 = transform.GetChild(i).position;
            Vector3 position2 = transform.GetChild(i + 1).position;
            Gizmos.DrawLine(position1, position2);
            Gizmos.color*=.75f;
        }
	}
    
}
