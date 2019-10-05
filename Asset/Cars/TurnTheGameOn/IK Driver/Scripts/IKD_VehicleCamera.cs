using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace TurnTheGameOn.IKDriver{
	public class IKD_VehicleCamera : MonoBehaviour {
		
		#region Public Variables
		public enum CameraType{ CarCamera, HelmetCamera}

		public IKD_VehicleController carController;
		public bool mobile;
		public string cameraButtonName = "Camera Switch Button";
		public EventTrigger.Entry switchCameraEvent;
		public CameraType cameraType;
		public Camera carCamera;
		public Camera helmetCamera;
		public Camera lookBackCamera;
		public Transform car;
		public float cameraFollowSpeed;
		//public float cameraShakeMultiplier;
		public float distance;
		public float height;
		public float rotationDamping;
		public float heightDamping;
		public float zoomRatio;
		public float DefaultFOV;
		private Vector3 rotationVector;
		private Vector3 position;
		private Rigidbody carBody;

		public float maxRotation;
		public float rotation;
		public float rotationSpeed;
		[Header("Camera Pivot")]
		public float max;
		public float value;
		public float minPivotMoveSpeed;
		public float maxPivotMoveSpeed;
		public float pivotMoveSpeed;
		public Vector3 targetPivotPosition;
		public bool cameraShake;
		[Range(0,1.0f)]public float shakeStartSpeedFactor;
		private float speedFactor;
		public float shakeAmount = 1.0f;
		Vector3 camStartPos;
		public float normalBlurFactor;
		public float nitroBlurFactor;
		public float normalVelocityMax;
		public float nitroVelocityMax;
		public float normalZoomRation;
		public float nitroZoomRatio;
		public float normalHeight;
		public float nitroHeightMultiplier;
		#endregion
		
		#region Private Variables
		
		#endregion
		
		#region Main Methods	
		void Start(){
			if(TurnTheGameOn.IKDriver.IKD_StaticUtility.m_IKD_UtilitySettings.useMobileController){
				mobile = true;
				EventTrigger findEvent = GameObject.Find (cameraButtonName).GetComponent<EventTrigger>();
				if (findEvent != null) {
					findEvent.triggers.Add (switchCameraEvent);
					findEvent = null;
				}
			}
			carBody = car.GetComponent<Rigidbody> ();
			camStartPos = carCamera.transform.localPosition;
		}

		void Update(){
			
			if (car != null) {
				if (cameraType == CameraType.CarCamera) {				
					transform.parent = null;
					carCamera.gameObject.SetActive (true);
					helmetCamera.gameObject.SetActive (false);
				} else if (cameraType == CameraType.HelmetCamera) {	
					helmetCamera.gameObject.SetActive (true);
					if (car != null) {
						transform.parent = car;
					}
					carCamera.gameObject.SetActive (false);
				}



				if (Input.GetKeyDown (carController.lookBackKey) || Input.GetKeyDown (carController.lookBackJoystick) ) {
					LookBackCamera ();
				} else if( Input.GetKeyUp (carController.lookBackKey) || Input.GetKeyUp (carController.lookBackJoystick) ){
					lookBackCamera.gameObject.SetActive (false);
					if (cameraType == CameraType.CarCamera) {
						carCamera.enabled = true;
					}else if (cameraType == CameraType.HelmetCamera){
						helmetCamera.enabled = true;
					}

				}
				//			if (Input.GetKeyDown (RGT_PlayerPrefs.inputData.cameraSwitchKey) || Input.GetKeyDown (RGT_PlayerPrefs.inputData.cameraSwitchJoystick)) {
				//				CycleCamera ();
				//			}
				//			if (Input.GetKeyDown (RGT_PlayerPrefs.inputData.nitroKey) || Input.GetKeyDown (RGT_PlayerPrefs.inputData.nitroJoystick)) {
				//				carController.NitroOn ();
				//			}
				//			if(Input.GetKeyUp (RGT_PlayerPrefs.inputData.nitroKey) || Input.GetKeyUp (RGT_PlayerPrefs.inputData.nitroJoystick)){
				//				carController.NitroOff ();
				//			}
			}
		}

		void LateUpdate() {
			if (Input.GetKeyDown (carController.cameraSwitchKey) || Input.GetKeyDown (carController.cameraSwitchJoystick)) {
				CycleCamera ();
			}
			if (car != null) {
				if (cameraType == CameraType.CarCamera) {				
					var wantedAngle = rotationVector.y;
					var wantedHeight = car.position.y + height;
					var myAngle = transform.eulerAngles.y;
					var myHeight = transform.position.y;
					myAngle = Mathf.LerpAngle (myAngle, wantedAngle, rotationDamping * Time.deltaTime);
					myHeight = Mathf.Lerp (myHeight, wantedHeight, heightDamping * Time.deltaTime);
					var currentRotation = Quaternion.Euler (0, myAngle, 0);
					transform.position = car.position;
					transform.position -= currentRotation * Vector3.forward * distance;
					position = transform.position;
					position.y = myHeight;
					//add shake based on speed
					//	float tempShake = Random.Range (1.0f,2.0f);
					//	float tempRPM;
					//	tempRPM = syncVars.wheelRPM;
					//	tempShake = tempShake * cameraShakeMultiplier * (tempRPM / 25);
					//	tempShake = Random.Range (-tempShake, tempShake);
					//	position.x += tempShake;
					//	position.y += tempShake * Random.Range (0f, 1.5f);
					//Debug.Log (tempShake);
					//	temp2 = new Vector3 (defaultSteering.x, defaultSteering.y, -(horizontalInput * (rotationLimit )));
					//	float zAngle = Mathf.SmoothDampAngle (steeringWheel.localEulerAngles.z, temp2.z - tempShake, ref yVelocity, steeringRotationSpeed );
					//	steeringWheel.localEulerAngles = new Vector3 (defaultSteering.x, defaultSteering.y, zAngle );

					transform.position = Vector3.Lerp (transform.position, position, cameraFollowSpeed);
					//transform.position = position;
					transform.LookAt (car);
					//carCamera.transform.LookAt (car);
					//Vector3.Lerp (cameraComponent.transform.position, targetPivotPosition, pivotMoveSpeed * Time.deltaTime);
					if (Input.GetAxis ("Horizontal") > 0) {
						value = -max;
						pivotMoveSpeed = minPivotMoveSpeed;
						if (rotation > 0) {
							rotation = Mathf.MoveTowards (rotation, maxRotation, Time.deltaTime * (rotationSpeed * speedFactor * 2));
						} else {
							rotation = Mathf.MoveTowards (rotation, maxRotation, Time.deltaTime * (rotationSpeed * speedFactor));
						}
					} else if (Input.GetAxis ("Horizontal") < 0) {
						value = max;
						pivotMoveSpeed = minPivotMoveSpeed;
						if (rotation > 0) {
							rotation = Mathf.MoveTowards (rotation, -maxRotation, Time.deltaTime * (rotationSpeed * speedFactor));
						} else {
							rotation = Mathf.MoveTowards (rotation, -maxRotation, Time.deltaTime * (rotationSpeed * speedFactor * 2));
						}
					} else {
						value = 0;
						pivotMoveSpeed = Mathf.Lerp (pivotMoveSpeed, maxPivotMoveSpeed, 1 * Time.deltaTime);
						rotation = Mathf.MoveTowards (rotation, 0, Time.deltaTime * (rotationSpeed * speedFactor * 2));
					}
					carCamera.transform.localRotation = Quaternion.Euler (carCamera.transform.localRotation.eulerAngles.x, rotation, carCamera.transform.localRotation.eulerAngles.z);

					targetPivotPosition.x = Mathf.Lerp (targetPivotPosition.x, value, pivotMoveSpeed * Time.deltaTime);
					carCamera.transform.localPosition = targetPivotPosition;

					speedFactor = carController.speed / carController.topSpeed;


					if (carController.nitroOn) {
						speedFactor *= 2.0f;
						//					motionBlur.velocityScale = Mathf.Lerp (motionBlur.velocityScale, nitroBlurFactor, Time.deltaTime);
						//					motionBlur.maxVelocity = Mathf.Lerp(motionBlur.maxVelocity, nitroVelocityMax, Time.deltaTime);
						zoomRatio = Mathf.Lerp(zoomRatio, nitroZoomRatio, Time.deltaTime);
						height = Mathf.Lerp(height, normalHeight + (nitroHeightMultiplier * speedFactor), Time.deltaTime);
					} else {
						//					motionBlur.velocityScale = Mathf.Lerp (motionBlur.velocityScale, normalBlurFactor, Time.deltaTime);
						//					motionBlur.maxVelocity = Mathf.Lerp(motionBlur.maxVelocity, normalVelocityMax, Time.deltaTime);
						zoomRatio = Mathf.Lerp(zoomRatio, normalZoomRation, Time.deltaTime);
						height = Mathf.Lerp(height, normalHeight, Time.deltaTime);
					}if (cameraShake &&  speedFactor > shakeStartSpeedFactor) {
						carCamera.transform.localPosition = camStartPos + Random.insideUnitSphere * (shakeAmount * speedFactor);
					}else{
						carCamera.transform.localPosition = camStartPos;
					}

					//shakeDuration _ = Time.deltaTime * decreaseFactor;

				} else if (cameraType == CameraType.HelmetCamera) {

				}
			}
		}

		void FixedUpdate(){

			if (cameraType == CameraType.CarCamera) {
				if (car && !mobile) {
					var localVelocity = car.InverseTransformDirection (carBody.velocity);
					if (localVelocity.z < -0.5f && Input.GetAxis (carController.throttleAxis) == -1) {
						rotationVector.y = car.eulerAngles.y + 180f;
					} else {
						rotationVector.y = car.eulerAngles.y;
					}
					var acc = carBody.velocity.magnitude;
					carCamera.fieldOfView = DefaultFOV + acc * zoomRatio * Time.deltaTime;
					//cameraComponent.transform.rotation = pivotRotation;
				}else if (car && mobile){

					var localVelocity = car.InverseTransformDirection (carBody.velocity);
					if (localVelocity.z < -0.5f && TurnTheGameOn.IKDriver.IKD_CrossPlatformInputManager.GetAxis (carController.throttleAxis) == -1) {
						rotationVector.y = car.eulerAngles.y + 180f;
					} else {
						rotationVector.y = car.eulerAngles.y;
					}
					var acc = carBody.velocity.magnitude;
					carCamera.fieldOfView = DefaultFOV + acc * zoomRatio * Time.deltaTime;
				}
			}
			else if (cameraType == CameraType.HelmetCamera){

			}
		}
		#endregion
		
		#region Utility Methods
		public void CycleCamera(){
			if (cameraType == CameraType.CarCamera) {
				cameraType = CameraType.HelmetCamera;
				helmetCamera.enabled = true;
			}else if (cameraType == CameraType.HelmetCamera){
				cameraType = CameraType.CarCamera;
				carCamera.enabled = true;
			}
		}

		public void LookBackCamera(){
			carCamera.enabled = false;
			helmetCamera.enabled = false;
			lookBackCamera.gameObject.SetActive(true);
		}
		#endregion
		
	}
}