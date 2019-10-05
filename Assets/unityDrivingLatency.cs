using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;
public class unityDrivingLatency : MonoBehaviour {
	public string NextScene;
	public UnityStandardAssets.Vehicles.Car.CarController carControl;
	public LogitechSteeringWheel Logitech;
	public Rigidbody Car;
	public Transform driverPos;
	public Text speedDisplay;
	public GameObject steerWheel;
	public GameObject rearCam;
	public float globalScale;
	public float accelScale;
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
	public float angularAccel;

	[Header ("Previous Vehicle State")]
	private float previousSpeed;
	private float previousAccel_z;
	private float previousAccel_x;
	public float previousAngularV;
	private float previousAngularA;

	[Header ("Filter Parameters")]
	public float Lin_z_low_coeff;
	public float Lin_x_low_coeff;
	//public float z_lin_rate;
	//public float x_lin_rate;
	public float Lin_z_high_coeff;
	public float Lin_x_high_coeff;
	public float Ang_y_high_coeff;

	[Header ("Filtered")]
	public float z_lin_low;
	//public float delta_z_low;
	public float z_lin_high;
	//public float delta_z_high;
	public float x_lin_low;
	//public float delta_x_low;
	public float x_lin_high;
	//public float delta_x_high;
	public float y_ang_high;
	//public float delta_y_high;

	[Header ("Vehicle Configuration")]
	public float defaultAccel;
	public float MaxSpeed;
	public float MaxSteerAngle;
	private float reverse; 
	private float neutral;

	[Header ("Simulator Inputs")]

	public float Bumpyness;
	public float Euler;
	public float Coriolis;
	public float Centrifugal;
	public float bodyConst;
	public float pitch_const;
	public float roll_const;
	public float yaw_const;
	public float pitch_rate_max, pitch_rate_low,yaw_rate;
	public float washOutYaw;

	public float bodyroll, bodypitch;
	public float roll, roll_new;
	public float pitch, pitch_new, pitch_default;
	public float yaw, yaw_new;

	public List<float> pitchs = new List<float>();
	public List<float> rolls = new List<float>();
	public List<float> yaws = new List<float>();

	public List<long> renderTicks = new List<long>();
	public List<long> simTicks = new List<long>();
	public long startTick;

	public float previousAccel_Input;
	public float previousAccel;
	//public List<float> speeds = new List<float>();
	//public List<float> angSpeeds = new List<float>();
	//public List<Vector3> positions = new List<Vector3>();
	//public List<float> times = new List<float>();
	//public int sampleSize;
	void Awake(){
		Physics.autoSimulation = false;
		QualitySettings.vSyncCount = 1;
		QualitySettings.maxQueuedFrames = 0;
		Application.targetFrameRate = 90;
	}

	// Use this for initialization
	void Start () {
		MaxSteerAngle = carControl.m_MaximumSteerAngle;
		MaxSpeed = carControl.MaxSpeed;
		startTick = DateTime.Now.Ticks;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//speeds.Add(Car.velocity.magnitude);
		//angSpeeds.Add(Car.angularVelocity.y);
		//if(positions.Count>sampleSize) positions.RemoveAt(0);
		//positions.Add(Car.transform.position);
		//if(times.Count>sampleSize) times.RemoveAt(0);
		//times.Add(Time.time);

	}

