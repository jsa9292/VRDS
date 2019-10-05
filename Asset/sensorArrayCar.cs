using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorArrayCar : MonoBehaviour {
        public forwardSensor[] sensors;
        public bool debug;
        public float sensorLength = 5.0f;
        public float speed = 10.0f;
        public float directionValue = 1.0f;
        public float turnValue = 3.0f;
        public float turnSpeed = 50.0f;
        public bool front, back, left, right;
        Collider myCollider;
        // Use this for initialization
        

        public int flag;
        // Update is called once per frame
        void Update()
        {

        foreach (forwardSensor sensor in sensors) {
            if (sensor.flag) {
                turnValue -= sensor.transform.localPosition.x * Time.deltaTime;
            }

        }

            turnValue *= 1 - Time.deltaTime;

            transform.Rotate(Vector3.up * (turnSpeed * turnValue) * Time.deltaTime);
            transform.position += transform.forward * (speed * directionValue) * Time.deltaTime;

        }


    
    }


