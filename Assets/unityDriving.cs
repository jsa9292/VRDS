using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unityDriving : MonoBehaviour {
	public UnityStandardAssets.Vehicles.Car.CarController carControl;
	public LogitechSteeringWheel Logitech;
	public Rigidbody Car;
	public Text speedDisplay;
	public GameObject steerWheel;
	public GameObject rearCam;

	[Header("Pedals")]
	public float acceleration;
	public float brake;
	public float accelConst;
	public float brakeConst;

	[Header ("Current Vehicle State")]
	public float CurrentSpeed;
	public float CurrentSteerAngle;
	public float CurrentAccel;
	public float CurrentAccel_z;
	public float CurrentAccel_x;
	public float angularVelocity;
	public float angularAcc;

	[Header ("Previous Vehicle State")]
	private float previousSpeed;
	private float previousAccel_z;
	private float previousAccel_x;
	private float previousAngularV;
	private float previousAngularA;

	[Header ("Filter Parameters")]
	public float Lin_z_low_coeff;
	public float Lin_z_high_coeff;
	public float Lin_x_low_coeff;
	public float Lin_x_high_coeff;
	public float Ang_y_high_coeff;

	[Header ("Filtered")]
	public float z_lin_low;
	public float delta_z_low;
	public float z_lin_high;
	public float delta_z_high;
	public float x_lin_low;
	public float delta_x_low;
	public float x_lin_high;
	public float delta_x_high;
	public float y_ang_high;
	public float delta_y_high;

	[Header ("Vehicle Configuration")]
	public float defaultAccel;
	public float MaxSpeed;
	public float MaxSteerAngle;
	private float reverse; 
	private float neutral;
	[Header ("Thresholds")]
	public float pitch_thres_low;
	public float pitch_thres_high;
	public float roll_thres_low;
	public float roll_thres_high;
	public float yaw_thres;
	[Header ("Simulator Inputs")]
	public float bodyConst;
	public float pitch_low_const, pitch_high_const;
	public float roll_low_const, roll_high_const;
	public float yaw_high_const;
	public float bodyroll, bodypitch, roll, pitch, yaw;

	public float roll_new, pitch_new, yaw_new;
	// Use this for initialization
	void Start () {
		MaxSteerAngle = carControl.m_MaximumSteerAngle;
		//Physics.autoSimulation = false;
	}
	
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		//Physics.Simulate (dt);
	
		acceleration = (defaultAccel + Logitech.accel * .9f) * neutral;
		acceleration = Mathf.Pow(acceleration,2f) * accelConst;
		brake = Mathf.Pow (Logitech.brake, 2f)* brakeConst;
		CurrentSteerAngle = carControl.CurrentSteerAngle;
		MaxSpeed = carControl.MaxSpeed;
		reverse = Logitech.reverse ? -.2f : 1f;
		neutral = Logitech.neutral ? 0f : 1f;

		carControl.Move (Logitech.wheel, acceleration, brake, 0 , reverse);


		bodyroll = (90f-Vector3.Angle(Vector3.up,Car.transform.right)) * bodyConst;
		bodypitch = (90f-Vector3.Angle(Vector3.up,Car.transform.forward)) * bodyConst;

		//angularVelocity = Vector3.Project(Car.angularVelocity,Car.transform.forward).y;

		//Linear Acceleration Filters
		CurrentSpeed = carControl.CurrentSpeed;
		angularVelocity = Car.angularVelocity.y;
		if (CurrentSpeed != previousSpeed) CurrentAccel = CurrentSpeed - previousSpeed; 
		CurrentAccel_z = CurrentAccel *Mathf.Sin(angularVelocity);
		CurrentAccel_x = CurrentAccel *Mathf.Cos(angularVelocity);

		//Linear_z LowPass
		delta_z_low = LowPassFilter(CurrentAccel_z, previousAccel_z, Lin_z_low_coeff) *roll_low_const;
		delta_z_low = delta_z_low - z_lin_low;
		delta_z_low = Mathf.Clamp (delta_z_low, -pitch_thres_low, pitch_thres_low);
		z_lin_low += delta_z_low;

		delta_z_high = HighPassFilter(CurrentAccel_z, previousAccel_z, Lin_z_high_coeff, Time.deltaTime) *roll_high_const;
		delta_z_high = delta_z_high - z_lin_high;
		delta_z_high = Mathf.Clamp (delta_z_high, -pitch_thres_high, pitch_thres_high);
		z_lin_high += delta_z_high;

		roll = z_lin_low + z_lin_high + bodyroll;

		//Linear_x LowPass
		delta_x_low = LowPassFilter(CurrentAccel_x, previousAccel_x, Lin_x_low_coeff) *pitch_low_const;
		delta_x_low = delta_x_low - x_lin_low;
		delta_x_low = Mathf.Clamp (delta_x_low, -pitch_thres_low, pitch_thres_low);
		x_lin_low += delta_x_low;

		delta_x_high = HighPassFilter(CurrentAccel_x, previousAccel_x, Lin_x_high_coeff, Time.deltaTime) *pitch_high_const;
		//delta_x_high = delta_x_high - x_lin_high;
		delta_x_high = Mathf.Clamp (delta_x_high, -pitch_thres_high, pitch_thres_high);
		x_lin_high = delta_x_high;

		pitch = x_lin_low + x_lin_high + bodypitch;

		//Angular_y HighPass
		y_ang_high = HighPassFilter(Car.angularVelocity.y,previousAngularV, Ang_y_high_coeff, Time.deltaTime)*yaw_high_const;
		y_ang_high = Mathf.Clamp (y_ang_high, -yaw_thres, yaw_thres);
		yaw = y_ang_high; 


		//sim safty
		roll = Mathf.Clamp (roll, -32767f, 32767f);
		pitch = Mathf.Clamp (pitch, -32767f, 32767f);
		yaw = Mathf.Clamp (yaw, -32767f, 32767f);

		QuPlaySimtools.QuSimtools_SendTelemetry (
				roll, //roll -32767 ~ 32767 car body rot
				-pitch, //pitch -32767 ~ 32767 car body rot
				0, //heave
				0, //yaw
				0, //sway -32767 ~ 32767 accel
				0, //surge -32767 ~ 32767 accel
				yaw, //extra1
				0, //extra2
				0 //extra3
			);
		rearCam.SetActive (Logitech.reverse);
		steerWheel.transform.localEulerAngles = new Vector3 (-18f, 0,Logitech.wheel*450f);
		speedDisplay.text = ((int) carControl.CurrentSpeed).ToString();

		if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
		if (Logitech.HomeButton || Input.GetKeyDown(KeyCode.Space))
		{
			UnityEngine.XR.InputTracking.Recenter();
		}

		//Update previous
		previousSpeed = CurrentSpeed;
		previousAccel_z = CurrentAccel_z;
		previousAccel_x = CurrentAccel_x;
		previousAngularV = Car.angularVelocity.y;
	}
	float LowPassFilter (float current, float before, float coeff1) {
		return (before * coeff1 + current * (1 - coeff1));
		
	}
	float HighPassFilter (float current, float before, float coeff2, float step) {
		float alpha = coeff2 / (coeff2 + step);
		return (before + (current - before) * alpha);
	}
}
