using UnityEngine;
using System.Collections;

namespace TurnTheGameOn.IKDriver{
	public class IKD_VehicleInput : MonoBehaviour {
		
		#region Public Variables
		public IKD_VehicleController vehicleController;
		#endregion
		
		#region Private Variables
		private float v;
		private float h;
		private float emergencyBrake;
		#endregion
		
		#region Main Methods
		private void Awake(){
			if (!vehicleController)	vehicleController = GetComponent<IKD_VehicleController>();
		}

		private void FixedUpdate()	{
			//h = Input.GetAxis(vehicleController.steeringAxis);
			//v = Input.GetAxis(vehicleController.throttleAxis);
			h = TurnTheGameOn.IKDriver.IKD_CrossPlatformInputManager.GetAxis(vehicleController.steeringAxis);
			v = TurnTheGameOn.IKDriver.IKD_CrossPlatformInputManager.GetAxis(vehicleController.throttleAxis);
			if (Input.GetKey (vehicleController.eBrakeKey) || Input.GetKey (vehicleController.eBrakeJoystick)) {
				emergencyBrake = 1;
			} else {
				emergencyBrake = 0;
			}
				
			vehicleController.Move(h, v, v, emergencyBrake);
//			//Get steering input
//			h = Input.GetAxis(vehicleController.steeringAxis);
//			//Get acceleration input
//			if (Input.GetKey (vehicleController.accelerateKey) || Input.GetKey (vehicleController.accelerateJoystick)) {
//				accel += 1 * vehicleController.accelerateSensitivity;
//				if (accel > 1) accel = 1;
//			} else {
//				accel -= 1 * vehicleController.accelerateSensitivity;
//				if (accel < 0) accel = 0;
//			}
//			//Get brake input
//			if (Input.GetKey (vehicleController.brakeKey) || Input.GetKey (vehicleController.brakeJoystick)) {
//				brake -= 1 * vehicleController.brakeSensitivity;
//				if (brake < -1) brake = -1;
//			} else {
//				brake += 1 * vehicleController.brakeSensitivity;
//				if (brake > 0) brake = 0;
//			}
//
//			//Get emergency brake input
//			if (Input.GetKey (vehicleController.eBrakeKey) || Input.GetKey (vehicleController.eBrakeJoystick)) {
//				emergencyBrake = 1;
//			} else {
//				emergencyBrake = 0;
//			}
//
//			if(!TurnTheGameOn.IKDriver.IKD_StaticUtility.m_IKD_UtilitySettings.useMobileController){
//				vehicleController.Move(h, accel, brake, emergencyBrake);
//			} else{
//				vehicleController.Move(h, accel, brake, 0f);
//			}
//			vehicleController.Move(h, accel, brake, emergencyBrake);
		}
		#endregion
		
	}
}