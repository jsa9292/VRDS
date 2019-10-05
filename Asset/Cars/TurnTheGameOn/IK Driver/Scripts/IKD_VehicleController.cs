using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace TurnTheGameOn.IKDriver{

	public class IKD_VehicleController : MonoBehaviour {

		public enum Joystick {
			None, JoystickButton0, JoystickButton1, JoystickButton2, JoystickButton3, JoystickButton4,
			JoystickButton5, JoystickButton6, JoystickButton7, JoystickButton8, JoystickButton9, JoystickButton10,
			JoystickButton11, JoystickButton12, JoystickButton13, JoystickButton14, JoystickButton15, JoystickButton16,
			JoystickButton17, JoystickButton18, JoystickButton19
		}
		public enum CarDriveType {
			FrontWheelDrive,
			RearWheelDrive,
			FourWheelDrive
		}
		public enum SpeedType {
			MPH,
			KPH
		}
		public enum MobileSteeringType {
			UIButtons,
			Tilt,
			UIJoystick,
			UISteeringWheel
		}

		#region Public Variables
		//Toggle mobile controls on or off.
		public bool mobileController;
		public MobileSteeringType mobileSteeringType;

		public string steeringAxis = "Horizontal";
		public string throttleAxis = "Vertical";

		public KeyCode eBrakeKey;
		public KeyCode eBrakeJoystick;
		[HideInInspector] public Joystick _eBrakeJoystick;

		//KeyCode used for camera switch key.
		public KeyCode cameraSwitchKey;
		//KeyCode used for nitro key.
		public KeyCode nitroKey;
		//KeyCode used for shift up key.
		public KeyCode shiftUpKey;
		//KeyCode used for shift down key.
		public KeyCode shiftDownKey;
		//KeyCode used for look back key.
		public KeyCode lookBackKey;
		//KeyCode used for camera switch joystick button.
		public KeyCode cameraSwitchJoystick;
		[HideInInspector] public Joystick _cameraSwitchJoystick;
		//KeyCode used for nitro joystick button.
		public KeyCode nitroJoystick;
		[HideInInspector] public Joystick _nitroJoystick;
		//KeyCode used for shift up joystick button.
		public KeyCode shiftUpJoystick;
		[HideInInspector] public Joystick _shiftUpJoystick;
		//KeyCode used for shift down joystick button.
		public KeyCode shiftDownJoystick;
		[HideInInspector] public Joystick _shiftDownJoystick;
		//KeyCode used for look back joystick button.
		public KeyCode lookBackJoystick;
		[HideInInspector] public Joystick _lookBackJoystick;
//UI & HUD Lookup
		public GameObject HUDPrefab;
		public GameObject mobilePrefab;
		public string parentCanvas = "IK Driver Demo Canvas";
		public GameObject backupCanvas;
		public string speedTextName = "Speed Text";
		public string gearTextName = "Gear Text";
		public string RPMSliderName = "RPM Slider";
		public string distanceTypeName = "DistanceType Text";
//Acceleration, Top Speed and Handling
		public SpeedType speedType;
		public float topSpeed = 125.0f;
		public float topSpeedReverse = 45.0f;
		public float fullTorqueOverAllWheels = 5000.0f;
		public float reverseTorque = 1200.0f;
		public float brakeTorque = 3000.0f;
		public float maxHandbrakeTorque = 0.0f;
//Transmission
		public bool manual = false;
		public int NoOfGears = 5;
		[Range(0,1)] public float minRPM = 0.2f;
		[Range(0,1)] public float maxRPM = 0.8f;
		[Range(0,5)] public float RPMFallRate = 1.0f;
		public AnimationCurve torqueCurve = AnimationCurve.EaseInOut (0, 0.85f, 1, 0.65f);
		public AnimationCurve gearSpeedLimitCurve = AnimationCurve.Linear (0, 0, 1, 1);
		public float revRangeBoundary = 1f;
		public EventTrigger.Entry shiftUp;
		public EventTrigger.Entry shiftDown;
		public string shiftUpButtonName = "Shift Up Button";
		public string shiftDownButtonName = "Shift Down Button";
//Nitro
		public float nitroTopSpeed;
		public float nitroFullTorque;
		public float nitroDuration;
		public float nitroSpendRate;
		public float nitroRefillRate;
		public GameObject nitroFX;
		public EventTrigger.Entry nitroON;
		public EventTrigger.Entry nitroOFF;
		public string nitroButtonName = "Nitro Button";
		public bool nitroOn;
//Physics and Handling
		public Vector3 centerOfMass;
		public CarDriveType carDriveType = CarDriveType.FourWheelDrive;
		public float downforce = 100f;
		public float slipLimit = 0.3f;
		public float steerSensitivity = 0.15f;
		[Range(0, 90)] public float maxSteerAngle = 35.0f;
		[Range(0, 90)] public float steerAngleAtMaxSpeed = 35.0f;
		[Tooltip("0 is raw physics , 1 the car will grip in the direction it is facing")] [Range(0, 1)] public float steerHelper = 0.57f;
		[Tooltip("0 is no traction control, 1 is full interference")] [Range(0, 1)] public float tractionControl = 0.77f;
//Wheels
		public WheelCollider[] wheelColliders = new WheelCollider[4];
		public GameObject[] wheelMeshes = new GameObject[4];
		public IKD_VehicleWheelEffects[] wheelEffects = new IKD_VehicleWheelEffects[4];
//
		public bool overrideBrake;
		public float overrideBrakePower;//overrides the brake input value, used to force ai to brake
		public bool overrideAcceleration;
		public float overrideAccelerationPower;//overrides the brake input value, used to force ai to brake
		public bool overrideSteering;
		[Range(-1,1)] public float overrideSteeringPower;//overrides the steer input value, used to force ai to brake
//
		public float speed;
		public bool reversing;
		public float CurrentSteerAngle{ get { return steerAngle; }}
		public float CurrentSpeed{ get { return rbody.velocity.magnitude*2.23693629f; }}
		public float Revs { get; private set; }
		public float AccelInput { get; private set; }
		public float BrakeInput { get; private set; }
		#endregion
		
		#region Private Variables
		public IKD_VehicleCamera vehicleCamera;
		public Rigidbody rbody;
		private Text distanceTypeText;
		private Text speedText;
		private Text gearText;
		private Slider RPMSlider;
		private Slider nitroSlider;
		private float currentGearSpeedLimit;
		private float currentTorque;
		private float nitroAmount;
		private int currentGear;
		private float gearSpeedRange;
		private float gearFactor;
		private float upGearLimit;
		private float downGearLimit;
		private float steerAngle;
		private float previousRotation;
		private bool isAudioMuted;
		private bool canShift = true;
		private float thrustTorque;
		private Quaternion[] wheelMeshLocalRotations;
		private const float k_ReversingThreshold = 0.01f;
		float f;
		float targetGearFactor;
		#endregion
		
		#region Main Methods
		void OnEnable(){
			if (mobileController) {
				TurnTheGameOn.IKDriver.IKD_StaticUtility.m_IKD_UtilitySettings.useMobileController = true;
			} else {
				TurnTheGameOn.IKDriver.IKD_StaticUtility.m_IKD_UtilitySettings.useMobileController = false;
			}
			GameObject targetParent = GameObject.Find (parentCanvas);
			if (!targetParent) {
				targetParent = Instantiate (backupCanvas);
				targetParent.name = parentCanvas;
			}
			//Spawn UI HUD
			if (HUDPrefab != null) {
				targetParent = GameObject.Find (parentCanvas);
				Instantiate (HUDPrefab, targetParent.transform);
			}
			//Spwan UI Mobile Input
			if (mobilePrefab != null && mobileController) {
				targetParent = GameObject.Find (parentCanvas);
				GameObject ui = (GameObject) Instantiate (mobilePrefab, targetParent.transform);
				IKD_MobileControlRig mobileRig = ui.GetComponent<IKD_MobileControlRig> ();
				mobileRig.vehicleController = (IKD_VehicleController) this as IKD_VehicleController;
			}
			targetParent = null;
		}

		void Start(){		

			if (mobileController){
				//Setup Nitro UI Button
				EventTrigger mobileButton = GameObject.Find(nitroButtonName).GetComponent<EventTrigger>();
				EventTrigger.Entry entry = nitroON;
				mobileButton.triggers.Add(entry);
				entry = nitroOFF;
				mobileButton.triggers.Add(entry);
				if (manual) {
					//Setup Shift Up UI Button
					mobileButton = GameObject.Find (shiftUpButtonName).GetComponent<EventTrigger> ();
					entry = shiftUp;
					mobileButton.triggers.Add (entry);
					//Setup Shift Down UI Button
					mobileButton = GameObject.Find (shiftDownButtonName).GetComponent<EventTrigger> ();
					entry = shiftDown;
					mobileButton.triggers.Add (entry);
				}
			}


			nitroSlider = GameObject.Find("Nitro Slider").GetComponent<Slider>();
			nitroAmount = nitroDuration;

			speedText = GameObject.Find(speedTextName).GetComponent<Text>();
			gearText = GameObject.Find(gearTextName).GetComponent<Text>();
			RPMSlider = GameObject.Find(RPMSliderName).GetComponent<Slider>();
			distanceTypeText = GameObject.Find(distanceTypeName).GetComponent<Text>();

			switch (speedType) {
			case SpeedType.KPH:
				distanceTypeText.text = "KPH";
				break;
			case SpeedType.MPH:
				distanceTypeText.text = "MPH";
				break;
			}

			wheelMeshLocalRotations = new Quaternion[4];
			for (int i = 0; i < 4; i++){
				wheelMeshLocalRotations[i] = wheelMeshes[i].transform.localRotation;
			}
			wheelColliders[0].attachedRigidbody.centerOfMass = centerOfMass;

			maxHandbrakeTorque = float.MaxValue;

			if(rbody == null)  rbody = GetComponent<Rigidbody>();
			currentTorque = fullTorqueOverAllWheels - (tractionControl * fullTorqueOverAllWheels);
		}

		void Update() {
	
		}

		#endregion
		
		#region Utility Methods
		void GearChanging(){
			f = Mathf.Abs(CurrentSpeed/topSpeed);
			upGearLimit = (1/(float) NoOfGears) * (currentGear + 1);
			downGearLimit = (1/(float) NoOfGears) * currentGear;

			if (currentGear > 0 && f < downGearLimit){
				currentGear--;
			}

			if (f > upGearLimit && (currentGear < (NoOfGears - 1))){
				currentGear++;
			}
			if(gearText){
				if(BrakeInput > 0f && reversing){
					gearText.text = "R";
				}else{
					if(currentGear == 0)
						gearText.text = "N";
				}
				if(AccelInput > 0f){
					gearText.text = (currentGear + 1f).ToString();
				}
			}
		}

		void UpdateUI(){
			//Speed
			speed = rbody.velocity.magnitude;
			float speedometerMultiplier = 0f;
			switch (speedType){
			case SpeedType.MPH:
				speedometerMultiplier = 2.23693629f;
				break;
			case SpeedType.KPH:
				speedometerMultiplier = 3.6f;
				break;
			}
			speed *= speedometerMultiplier;
			if(speedText)	speedText.text = speed.ToString("F0");
			//Gears
			if (gearText && !manual) {
				if (BrakeInput > 0f && reversing) {
					gearText.text = "R";
				} else {
					if (currentGear == 0)
						gearText.text = "N";
				}
				if (AccelInput > 0f) {
					gearText.text = (currentGear + 1f).ToString ();
				}
			} else if(gearText){
				if (currentGear == 0) {
					gearText.text = "N";
				} else if (currentGear == -1) {
					gearText.text = "R";
				} else {
					gearText.text = (currentGear).ToString ();
				}
			}

		}

		void CheckGear(){
			gearSpeedRange = Mathf.Abs(CurrentSpeed/topSpeed);
			upGearLimit = (1/(float) NoOfGears)*(currentGear + 1);
			downGearLimit = (1/(float) NoOfGears)*currentGear;

			if (!manual) { 
				if (currentGear > 0 && gearSpeedRange < downGearLimit) {
					currentGear--;
				}
				if (gearSpeedRange > upGearLimit && (currentGear < (NoOfGears - 1))) {
					currentGear++;
				}
			} else {
				if (canShift) {
					if (Input.GetKeyDown (shiftUpKey) || Input.GetKeyDown (shiftUpJoystick)) {
						canShift = false;
						ShiftUp ();			
					}
					if (Input.GetKeyDown (shiftDownKey) || Input.GetKeyDown (shiftDownJoystick)) {
						canShift = false;
						ShiftDown ();
					}
				}
				else {
					if (Input.GetKeyUp (shiftUpKey) || Input.GetKeyUp (shiftUpJoystick)) {
						canShift = true;		
					}
					if (Input.GetKeyUp (shiftDownKey) || Input.GetKeyUp (shiftDownJoystick)) {
						canShift = true;
					}
				}
			}
		}

		// simple function to add a curved bias towards 1 for a value in the 0-1 range
		private static float CurveFactor(float factor){
			return 1 - (1 - factor)*(1 - factor);
		}

		// unclamped version of Lerp, to allow value to exceed the from-to range
		private static float ULerp(float from, float to, float value){
			return (1.0f - value)*from + value*to;
		}

		void CalculateGearFactor(){
			if (manual) {
				if (currentGear == 0) {
					targetGearFactor = minRPM;
				} else if (currentGear == -1) {
					//				if (mobileController) {
					//					
					//				} else {
					//					
					//				}
					targetGearFactor = (minRPM + (speed / currentGearSpeedLimit) * BrakeInput) - (1 - maxRPM);
				} else {
					targetGearFactor = (minRPM + (speed / currentGearSpeedLimit) * AccelInput) - (1 - maxRPM);
				}
				gearFactor = Mathf.Lerp (gearFactor, targetGearFactor, Time.deltaTime * RPMFallRate);
				if (System.Single.IsNaN (gearFactor)) {
					gearFactor = minRPM;
				}
				if (RPMSlider)
					RPMSlider.value = gearFactor;
			} else {
				f = (1 / (float)NoOfGears);
				targetGearFactor = Mathf.InverseLerp (f * currentGear, f * (currentGear + 1), Mathf.Abs (CurrentSpeed / topSpeed));
				gearFactor = Mathf.Lerp (gearFactor, targetGearFactor, Time.deltaTime * 5f);
				if (RPMSlider) {
					switch (speedType) {
					case SpeedType.KPH:
						if (currentGear != 3)	RPMSlider.value = gearFactor;
						if (currentGear == 3)	RPMSlider.value = 0.9f - gearFactor;
						break;
					case SpeedType.MPH:
						RPMSlider.value = gearFactor;
						break;
					}
				}
			}
			// calculate engine revs (for display / sound)
			// (this is done in retrospect - revs are not used in force/power calculations)
			var gearNumFactor = currentGear/(float) NoOfGears;
			var revsRangeMin = ULerp(0f, revRangeBoundary, CurveFactor(gearNumFactor));
			var revsRangeMax = ULerp(revRangeBoundary, 1f, gearNumFactor);
			Revs = ULerp(revsRangeMin, revsRangeMax, gearFactor);
		}


		public void Move(float steering, float accel, float footbrake, float handbrake){
			if (!reversing && currentGear == -1) {	rbody.AddForce (transform.forward * -5000);	accel = 0;	}
			if (!reversing && speed < 2 && Input.GetKey(eBrakeKey) == false && Input.GetKey(eBrakeJoystick) == false)	rbody.AddForce (transform.forward * 5000);



			for (int i = 0; i < 4; i++){
				Quaternion quat;
				Vector3 position;
				wheelColliders[i].GetWorldPose(out position, out quat);
				wheelMeshes[i].transform.position = position;
				wheelMeshes[i].transform.rotation = quat;
			}

			//clamp input values
			if (overrideSteering) steering = overrideSteeringPower;
			steering = Mathf.Clamp(steering, -1, 1);
			AccelInput = accel = Mathf.Clamp(accel, 0, 1);
			BrakeInput = footbrake = -1 * Mathf.Clamp (footbrake, -1, 0);
			handbrake = Mathf.Clamp(handbrake, 0, 1);

			//Set the steer on the front wheels.
			//Assuming that wheels 0 and 1 are the front wheels.
			float speedFactor = steerSensitivity * CurrentSpeed * 1.609344f / topSpeed;

			steerAngle = Mathf.Lerp(maxSteerAngle,steerAngleAtMaxSpeed, speedFactor);
			steerAngle *= steering;
			wheelColliders[0].steerAngle = steerAngle;
			wheelColliders[1].steerAngle = steerAngle;

			if(overrideBrake){
				footbrake = overrideBrakePower;
			}
			if(overrideAcceleration){
				accel = overrideAccelerationPower;
				ApplyDrive(accel, footbrake);
				return;
			}
			SteerHelper();
			ApplyDrive(accel, footbrake);
			//Set the handbrake.
			//Assuming that wheels 2 and 3 are the rear wheels.
			if (handbrake > 0f)	{
				var hbTorque = handbrake*maxHandbrakeTorque;
				wheelColliders[2].brakeTorque = hbTorque;
				wheelColliders[3].brakeTorque = hbTorque;
			}
			CalculateGearFactor ();
			CheckGear();
			UpdateUI ();
			AddDownForce();
			CheckForWheelSpin();
			TractionControl();
		}

		private void ApplyDrive(float accel, float footbrake){
			if (manual) {
				switch (carDriveType) {
				case CarDriveType.FourWheelDrive:
					if (speed < currentGearSpeedLimit) {
						thrustTorque = accel * (currentTorque / 4f);
						for (int i = 0; i < 4; i++) {
							wheelColliders [i].motorTorque = thrustTorque;
						}
					} else {
						for (int i = 0; i < 4; i++) {
							wheelColliders [i].motorTorque = 0;
						}
					}
					break;
				case CarDriveType.FrontWheelDrive:
					if (speed < currentGearSpeedLimit) {
						thrustTorque = accel * (currentTorque / 2f);
						wheelColliders [0].motorTorque = wheelColliders [1].motorTorque = thrustTorque;
					} else {
						wheelColliders [0].motorTorque = wheelColliders [1].motorTorque = 0;
					}
					break;
				case CarDriveType.RearWheelDrive:
					if (speed < currentGearSpeedLimit) {
						thrustTorque = accel * (currentTorque / 2f);
						wheelColliders [2].motorTorque = wheelColliders [3].motorTorque = thrustTorque;
					} else {
						wheelColliders [2].motorTorque = wheelColliders [3].motorTorque = 0;
					}
					break;
				}
			} else {
				switch (carDriveType) {
				case CarDriveType.FourWheelDrive:
					thrustTorque = accel * (currentTorque / 4f);
					for (int i = 0; i < 4; i++) {
						wheelColliders [i].motorTorque = thrustTorque;
					}
					break;
				case CarDriveType.FrontWheelDrive:
					thrustTorque = accel * (currentTorque / 2f);
					wheelColliders [0].motorTorque = wheelColliders [1].motorTorque = thrustTorque;
					break;
				case CarDriveType.RearWheelDrive:
					thrustTorque = accel * (currentTorque / 2f);
					wheelColliders [2].motorTorque = wheelColliders [3].motorTorque = thrustTorque;
					break;
				}
			}

			if (overrideBrake) footbrake = overrideBrakePower;
			if (overrideAcceleration) accel = overrideAccelerationPower;

			for (int i = 0; i < 4; i++) {
				if (CurrentSpeed > 0 && Vector3.Angle (transform.forward, rbody.velocity) < 50f) {
					reversing = false;
					wheelColliders [i].brakeTorque = brakeTorque * footbrake;
				} else if (footbrake > 0) {
					if (manual) {
						if (currentGear == -1) {
							reversing = true;
							if (speed < currentGearSpeedLimit) {
								wheelColliders [i].brakeTorque = 0f;
								wheelColliders [i].motorTorque = -reverseTorque * footbrake;
							}else{
								wheelColliders [i].motorTorque = 0;
							}
						}
					} else {
						reversing = true;
						wheelColliders [i].brakeTorque = 0f;
						wheelColliders [i].motorTorque = -reverseTorque * footbrake;
					}
				}
			}

		}

		private void SteerHelper(){
			for (int i = 0; i < 4; i++){
				WheelHit wheelhit;
				wheelColliders[i].GetGroundHit(out wheelhit);
				if (wheelhit.normal == Vector3.zero)
					return; // wheels arent on the ground so dont realign the rigidbody velocity
			}
			// this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
			if (Mathf.Abs(previousRotation - transform.eulerAngles.y) < 10f){
				var turnadjust = (transform.eulerAngles.y - previousRotation) * steerHelper;
				Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
				rbody.velocity = velRotation * rbody.velocity;
			}
			previousRotation = transform.eulerAngles.y;
		}

		// used to add more grip in relation to speed
		private void AddDownForce(){
			rbody.AddForce(-transform.up * downforce * rbody.velocity.magnitude);
		}

		// checks if the wheels are spinning and is so does three things
		// 1) emits particles
		// 2) plays tiure skidding sounds
		// 3) leaves skidmarks on the ground
		// these effects are controlled through the WheelEffects class
		private void CheckForWheelSpin(){
			// loop through all wheels
//			for (int i = 0; i < 4; i++)	{
//				WheelHit wheelHit;
//				wheelColliders[i].GetGroundHit(out wheelHit);
//				// is the tire slipping above the given threshhold
//				if (Mathf.Abs(wheelHit.forwardSlip) >= slipLimit || Mathf.Abs(wheelHit.sidewaysSlip) >= slipLimit){
//					wheelEffects[i].EmitTyreSmoke();
//					// avoiding all four tires screeching at the same time
//					// if they do it can lead to some strange audio artefacts
//					if (!AnySkidSoundPlaying())	{
//						if(wheelEffects[i].enabled)
//							wheelEffects[i].PlayAudio();
//					}
//					continue;
//				}
//				// if it wasnt slipping stop all the audio
//				if (wheelEffects[i].PlayingAudio){
//					wheelEffects[i].StopAudio();
//				}
//				// end the trail generation
//				wheelEffects[i].EndSkidTrail();
//			}
		}

		//reduces the power to wheel if the car is wheel spinning too much
		void TractionControl(){
			WheelHit wheelHit;
			switch (carDriveType){
			case CarDriveType.FourWheelDrive:
				for (int i = 0; i < 4; i++){
					wheelColliders[i].GetGroundHit(out wheelHit);
					AdjustTorque(wheelHit.forwardSlip);
				}
				break;
			case CarDriveType.RearWheelDrive:
				wheelColliders[2].GetGroundHit(out wheelHit);
				AdjustTorque(wheelHit.forwardSlip);
				wheelColliders[3].GetGroundHit(out wheelHit);
				AdjustTorque(wheelHit.forwardSlip);
				break;
			case CarDriveType.FrontWheelDrive:
				wheelColliders[0].GetGroundHit(out wheelHit);
				AdjustTorque(wheelHit.forwardSlip);
				wheelColliders[1].GetGroundHit(out wheelHit);
				AdjustTorque(wheelHit.forwardSlip);
				break;
			}
		}

		private bool AnySkidSoundPlaying(){
			for (int i = 0; i < 4; i++){
				if (wheelEffects[i].PlayingAudio)	{
					return true;
				}
			}
			return false;
		}

		void AdjustTorque(float forwardSlip){
			if (forwardSlip >= slipLimit && currentTorque >= 0){
				currentTorque -= 10 * tractionControl;
			}
			else{
				currentTorque += 10 * tractionControl;
				if (currentTorque > fullTorqueOverAllWheels){
					currentTorque = fullTorqueOverAllWheels;
				}
			}
			float curvePoint = (float)currentGear / (float)NoOfGears;
			currentTorque = fullTorqueOverAllWheels * torqueCurve.Evaluate (curvePoint);
			if (currentGear >= 0) {
				currentGearSpeedLimit = topSpeed * gearSpeedLimitCurve.Evaluate (curvePoint);
			} else {
				currentGearSpeedLimit = topSpeedReverse;
			}
		}

		[ContextMenu("Shift Up")]
		public void ShiftUp(){
			if (!manual)	return;
			if ((currentGear < (NoOfGears - 1))) {
				currentGear++;
				gearFactor = minRPM;
			}
		}

		[ContextMenu("Shift Down")]
		public void ShiftDown(){
			if (!manual)	return;
			if (gearSpeedRange < downGearLimit) {
				currentGear--;
				gearFactor = minRPM;
			} else if (currentGear == 0) {
				currentGear--;
				gearFactor = minRPM;
			}
		}

		[ContextMenu("NiroON")]
		public void NitroOn() {
			if (!nitroOn && nitroAmount > 2.0f) {
				GameObject tempObject = Instantiate (Resources.Load ("IKD_AudioClip_Nitro")) as GameObject;
				tempObject.name = "Audio Clip - Nitro";
				tempObject = null;
				nitroFX.SetActive (true);
				topSpeed = topSpeed + nitroTopSpeed;
				fullTorqueOverAllWheels = fullTorqueOverAllWheels + nitroFullTorque;
				nitroOn = true;
			}
		}

		[ContextMenu("NiroOFF")]
		public void NitroOff() {
			if (nitroOn) {
				nitroFX.SetActive(false);
				topSpeed = topSpeed - nitroTopSpeed;
				fullTorqueOverAllWheels = fullTorqueOverAllWheels - nitroFullTorque;
				nitroOn = false;
			}
		}

		private void CapSpeed(){
			speed = rbody.velocity.magnitude;
			switch (speedType)
			{
			case SpeedType.MPH:

				speed *= 2.23693629f;
				if(speedText){
					speedText.text = speed.ToString("F0");
				}
				if (speed > topSpeed)
					rbody.velocity = (topSpeed/2.23693629f) * rbody.velocity.normalized;
				break;

			case SpeedType.KPH:
				speed *= 3.6f;
				if(speedText){
					speedText.text = speed.ToString("F0");
				}
				if (speed > topSpeed)
					rbody.velocity = (topSpeed/3.6f) * rbody.velocity.normalized;
				break;
			}
		}

		[ContextMenu("Align Wheel Colliders")]
		public void AlignWheelColliders() {
			// make a reference to the colliders original parent
			Transform defaultColliderParent = wheelColliders [0].transform.parent;
			// move colliders to the reference positions
			wheelColliders [0].transform.parent = wheelMeshes [0].transform;
			wheelColliders [1].transform.parent = wheelMeshes [1].transform;
			wheelColliders [2].transform.parent = wheelMeshes [2].transform;
			wheelColliders [3].transform.parent = wheelMeshes [3].transform;
			//adjust the wheel collider positions on x and z axis to match the new wheel position
			wheelColliders [0].transform.position = new Vector3 (wheelMeshes [0].transform.position.x, 
				wheelColliders [0].transform.position.y, wheelMeshes [0].transform.position.z);
			wheelColliders [1].transform.position = new Vector3 (wheelMeshes [1].transform.position.x, 
				wheelColliders [1].transform.position.y, wheelMeshes [1].transform.position.z);
			wheelColliders [2].transform.position = new Vector3 (wheelMeshes [2].transform.position.x, 
				wheelColliders [2].transform.position.y, wheelMeshes [2].transform.position.z);
			wheelColliders [3].transform.position = new Vector3 (wheelMeshes [3].transform.position.x, 
				wheelColliders [3].transform.position.y, wheelMeshes [3].transform.position.z);
			// move colliders back to the original parent
			wheelColliders [0].transform.parent = defaultColliderParent;
			wheelColliders [1].transform.parent = defaultColliderParent;
			wheelColliders [2].transform.parent = defaultColliderParent;
			wheelColliders [3].transform.parent = defaultColliderParent;
		}
		#endregion

	}
}