	void Update(){
		reverse = Logitech.reverse ? -.2f : 1f;
		neutral = Logitech.neutral ? 0f : 1f;
		//acceleration = (defaultAccel + Logitech.accel * .9f) * neutral* accelConst;
		if(!Logitech.neutral) {
			acceleration = 1;
		}
		else {
			acceleration = 0;
		}
		brake = Mathf.Pow (Logitech.brake, 2f)*Mathf.Clamp(CurrentSpeed,1f,10f)/10f;
		acceleration = LowPassFilter(acceleration, previousAccel_Input, .5f, 1f);

//		if (simTicks.Count>3) simTicks.RemoveAt(0);
//		simTicks.Add(DateTime.Now.Ticks - startTick);
		//speeds.Clear();
		carControl.Move (Logitech.wheel, acceleration, brake, 0 , reverse);
		Physics.Simulate(Time.deltaTime);


		//if (positions.Count == sampleSize+1) {
		//	for (int i = 1; i<sampleSize; i++){
		//		speeds.Add( (positions[i]-positions[i-1]).magnitude/(times[i]-times[i-1]) );
		//	}
		//}else return;
		//Physics.Simulate(Time.deltaTime);
		//if(speeds.Count() > 0)CurrentSpeed = speeds.Average();
		//if(angSpeeds.Count() > 0)angularVelocity = angSpeeds.Average();

		//speeds.Clear();
		//angSpeeds.Clear();
		CurrentSpeed = Car.GetPointVelocity(transform.TransformPoint(driverPos.localPosition)).magnitude;// speeds.Average();
		angularVelocity = Car.angularVelocity.y;//CurrentSteerAngle*Mathf.PI /180f;
		//Car.drag = 0.2f - carControl.m_GearNum*.05f;

		speedDisplay.text =((int) (carControl.CurrentSpeed*2.23693629f)).ToString();
		CurrentSteerAngle = carControl.CurrentSteerAngle;
		steerWheel.transform.localEulerAngles = new Vector3 (-18, 0,Logitech.wheel*450f);

		//float pitch_prev = pitch_new;
		//float pitch_diff = pitch-pitch_prev;
		//pitch_diff = Mathf.Clamp(pitch_diff, - pitch_rate_max,  pitch_rate_max);
		CurrentAccel = CurrentSpeed-previousSpeed;
		angularAccel = angularVelocity - previousAngularV;
		//CurrentAccel = LowPassFilter(CurrentAccel, previousAccel, .5f, 1f);

		//if(CurrentAccel== 0f) return;
		CurrentAccel_z = CurrentAccel *Mathf.Sin(angularVelocity); //right
		CurrentAccel_x = CurrentAccel *Mathf.Cos(angularVelocity); //forward
		bodyroll = (90f-Vector3.Angle(Vector3.up,Car.transform.right)) * bodyConst;
		bodypitch = (90f-Vector3.Angle(Vector3.up,Car.transform.forward)) * bodyConst;
		//Debug.Log(Car.inertiaTensor);


		//Linear_x
		//x_lin_low = LowPassFilter(CurrentAccel_x, previousAccel_x, Lin_x_low_coeff,   2f*Time.deltaTime);
		//x_lin_high = HighPassFilter(CurrentAccel_x, previousAccel_x, Lin_x_high_coeff, Time.deltaTime, 0.02f);
		//pitch = (x_lin_low + x_lin_high)* pitch_const;// + x_lin_high
		pitch = CurrentAccel_x *pitch_const;//(previousAccel_x - CurrentAccel_x)*pitch_const;
		pitch += CurrentSpeed/MaxSpeed;// * UnityEngine.Random.Range(-1f,1f)* Bumpyness;
		//pitch += Euler * angularAccel * CurrentSpeed / angularVelocity;
		//pitch += -Coriolis * Mathf.Abs(angularVelocity) * CurrentSpeed;
		//Linear_z 
		//z_lin_low = LowPassFilter(CurrentAccel_z, previousAccel_z, Lin_z_low_coeff,  100f);
		//z_lin_high = HighPassFilter(CurrentAccel_z,previousAccel_z, Lin_z_high_coeff, Time.deltaTime, 0.02f);
		roll =CurrentAccel_z*roll_const;//(z_lin_low+z_lin_high) * roll_const;
		//roll += CurrentSpeed/MaxSpeed * UnityEngine.Random.Range(-1f,1f)* Bumpyness;
		roll += Centrifugal *angularVelocity * CurrentSpeed;

		yaw += angularVelocity * yaw_const;

		if (Mathf.Abs(yaw)>washOutYaw){
			yaw -= Mathf.Sign(yaw)* washOutYaw;
		}
		else {
			yaw = 0;
		}

		float accelAmp = pitch_new>0 ? accelScale:1f;
		pitch_new = pitch*globalScale*accelAmp;
		roll_new = roll*globalScale;
		yaw_new = yaw*globalScale;

		roll_new = Mathf.Clamp (roll_new , -30000f, 30000f);
		pitch_new = Mathf.Clamp (pitch_new, -30000f, 30000f);
		yaw_new = Mathf.Clamp (yaw_new, -30000f, 30000f);
		//rearCam.SetActive (Logitech.reverse);



		if (pitchs.Count>3) pitchs.RemoveAt(0);
		pitchs.Add(pitch_new+bodypitch+pitch_default);
		if (rolls.Count>3) rolls.RemoveAt(0);
		rolls.Add(roll_new+bodyroll);
		if (yaws.Count>3) yaws.RemoveAt(0);
		yaws.Add(yaw_new);

		//Debug.Log(pitchs.Count);
//		float r2f = (simTicks[3]-renderTicks[3]); //render to fixed
//		float u2r = (renderTicks[3]-simTicks[2]); //update to render
//		float i2s = 1000000; //ticks 1ms = 1000us = 1000,000ns 
//		if(!Logitech.neutral){
//			QuPlaySimtools.QuSimtools_SendTelemetry (
//				(rolls[2]*(r2f+i2s)+rolls[3]*u2r)/(r2f+i2s+u2r), //roll -32767 ~ 32767 car body rot
//				-(pitchs[2]*(r2f+i2s)+pitchs[3]*u2r)/(r2f+i2s+u2r), //pitch -32767 ~ 32767 car body rot
//				0, //heave
//				0, //yaw
//				0, //sway -32767 ~ 32767 accel
//				0, //surge -32767 ~ 32767 accel
//				(yaws[2]*(r2f+i2s)+yaws[3]*u2r)/(r2f+i2s+u2r),//-(yaw *yaw_const)*globalScale, //extra1
//				0, //extra2
//				0 //extra3
//			);
//		}

		//Update previous
		previousSpeed = CurrentSpeed;
		previousAccel= CurrentAccel;
		previousAccel_z = CurrentAccel_z;
		previousAccel_x = CurrentAccel_x;
		previousAngularV = angularVelocity;
		previousAccel_Input = acceleration;

		if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene(NextScene);
		}
		if (Logitech.HomeButton || Input.GetKeyDown(KeyCode.Space))
		{
			UnityEngine.XR.InputTracking.Recenter();
		}
		StartCoroutine(RenderTime());
		//Debug.Log(simTicks[3]-renderTicks[3]);
	}
	public IEnumerator RenderTime(){
		yield return new WaitForEndOfFrame();
//		if (renderTicks.Count>3) renderTicks.RemoveAt(0);
//		renderTicks.Add(DateTime.Now.Ticks - startTick);
		if(!Logitech.neutral){
			QuPlaySimtools.QuSimtools_SendTelemetry (
				rolls[3],//(rolls[2]*(r2f+i2s)+rolls[3]*u2r)/(r2f+i2s+u2r), //roll -32767 ~ 32767 car body rot
				-pitchs[3],//-(pitchs[2]*(r2f+i2s)+pitchs[3]*u2r)/(r2f+i2s+u2r), //pitch -32767 ~ 32767 car body rot
				0, //heave
				0, //yaw
				0, //sway -32767 ~ 32767 accel
				0, //surge -32767 ~ 32767 accel
				yaws[3],//(yaws[2]*(r2f+i2s)+yaws[3]*u2r)/(r2f+i2s+u2r),//-(yaw *yaw_const)*globalScale, //extra1
				0, //extra2
				0 //extra3
			);
		}
		yield return null;

	}
	float LowPassFilter (float current, float before, float coeff1, float max) {
		float temp = before * coeff1 +current* (1-coeff1);
		temp = Mathf.Clamp (temp, -max, max);
		return (temp);
		
	}
	float HighPassFilter (float current, float before, float coeff2, float step, float max) {
		float alpha = coeff2 / (coeff2 + step);
		float temp = (current - before) * alpha;
		return (Mathf.Clamp(temp,-max,max));
	}
}
