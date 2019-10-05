using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
	public ConstructionWorkerMove constructionWorker;
	public CarTrigger ct;

	void Start()
	{
		
	}

	void Update()
	{
		if (ct.trigger)
		{
			constructionWorker.active = true;
		}
	}
}
