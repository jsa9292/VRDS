using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeSystem : MonoBehaviour {
    int n;
    public Color color;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        n = transform.childCount;

        for (int i = 0; i < n; i++)
        {

            if (i + 1 < n)
            {
				Debug.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position, color);
            }

        }

		Debug.DrawLine(transform.GetChild(0).position, transform.GetChild(n - 1).position, color);
    }


}