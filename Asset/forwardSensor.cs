using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardSensor : MonoBehaviour {
    public RaycastHit hit;
    public bool flag;
    public float output;
    public float sensorLength;
    public Collider myCollider;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (flag = Physics.Raycast(transform.position, transform.forward, out hit, sensorLength))
        {
            if (hit.collider.tag != "Obstacle" || hit.collider == myCollider)
            {
                return;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, transform.forward * (sensorLength + transform.localScale.z));
    }
}
