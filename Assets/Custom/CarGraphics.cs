using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGraphics : MonoBehaviour {
    public Transform control;
    public float tension;
    public float wheelSlip;
    public float stopDistance;
    public float checkDistance;
    public Vector3 distanceChange;
    public Vector3 targetPos;
    public Transform[] Wheels;
    public Transform[] frontWheelPivots;
    public GameObject signalLightLeft;
    public GameObject signalLightRight;
    public GameObject signalLightBrake;
    public Transform pivotCenter;
    private NodeFollower nf;
    public bool signalRight;
    public bool signalLeft;
    public bool signalBrake;
	// Use this for initialization
	void Start () {
        nf = control.GetComponent<NodeFollower>();

    }
	
	// Update is called once per frame
	void Update () {
        signalBrake = false;
        if (nf.CurrentNode != null) { 
            signalLeft = nf.CurrentNode.GetComponent<Node>().leftturn;
            signalRight = nf.CurrentNode.GetComponent<Node>().rightturn;
        }
        Gizmos.color = Color.white;
		Debug.DrawLine(transform.position, nf.transform.position);
        RaycastHit hit;
		Vector3 dir = nf.transform.position - transform.position;
		if (Physics.Raycast(transform.position, dir, out hit, dir.magnitude))
        {
            Gizmos.color = Color.yellow;
            Debug.DrawLine(transform.position, hit.point);
            if (hit.distance < stopDistance) return;
            if (hit.distance < stopDistance * 2f)
            {
                targetPos = hit.point - transform.forward * stopDistance;
                //nf.stop = true;
                //nf.transform.position = targetPos;
            }
            signalBrake = true;
            
        }
        else
        {
            nf.stop = false;
            targetPos = control.position;
        }
        if (nf.deactivate) signalBrake = true;
        signalLightLeft.SetActive(signalLeft);
        signalLightRight.SetActive(signalRight);
        signalLightBrake.SetActive(signalBrake);
        if ((transform.position - control.position).magnitude < 10f && (nf.CurrentNode == null || nf.lastNode)) nf.detect = true;
        distanceChange = (targetPos - transform.position) * tension * Time.deltaTime;
        if (distanceChange.magnitude < .001f) return;
        if (distanceChange.magnitude > .2f) distanceChange = distanceChange.normalized * .2f;
        transform.LookAt(targetPos,Vector3.up);
        transform.position += distanceChange;

        foreach (Transform w in Wheels) {
            w.Rotate(Vector3.right, distanceChange.magnitude*wheelSlip);
        }
        if ((transform.position-control.position).magnitude > (transform.position-pivotCenter.position).magnitude)pivotCenter.LookAt(control);
        foreach (Transform p in frontWheelPivots) {
            p.rotation = pivotCenter.rotation;
        }
	}
//	private void OnCollisionEnter(Collision col)
//    {
//		if (col.transform.name == "SportsCar")
//        {
//			col.transform.localEulerAngles += Vector3.right * 5f;
//            Destroy(gameObject);
//        }
//    }
}
