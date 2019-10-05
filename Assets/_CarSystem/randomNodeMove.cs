using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomNodeMove : MonoBehaviour {
    public float timer;
    public Transform[] Nodes;
    public int destPoint = 0;


    // Use this for initialization
    void Start()
    {

        timer += Time.deltaTime;

    }


    // Update is called once per frame
    void Update()
    {
    }
}