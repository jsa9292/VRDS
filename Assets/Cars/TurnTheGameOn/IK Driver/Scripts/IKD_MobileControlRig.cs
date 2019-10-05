using UnityEngine;
using System.Collections;

namespace TurnTheGameOn.IKDriver{
	public class IKD_MobileControlRig : MonoBehaviour {
		
		#region Public Variables
		public IKD_VehicleController vehicleController;
		public GameObject turnLeftButton;
		public GameObject turnRightButton;
		public GameObject steeringJoystick;
		public GameObject tiltInput;
		public GameObject steeringWheel;
		public GameObject shiftUpButton;
		public GameObject shiftDownButton;
		#endregion
		
		#region Main Methods
		void Start (){
			foreach (Transform t in transform){
				t.gameObject.SetActive(true);
			}

			steeringJoystick.SetActive (false);
			turnLeftButton.SetActive (false);
			turnRightButton.SetActive (false);
			tiltInput.SetActive (false);
			steeringWheel.SetActive (false);

			IKD_VehicleController.MobileSteeringType mobileSteeringType = vehicleController.mobileSteeringType;
			//IKD_VehicleController.MobileSteeringType mo = IKD_VehicleController.MobileSteeringType.UIButtons;
			//int mobileType = 0;
			switch (mobileSteeringType) {
			case IKD_VehicleController.MobileSteeringType.UIButtons:		//Arrow Button Steering
				turnLeftButton.SetActive (true);
				turnRightButton.SetActive (true);
				break;
			case IKD_VehicleController.MobileSteeringType.Tilt:				//Tilt Steering
				tiltInput.SetActive (true);
				break;
			case IKD_VehicleController.MobileSteeringType.UIJoystick:		//Joystick Steering
				steeringJoystick.SetActive (true);
				break;
			case IKD_VehicleController.MobileSteeringType.UISteeringWheel:	//Steering Wheel
				steeringWheel.SetActive (true);
				break;
			}

			shiftUpButton.SetActive (vehicleController.manual);
			shiftDownButton.SetActive (vehicleController.manual);

		}
		#endregion	

	}
}