using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCross : MonoBehaviour {
    public GameObject[] lights;
    public Node[] nodes;
	// Use this for initialization
	void Start () {
		
	}
    public float signal;
    public float timer;
    public float interval;
    public bool green;
    public bool yellow;
    public bool red;
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            timer = 0f;
        }
        if (green = (timer >= 0 && timer < interval * (3f / 8))) signal = .25f;

        if (yellow = (timer >= interval * (3f / 8) && timer < interval * (4f / 8))) signal = .5f;

        if (red = (timer >= interval * (4f / 8) && timer < interval)) signal = .75f;
        foreach (Node n in nodes) {
            n.stop = red;
        }
        foreach (GameObject light in lights)
        {
            Renderer _myRenderer = light.GetComponent<Renderer>();
            Vector2 offset = new Vector2(.5f, signal);//0,.25 green 0,.75 red .25,.5 yellow
            _myRenderer.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
