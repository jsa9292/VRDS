using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
	private Material m;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        m = transform.GetComponent<Renderer>().material;
        Color color = m.color;
        color.a = (Mathf.Sin(Time.time)+1)/2; //change this value for fading
        m.color = color;
    }
}
