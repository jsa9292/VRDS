using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInScript : MonoBehaviour {

    public GameObject blinder;
    public Material mat; 

	// Use this for initialization
	void Start () {
        blinder = GetComponent<GameObject>();
        Color c = blinder.GetComponent<MeshRenderer>().material.color;
        c.a = 0f;
        blinder.GetComponent<MeshRenderer>().material.color = c;
	}

    // Update is called once per frame
    private IEnumerator FadeIn()
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = blinder.GetComponent<MeshRenderer>().material.color;
            c.a = f;
            blinder.GetComponent<MeshRenderer>().material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    } 

     void startFading ()
    {
        StartCoroutine ("OnBecameInvisible");
    }
}
