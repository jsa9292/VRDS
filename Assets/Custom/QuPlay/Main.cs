using UnityEngine;
using UnityEngine.XR;

public class Main : MonoBehaviour
{
    public LogitechSteeringWheel Logitech;
    public Rigidbody Car;
    public Vector3 CenterOfMass;
    public GameObject steeringWheel;
    public WheelCollider[] Wheels;
    public Transform[] WheelsGraphics;
    public float engineRPM;
    public float idleEngineRPM;
    public float engineConst;
    public float[] gearConst;
    public int gear;
    
    public float velocity;
    public float angleVelocity;

    public float acceleration;
    public float angleAcceleration;

    private float lastVelocity;
    private float lastAngleVelocity;

    public float accelConst;
    public float brakeConst;
    public float steerConst;

    public AudioSource shift;
    public AudioSource engine;

    // Update is called once per frame
    private void Start()
    {
        foreach (WheelCollider wheel in Wheels)
        {
            wheel.ConfigureVehicleSubsteps(1000.0f, 20, 20);
        }
        Car.centerOfMass += CenterOfMass;
    }

    private void GearChange() {
        float startGear = gear;
        if (engineRPM > 4000f)
        {
            if (velocity > 30f) gear =1;
            if (velocity > 50f) gear =2;
            if (velocity > 75f) gear =3;
            if (velocity > 90f) gear =4;
            if (velocity > 135f) gear =5;
            if (velocity > 180f) gear =6;
            if (velocity > 205f) gear =7;
        }
        if (engineRPM < 2000f)
        {   
            if (velocity < 205f) gear =6;
            if (velocity < 180f) gear =5;
            if (velocity < 135f) gear =4;
            if (velocity < 90f) gear =3;
            if (velocity < 75f) gear =2;
            if (velocity < 50f) gear =1;
            if (velocity < 30f) gear =0;
        }
        if (startGear != gear)
        {
            //engineRPM = velocity * gearConst[gear];
            shift.Play();
        }
    }

    void FixedUpdate()
    {
        steeringWheel.transform.localEulerAngles = new Vector3(0, 0, Logitech.wheel * 270f);
        engineRPM *= .99f;
        engineRPM += Logitech.accel * engineConst;
        if (engineRPM < idleEngineRPM) engineRPM = idleEngineRPM;
        engine.pitch = 1f + engineRPM / 8000 * 2f;

        velocity = new Vector3 (Car.velocity.x, 0, Car.velocity.z).magnitude * 3600 / 1000;
        acceleration = (velocity - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = velocity;

        angleVelocity = Car.angularVelocity.magnitude;
        angleAcceleration = (angleVelocity - lastAngleVelocity) / Time.fixedDeltaTime;
        lastAngleVelocity = angleVelocity;


        //Reverse
        float reverse = Logitech.reverse ? -1f : 1f;

        //GearChange();
        Car.drag = gearConst[gear];

        for (int i = 0; i < Wheels.Length; i++)
        {
            //Front Wheel
            if (i < 2)
            {
                Wheels[i].steerAngle = Logitech.wheel * steerConst;
            }


            //Rear Wheel
            if (i > 1)
            {
                if (Logitech.brake < 0.001f) Wheels[i].motorTorque = Wheels[i].motorTorque * .995f + (engineRPM * .005f * reverse); //Wheels[i].motorTorque * .999f +
                if (Logitech.brake > 0.001f)
                {
                    Wheels[i].motorTorque -= Logitech.brake *reverse* brakeConst;
                    if (Wheels[i].motorTorque*reverse < 0) Wheels[i].motorTorque = 0f;
                }
               
               //Debug.Log(Wheels[3].motorTorque);
            }

            //Brake
            //Wheels[i].brakeTorque = Logitech.brake * brakeConst;
            
            //Wheel graphics
            Vector3 pos;
            Quaternion quat;
            Wheels[i].GetWorldPose(out pos, out quat);
            WheelsGraphics[i].SetPositionAndRotation(pos, quat);
            //Debug.Log(Wheels[i].isGrounded);
        }
        roll = Car.transform.localEulerAngles.z * rollConst;
        pitch = Car.transform.localEulerAngles.x * pitchConst;
        sway = sway * .95f + (angleAcceleration * swayConst)*.05f;
        //surge = surge * .98f + (Logitech.accel -Logitech.brake)* 32767f *reverse * .02f;
         surge = surge *.95f + ((acceleration) * surgeConst)*.05f; 

        //surge /= .95f;
        //surge += (Logitech.accel - Logitech.brake) * 327f;
        QuPlaySimtools.QuSimtools_SendTelemetry(
            roll, //roll -32767 ~ 32767 car body rot
            pitch, //pitch -32767 ~ 32767 car body rot
            0, //heave
            0, //yaw
            sway, //sway -32767 ~ 32767 accel
            surge, //surge -32767 ~ 32767 accel
            0, //extra1
            0, //extra2
            0);//extra3

    }

    public float surgeGain, d_surge;
    public float roll, pitch, heave, yaw, sway, surge, extra1, extra2, extra3;

    public float rollConst;
    public float pitchConst;
    public float swayConst;
    public float surgeConst;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.XR.InputTracking.Recenter();
        }

    }
   }
