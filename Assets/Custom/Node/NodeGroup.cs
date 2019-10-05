using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGroup : MonoBehaviour {
    public List<NodeFollower> waiting;
    	
	// Update is called once per frame
	void FixedUpdate () {
        foreach (NodeFollower nf in waiting) {
            nf.deactivate = true;
        }
        if (waiting.Count == 1) waiting[0].deactivate = false;
        if (waiting.Count > 0)
        {
            
            if (waiting[0].stop)
            {
                NodeFollower bad = waiting[0];
                waiting.Remove(waiting[0]);
                waiting.Add(bad);
            }else waiting[0].deactivate = false;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        NodeFollower entered;
        entered = other.GetComponent<NodeFollower>();
        if (entered != null)
        {
            waiting.Add(entered);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        NodeFollower exiting;
        exiting = other.GetComponent<NodeFollower>();
        if (exiting != null && waiting.Contains(exiting)) {
            waiting.Remove(exiting);
        }
    }
}
