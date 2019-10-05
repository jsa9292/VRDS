using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPMove : MonoBehaviour
{
	public Animator TPanim;

	public Transform target;

	public float speed;
	public float distance;

	public TPEvents EventsManager;

	public List<Transform>Waypoints;


    // Update is called once per frame
    void Update()
    {
		if (!(EventsManager.timer > 10f)) return;

		distance = Vector3.Distance(transform.position, target.position);
		if (distance < 0.1f)
		{
			
			int i = target.GetSiblingIndex();
			if (i == target.transform.parent.childCount) return;
			if (i +1 < target.transform.parent.childCount) 
			{
				target = target.transform.parent.GetChild(i + 1) ;
				speed = 0.5f;
			}

			if (target == target.transform.parent.GetChild(3))
			{
				speed = 0.4f;
				TPanim.SetFloat("Speed", Mathf.Clamp(speed, 0f, 0f));
			}
    	}

		transform.position += transform.forward * speed * Time.deltaTime;
		transform.LookAt(target);


	}
}
