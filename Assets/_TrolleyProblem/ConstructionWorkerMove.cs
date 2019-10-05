using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionWorkerMove : MonoBehaviour
{
	public Transform ConstructionWorker;
	public float speed; 
	public float distance;
	public float accel;
	public Transform target;
	public List<Transform> Waypoints;
	public Animator animate;
	public bool active = false;
	public bool inTrigger;
	public Collider CarCollider;

	public TPEvents EventsManager;


    // Update is called once per frame
    void Update()
    {
		if (!(EventsManager.timer > 10f)) return;
		ConstructionWorker.position += transform.forward * accel * Time.deltaTime;
		transform.LookAt(target);
		distance = Vector3.Distance(transform.position, target.position);

		if (distance < 0.1f)
		{
		int i = target.GetSiblingIndex();
		if (i + 1 < target.transform.parent.childCount) target = target.transform.parent.GetChild(i + 1);
		//if (i + 1 == target.transform.parent.childCount) target = target.transform.parent.GetChild(0);
			accel = 0.5f;

			if (target == target.transform.parent.GetChild(3))
			{
				accel = 0f;
				animate.SetFloat("Speed", Mathf.Clamp(accel, 0f, 0f));
			}
		}
	}
}
