using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianTrigger : MonoBehaviour
{
    public GameObject mycollider;
    public bool inTrigger;

	void Start(){
		mycollider = transform.GetChild(0).gameObject;
		mycollider.SetActive(false);
	}

    // Use this for initialization
    void OnTriggerEnter(Collider other) 
    {
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
		if (other.GetComponent<PedestrianMove>()){
			mycollider.SetActive(true);
		}
    }

    void OnTriggerExit(Collider other)
    {
		if (other.GetComponent<PedestrianMove>()){
			mycollider.SetActive(false);
		}
    }

}
