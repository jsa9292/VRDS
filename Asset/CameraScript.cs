using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    private float distance = 1.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 4.0f;
    private float sensitivityY = 1.0f;

    // Use this for initialization
    void Start ()
    {
        camTransform = transform;
        cam = Camera.main;
    }
    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
    }
    // Update is called once per frame
    void LateUpdate ()
    {
        Vector3 dir = new Vector3(0, 2, /*-distance*/ -3);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);

    }
}
