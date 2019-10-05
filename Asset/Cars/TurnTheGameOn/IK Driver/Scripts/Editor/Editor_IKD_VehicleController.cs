using UnityEngine;
using UnityEditor;
using System.Collections;

namespace TurnTheGameOn.IKDriver{
	[CustomEditor(typeof(IKD_VehicleController))]
	public class Editor_IKD_VehicleController : Editor {

		bool showInput;
		bool showHUD;
		bool showSpeed;
		bool showTransmission;
		bool showNitro;
		bool showHandling;
		bool showWheels;
		bool showAIOverride;

		public override void OnInspectorGUI(){
			IKD_VehicleController vehicleController = (IKD_VehicleController)target;

			EditorGUILayout.BeginVertical ("Box");
			showInput = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showInput, "Player Input", true);	
			if (showInput) {

				SerializedProperty mobileController = serializedObject.FindProperty ("mobileController");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (mobileController, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty mobileSteeringType = serializedObject.FindProperty ("mobileSteeringType");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (mobileSteeringType, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				if (vehicleController.mobileController) {
					TurnTheGameOn.IKDriver.IKD_StaticUtility.m_IKD_UtilitySettings.useMobileController = true;
				} else {
					TurnTheGameOn.IKDriver.IKD_StaticUtility.m_IKD_UtilitySettings.useMobileController = false;
				}

				EditorGUILayout.Space ();
				EditorGUILayout.LabelField ("Input Manager Axis Input", EditorStyles.boldLabel);
				EditorGUILayout.Space ();

				SerializedProperty steeringAxis = serializedObject.FindProperty ("steeringAxis");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringAxis, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty throttleAxis = serializedObject.FindProperty ("throttleAxis");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (throttleAxis, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				EditorGUILayout.Space ();
				EditorGUILayout.LabelField ("Keyboard Input", EditorStyles.boldLabel);
				EditorGUILayout.Space ();

				SerializedProperty eBrakeKey = serializedObject.FindProperty ("eBrakeKey");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (eBrakeKey, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty shiftUpKey = serializedObject.FindProperty ("shiftUpKey");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (shiftUpKey, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty shiftDownKey = serializedObject.FindProperty ("shiftDownKey");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (shiftDownKey, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty lookBackKey = serializedObject.FindProperty ("lookBackKey");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (lookBackKey, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty cameraSwitchKey = serializedObject.FindProperty ("cameraSwitchKey");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (cameraSwitchKey, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				EditorGUILayout.Space ();
				EditorGUILayout.LabelField ("Joystick Input", EditorStyles.boldLabel);
				EditorGUILayout.Space ();

				vehicleController._eBrakeJoystick = (IKD_VehicleController.Joystick)EditorGUILayout.EnumPopup ("Emergency Brake", vehicleController._eBrakeJoystick);
				switch(vehicleController._eBrakeJoystick){
				case IKD_VehicleController.Joystick.None:
					vehicleController.eBrakeJoystick = KeyCode.None;
					break;
				case IKD_VehicleController.Joystick.JoystickButton0:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton0; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton1:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton1; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton2:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton2; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton3:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton3; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton4:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton4; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton5:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton5; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton6:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton6; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton7:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton7; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton8:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton8; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton9:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton9; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton10:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton10; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton11:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton11; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton12:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton12; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton13:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton13; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton14:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton14; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton15:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton15; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton16:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton16; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton17:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton17; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton18:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton18; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton19:
					vehicleController.eBrakeJoystick = KeyCode.JoystickButton19;
					break;
				}
				vehicleController._nitroJoystick = (IKD_VehicleController.Joystick)EditorGUILayout.EnumPopup ("Nitro", vehicleController._nitroJoystick);
				switch(vehicleController._nitroJoystick){
				case IKD_VehicleController.Joystick.None:
					vehicleController.nitroJoystick = KeyCode.None;
					break;
				case IKD_VehicleController.Joystick.JoystickButton0:
					vehicleController.nitroJoystick = KeyCode.JoystickButton0; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton1:
					vehicleController.nitroJoystick = KeyCode.JoystickButton1; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton2:
					vehicleController.nitroJoystick = KeyCode.JoystickButton2; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton3:
					vehicleController.nitroJoystick = KeyCode.JoystickButton3; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton4:
					vehicleController.nitroJoystick = KeyCode.JoystickButton4; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton5:
					vehicleController.nitroJoystick = KeyCode.JoystickButton5; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton6:
					vehicleController.nitroJoystick = KeyCode.JoystickButton6; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton7:
					vehicleController.nitroJoystick = KeyCode.JoystickButton7; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton8:
					vehicleController.nitroJoystick = KeyCode.JoystickButton8; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton9:
					vehicleController.nitroJoystick = KeyCode.JoystickButton9; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton10:
					vehicleController.nitroJoystick = KeyCode.JoystickButton10; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton11:
					vehicleController.nitroJoystick = KeyCode.JoystickButton11; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton12:
					vehicleController.nitroJoystick = KeyCode.JoystickButton12; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton13:
					vehicleController.nitroJoystick = KeyCode.JoystickButton13; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton14:
					vehicleController.nitroJoystick = KeyCode.JoystickButton14; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton15:
					vehicleController.nitroJoystick = KeyCode.JoystickButton15; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton16:
					vehicleController.nitroJoystick = KeyCode.JoystickButton16; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton17:
					vehicleController.nitroJoystick = KeyCode.JoystickButton17; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton18:
					vehicleController.nitroJoystick = KeyCode.JoystickButton18; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton19:
					vehicleController.nitroJoystick = KeyCode.JoystickButton19;
					break;
				}
				vehicleController._shiftUpJoystick = (IKD_VehicleController.Joystick)EditorGUILayout.EnumPopup ("Shift Up", vehicleController._shiftUpJoystick);
				switch(vehicleController._shiftUpJoystick){
				case IKD_VehicleController.Joystick.None:
					vehicleController.shiftUpJoystick = KeyCode.None;
					break;
				case IKD_VehicleController.Joystick.JoystickButton0:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton0; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton1:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton1; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton2:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton2; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton3:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton3; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton4:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton4; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton5:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton5; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton6:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton6; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton7:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton7; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton8:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton8; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton9:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton9; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton10:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton10; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton11:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton11; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton12:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton12; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton13:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton13; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton14:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton14; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton15:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton15; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton16:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton16; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton17:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton17; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton18:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton18; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton19:
					vehicleController.shiftUpJoystick = KeyCode.JoystickButton19;
					break;
				}
				vehicleController._shiftDownJoystick = (IKD_VehicleController.Joystick)EditorGUILayout.EnumPopup ("Shift Down", vehicleController._shiftDownJoystick);
				switch(vehicleController._shiftDownJoystick){
				case IKD_VehicleController.Joystick.None:
					vehicleController.shiftDownJoystick = KeyCode.None;
					break;
				case IKD_VehicleController.Joystick.JoystickButton0:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton0; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton1:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton1; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton2:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton2; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton3:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton3; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton4:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton4; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton5:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton5; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton6:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton6; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton7:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton7; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton8:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton8; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton9:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton9; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton10:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton10; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton11:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton11; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton12:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton12; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton13:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton13; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton14:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton14; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton15:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton15; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton16:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton16; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton17:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton17; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton18:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton18; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton19:
					vehicleController.shiftDownJoystick = KeyCode.JoystickButton19;
					break;
				}
				vehicleController._lookBackJoystick = (IKD_VehicleController.Joystick)EditorGUILayout.EnumPopup ("Look Back", vehicleController._lookBackJoystick);
				switch(vehicleController._lookBackJoystick){
				case IKD_VehicleController.Joystick.None:
					vehicleController.lookBackJoystick = KeyCode.None;
					break;
				case IKD_VehicleController.Joystick.JoystickButton0:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton0; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton1:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton1; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton2:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton2; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton3:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton3; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton4:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton4; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton5:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton5; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton6:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton6; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton7:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton7; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton8:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton8; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton9:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton9; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton10:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton10; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton11:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton11; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton12:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton12; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton13:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton13; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton14:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton14; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton15:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton15; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton16:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton16; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton17:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton17; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton18:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton18; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton19:
					vehicleController.lookBackJoystick = KeyCode.JoystickButton19;
					break;
				}
				vehicleController._cameraSwitchJoystick = (IKD_VehicleController.Joystick)EditorGUILayout.EnumPopup ("Camera Switch", vehicleController._cameraSwitchJoystick);
				switch(vehicleController._cameraSwitchJoystick){
				case IKD_VehicleController.Joystick.None:
					vehicleController.cameraSwitchJoystick = KeyCode.None;
					break;
				case IKD_VehicleController.Joystick.JoystickButton0:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton0; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton1:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton1; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton2:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton2; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton3:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton3; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton4:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton4; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton5:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton5; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton6:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton6; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton7:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton7; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton8:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton8; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton9:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton9; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton10:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton10; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton11:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton11; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton12:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton12; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton13:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton13; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton14:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton14; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton15:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton15; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton16:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton16; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton17:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton17; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton18:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton18; 
					break;
				case IKD_VehicleController.Joystick.JoystickButton19:
					vehicleController.cameraSwitchJoystick = KeyCode.JoystickButton19;
					break;
				}
				EditorUtility.SetDirty (vehicleController);

			}
			EditorGUILayout.EndVertical ();

			EditorGUILayout.BeginVertical ("Box");
			showHUD = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showHUD, "UI & HUD Lookup", true);	
			if (showHUD) {

				SerializedProperty HUDPrefab = serializedObject.FindProperty ("HUDPrefab");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (HUDPrefab, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty mobilePrefab = serializedObject.FindProperty ("mobilePrefab");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (mobilePrefab, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty backupCanvas = serializedObject.FindProperty ("backupCanvas");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (backupCanvas, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty parentCanvas = serializedObject.FindProperty ("parentCanvas");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (parentCanvas, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty speedTextName = serializedObject.FindProperty ("speedTextName");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (speedTextName, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty gearTextName = serializedObject.FindProperty ("gearTextName");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (gearTextName, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty RPMSliderName = serializedObject.FindProperty ("RPMSliderName");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (RPMSliderName, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty distanceTypeName = serializedObject.FindProperty ("distanceTypeName");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (distanceTypeName, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty shiftUpButtonName = serializedObject.FindProperty ("shiftUpButtonName");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (shiftUpButtonName, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty shiftDownButtonName = serializedObject.FindProperty ("shiftDownButtonName");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (shiftDownButtonName, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty nitroButtonName = serializedObject.FindProperty ("nitroButtonName");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (nitroButtonName, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				EditorUtility.SetDirty (vehicleController);

			}
			EditorGUILayout.EndVertical ();

			EditorGUILayout.BeginVertical ("Box");
			showSpeed = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showSpeed, "Acceleration, Speed and Braking", true);	
			if (showSpeed) {

				SerializedProperty speedType = serializedObject.FindProperty ("speedType");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (speedType, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty topSpeed = serializedObject.FindProperty ("topSpeed");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (topSpeed, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty topSpeedReverse = serializedObject.FindProperty ("topSpeedReverse");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (topSpeedReverse, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty fullTorqueOverAllWheels = serializedObject.FindProperty ("fullTorqueOverAllWheels");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (fullTorqueOverAllWheels, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty reverseTorque = serializedObject.FindProperty ("reverseTorque");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (reverseTorque, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty brakeTorque = serializedObject.FindProperty ("brakeTorque");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (brakeTorque, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty maxHandbrakeTorque = serializedObject.FindProperty ("maxHandbrakeTorque");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (maxHandbrakeTorque, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				EditorUtility.SetDirty (vehicleController);

			}
			EditorGUILayout.EndVertical ();

			EditorGUILayout.BeginVertical ("Box");
			showTransmission = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showTransmission, "Transmission", true);	
			if (showTransmission) {

				SerializedProperty manual = serializedObject.FindProperty ("manual");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (manual, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty NoOfGears = serializedObject.FindProperty ("NoOfGears");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (NoOfGears, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty minRPM = serializedObject.FindProperty ("minRPM");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (minRPM, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty maxRPM = serializedObject.FindProperty ("maxRPM");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (maxRPM, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty RPMFallRate = serializedObject.FindProperty ("RPMFallRate");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (RPMFallRate, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty torqueCurve = serializedObject.FindProperty ("torqueCurve");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (torqueCurve, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty gearSpeedLimitCurve = serializedObject.FindProperty ("gearSpeedLimitCurve");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (gearSpeedLimitCurve, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty revRangeBoundary = serializedObject.FindProperty ("revRangeBoundary");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (revRangeBoundary, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty shiftUp = serializedObject.FindProperty ("shiftUp");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (shiftUp, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty shiftDown = serializedObject.FindProperty ("shiftDown");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (shiftDown, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				EditorUtility.SetDirty (vehicleController);

			}
			EditorGUILayout.EndVertical ();

			EditorGUILayout.BeginVertical ("Box");
			showNitro = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showNitro, "Nitro", true);	
			if (showNitro) {

				SerializedProperty nitroTopSpeed = serializedObject.FindProperty ("nitroTopSpeed");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (nitroTopSpeed, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty nitroFullTorque = serializedObject.FindProperty ("nitroFullTorque");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (nitroFullTorque, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty nitroDuration = serializedObject.FindProperty ("nitroDuration");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (nitroDuration, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty nitroSpendRate = serializedObject.FindProperty ("nitroSpendRate");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (nitroSpendRate, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty nitroRefillRate = serializedObject.FindProperty ("nitroRefillRate");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (nitroRefillRate, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty nitroFX = serializedObject.FindProperty ("nitroFX");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (nitroFX, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty nitroON = serializedObject.FindProperty ("nitroON");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (nitroON, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty nitroOFF = serializedObject.FindProperty ("nitroOFF");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (nitroOFF, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty nitroOn = serializedObject.FindProperty ("nitroOn");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (nitroOn, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();


				EditorUtility.SetDirty (vehicleController);

			}
			EditorGUILayout.EndVertical ();

			EditorGUILayout.BeginVertical ("Box");
			showHandling = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showHandling, "Physics and Handling", true);	
			if (showHandling) {

				SerializedProperty rbody = serializedObject.FindProperty ("rbody");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (rbody, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty centerOfMass = serializedObject.FindProperty ("centerOfMass");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (centerOfMass, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty carDriveType = serializedObject.FindProperty ("carDriveType");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (carDriveType, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty downforce = serializedObject.FindProperty ("downforce");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (downforce, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty slipLimit = serializedObject.FindProperty ("slipLimit");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (slipLimit, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty steerSensitivity = serializedObject.FindProperty ("steerSensitivity");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steerSensitivity, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty maxSteerAngle = serializedObject.FindProperty ("maxSteerAngle");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (maxSteerAngle, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty steerAngleAtMaxSpeed = serializedObject.FindProperty ("steerAngleAtMaxSpeed");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steerAngleAtMaxSpeed, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty steerHelper = serializedObject.FindProperty ("steerHelper");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steerHelper, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty tractionControl = serializedObject.FindProperty ("tractionControl");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (tractionControl, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				EditorUtility.SetDirty (vehicleController);

			}
			EditorGUILayout.EndVertical ();

			EditorGUILayout.BeginVertical ("Box");
			showWheels = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showWheels, "Wheels", true);	
			if (showWheels) {

				SerializedProperty wheelMeshes = serializedObject.FindProperty ("wheelMeshes");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (wheelMeshes, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				if (GUILayout.Button ("Align Wheel Colliders") ) {
					vehicleController.AlignWheelColliders ();
				}

				SerializedProperty wheelColliders = serializedObject.FindProperty ("wheelColliders");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (wheelColliders, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty wheelEffects = serializedObject.FindProperty ("wheelEffects");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (wheelEffects, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();


				EditorUtility.SetDirty (vehicleController);

			}
			EditorGUILayout.EndVertical ();

			EditorGUILayout.BeginVertical ("Box");
			showAIOverride = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showAIOverride, "AI Controller Overrides", true);	
			if (showAIOverride) {

				SerializedProperty overrideBrake = serializedObject.FindProperty ("overrideBrake");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (overrideBrake, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty overrideBrakePower = serializedObject.FindProperty ("overrideBrakePower");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (overrideBrakePower, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty overrideAcceleration = serializedObject.FindProperty ("overrideAcceleration");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (overrideAcceleration, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty overrideAccelerationPower = serializedObject.FindProperty ("overrideAccelerationPower");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (overrideAccelerationPower, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty overrideSteering = serializedObject.FindProperty ("overrideSteering");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (overrideSteering, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				SerializedProperty overrideSteeringPower = serializedObject.FindProperty ("overrideSteeringPower");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (overrideSteeringPower, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();


				EditorUtility.SetDirty (vehicleController);

			}
			EditorGUILayout.EndVertical ();

		}

	}
}
//
//
///// Input Settings
//if(inputSettings){
//	EditorGUILayout.BeginVertical("Box");
//	EditorGUILayout.LabelField ("Input Manager Axes Settings", editorSkin.customStyles [0]);
//	EditorGUILayout.EndVertical();
//	SerializedObject serializedObject = new SerializedObject (AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
//
//	SerializedProperty axisProperty = serializedObject.FindProperty ("m_Axes");
//	EditorGUI.BeginChangeCheck ();
//	EditorGUILayout.PropertyField (axisProperty, true);
//	if (EditorGUI.EndChangeCheck ())
//		serializedObject.ApplyModifiedProperties ();
//	EditorGUILayout.BeginVertical("Box");
//	EditorGUILayout.LabelField ("Keyboard Input Settings", editorSkin.customStyles [0]);
//	EditorGUILayout.EndVertical();
//
//	RGT_PlayerPrefs.inputData.pauseKey = (KeyCode)EditorGUILayout.EnumPopup ("Pause", RGT_PlayerPrefs.inputData.pauseKey);
//	RGT_PlayerPrefs.inputData.cameraSwitchKey = (KeyCode)EditorGUILayout.EnumPopup ("Camera Switch", RGT_PlayerPrefs.inputData.cameraSwitchKey);
//	RGT_PlayerPrefs.inputData.nitroKey = (KeyCode)EditorGUILayout.EnumPopup ("Nitro", RGT_PlayerPrefs.inputData.nitroKey);
//	RGT_PlayerPrefs.inputData.shiftUp = (KeyCode)EditorGUILayout.EnumPopup ("Shift Up", RGT_PlayerPrefs.inputData.shiftUp);
//	RGT_PlayerPrefs.inputData.shiftDown = (KeyCode)EditorGUILayout.EnumPopup ("Shift Down", RGT_PlayerPrefs.inputData.shiftDown);
//	RGT_PlayerPrefs.inputData.lookBack = (KeyCode)EditorGUILayout.EnumPopup ("Look Back", RGT_PlayerPrefs.inputData.lookBack);
//
//	EditorGUILayout.BeginVertical("Box");
//	EditorGUILayout.LabelField ("Joystick Input Settings", editorSkin.customStyles [0]);
//	EditorGUILayout.EndVertical();
//
//	RGT_PlayerPrefs.inputData._pauseJoystick = (InputData.Joystick)EditorGUILayout.EnumPopup ("Pause", RGT_PlayerPrefs.inputData._pauseJoystick);
//	switch(RGT_PlayerPrefs.inputData._pauseJoystick){
//	case InputData.Joystick.None:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.None;
//		break;
//	case InputData.Joystick.JoystickButton0:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton0; 
//		break;
//	case InputData.Joystick.JoystickButton1:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton1; 
//		break;
//	case InputData.Joystick.JoystickButton2:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton2; 
//		break;
//	case InputData.Joystick.JoystickButton3:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton3; 
//		break;
//	case InputData.Joystick.JoystickButton4:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton4; 
//		break;
//	case InputData.Joystick.JoystickButton5:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton5; 
//		break;
//	case InputData.Joystick.JoystickButton6:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton6; 
//		break;
//	case InputData.Joystick.JoystickButton7:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton7; 
//		break;
//	case InputData.Joystick.JoystickButton8:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton8; 
//		break;
//	case InputData.Joystick.JoystickButton9:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton9; 
//		break;
//	case InputData.Joystick.JoystickButton10:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton10; 
//		break;
//	case InputData.Joystick.JoystickButton11:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton11; 
//		break;
//	case InputData.Joystick.JoystickButton12:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton12; 
//		break;
//	case InputData.Joystick.JoystickButton13:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton13; 
//		break;
//	case InputData.Joystick.JoystickButton14:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton14; 
//		break;
//	case InputData.Joystick.JoystickButton15:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton15; 
//		break;
//	case InputData.Joystick.JoystickButton16:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton16; 
//		break;
//	case InputData.Joystick.JoystickButton17:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton17; 
//		break;
//	case InputData.Joystick.JoystickButton18:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton18; 
//		break;
//	case InputData.Joystick.JoystickButton19:
//		RGT_PlayerPrefs.inputData.pauseJoystick = KeyCode.JoystickButton19;
//		break;
//	}
//	RGT_PlayerPrefs.inputData._cameraSwitchJoystick = (InputData.Joystick)EditorGUILayout.EnumPopup ("Camera Switch", RGT_PlayerPrefs.inputData._cameraSwitchJoystick);
//	switch(RGT_PlayerPrefs.inputData._cameraSwitchJoystick){
//	case InputData.Joystick.None:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.None;
//		break;
//	case InputData.Joystick.JoystickButton0:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton0; 
//		break;
//	case InputData.Joystick.JoystickButton1:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton1; 
//		break;
//	case InputData.Joystick.JoystickButton2:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton2; 
//		break;
//	case InputData.Joystick.JoystickButton3:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton3; 
//		break;
//	case InputData.Joystick.JoystickButton4:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton4; 
//		break;
//	case InputData.Joystick.JoystickButton5:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton5; 
//		break;
//	case InputData.Joystick.JoystickButton6:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton6; 
//		break;
//	case InputData.Joystick.JoystickButton7:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton7; 
//		break;
//	case InputData.Joystick.JoystickButton8:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton8; 
//		break;
//	case InputData.Joystick.JoystickButton9:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton9; 
//		break;
//	case InputData.Joystick.JoystickButton10:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton10; 
//		break;
//	case InputData.Joystick.JoystickButton11:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton11; 
//		break;
//	case InputData.Joystick.JoystickButton12:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton12; 
//		break;
//	case InputData.Joystick.JoystickButton13:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton13; 
//		break;
//	case InputData.Joystick.JoystickButton14:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton14; 
//		break;
//	case InputData.Joystick.JoystickButton15:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton15; 
//		break;
//	case InputData.Joystick.JoystickButton16:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton16; 
//		break;
//	case InputData.Joystick.JoystickButton17:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton17; 
//		break;
//	case InputData.Joystick.JoystickButton18:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton18; 
//		break;
//	case InputData.Joystick.JoystickButton19:
//		RGT_PlayerPrefs.inputData.cameraSwitchJoystick = KeyCode.JoystickButton19;
//		break;
//	}
//	RGT_PlayerPrefs.inputData._nitroJoystick = (InputData.Joystick)EditorGUILayout.EnumPopup ("Nitro", RGT_PlayerPrefs.inputData._nitroJoystick);
//	switch(RGT_PlayerPrefs.inputData._nitroJoystick){
//	case InputData.Joystick.None:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.None;
//		break;
//	case InputData.Joystick.JoystickButton0:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton0; 
//		break;
//	case InputData.Joystick.JoystickButton1:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton1; 
//		break;
//	case InputData.Joystick.JoystickButton2:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton2; 
//		break;
//	case InputData.Joystick.JoystickButton3:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton3; 
//		break;
//	case InputData.Joystick.JoystickButton4:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton4; 
//		break;
//	case InputData.Joystick.JoystickButton5:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton5; 
//		break;
//	case InputData.Joystick.JoystickButton6:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton6; 
//		break;
//	case InputData.Joystick.JoystickButton7:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton7; 
//		break;
//	case InputData.Joystick.JoystickButton8:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton8; 
//		break;
//	case InputData.Joystick.JoystickButton9:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton9; 
//		break;
//	case InputData.Joystick.JoystickButton10:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton10; 
//		break;
//	case InputData.Joystick.JoystickButton11:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton11; 
//		break;
//	case InputData.Joystick.JoystickButton12:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton12; 
//		break;
//	case InputData.Joystick.JoystickButton13:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton13; 
//		break;
//	case InputData.Joystick.JoystickButton14:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton14; 
//		break;
//	case InputData.Joystick.JoystickButton15:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton15; 
//		break;
//	case InputData.Joystick.JoystickButton16:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton16; 
//		break;
//	case InputData.Joystick.JoystickButton17:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton17; 
//		break;
//	case InputData.Joystick.JoystickButton18:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton18; 
//		break;
//	case InputData.Joystick.JoystickButton19:
//		RGT_PlayerPrefs.inputData.nitroJoystick = KeyCode.JoystickButton19;
//		break;
//	}
//	RGT_PlayerPrefs.inputData._shiftUpJoystick = (InputData.Joystick)EditorGUILayout.EnumPopup ("Shift Up", RGT_PlayerPrefs.inputData._shiftUpJoystick);
//	switch(RGT_PlayerPrefs.inputData._shiftUpJoystick){
//	case InputData.Joystick.None:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.None;
//		break;
//	case InputData.Joystick.JoystickButton0:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton0; 
//		break;
//	case InputData.Joystick.JoystickButton1:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton1; 
//		break;
//	case InputData.Joystick.JoystickButton2:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton2; 
//		break;
//	case InputData.Joystick.JoystickButton3:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton3; 
//		break;
//	case InputData.Joystick.JoystickButton4:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton4; 
//		break;
//	case InputData.Joystick.JoystickButton5:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton5; 
//		break;
//	case InputData.Joystick.JoystickButton6:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton6; 
//		break;
//	case InputData.Joystick.JoystickButton7:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton7; 
//		break;
//	case InputData.Joystick.JoystickButton8:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton8; 
//		break;
//	case InputData.Joystick.JoystickButton9:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton9; 
//		break;
//	case InputData.Joystick.JoystickButton10:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton10; 
//		break;
//	case InputData.Joystick.JoystickButton11:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton11; 
//		break;
//	case InputData.Joystick.JoystickButton12:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton12; 
//		break;
//	case InputData.Joystick.JoystickButton13:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton13; 
//		break;
//	case InputData.Joystick.JoystickButton14:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton14; 
//		break;
//	case InputData.Joystick.JoystickButton15:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton15; 
//		break;
//	case InputData.Joystick.JoystickButton16:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton16; 
//		break;
//	case InputData.Joystick.JoystickButton17:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton17; 
//		break;
//	case InputData.Joystick.JoystickButton18:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton18; 
//		break;
//	case InputData.Joystick.JoystickButton19:
//		RGT_PlayerPrefs.inputData.shiftUpJoystick = KeyCode.JoystickButton19;
//		break;
//	}
//	RGT_PlayerPrefs.inputData._shiftDownJoystick = (InputData.Joystick)EditorGUILayout.EnumPopup ("Shift Down", RGT_PlayerPrefs.inputData._shiftDownJoystick);
//	switch(RGT_PlayerPrefs.inputData._shiftDownJoystick){
//	case InputData.Joystick.None:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.None;
//		break;
//	case InputData.Joystick.JoystickButton0:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton0; 
//		break;
//	case InputData.Joystick.JoystickButton1:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton1; 
//		break;
//	case InputData.Joystick.JoystickButton2:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton2; 
//		break;
//	case InputData.Joystick.JoystickButton3:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton3; 
//		break;
//	case InputData.Joystick.JoystickButton4:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton4; 
//		break;
//	case InputData.Joystick.JoystickButton5:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton5; 
//		break;
//	case InputData.Joystick.JoystickButton6:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton6; 
//		break;
//	case InputData.Joystick.JoystickButton7:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton7; 
//		break;
//	case InputData.Joystick.JoystickButton8:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton8; 
//		break;
//	case InputData.Joystick.JoystickButton9:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton9; 
//		break;
//	case InputData.Joystick.JoystickButton10:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton10; 
//		break;
//	case InputData.Joystick.JoystickButton11:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton11; 
//		break;
//	case InputData.Joystick.JoystickButton12:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton12; 
//		break;
//	case InputData.Joystick.JoystickButton13:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton13; 
//		break;
//	case InputData.Joystick.JoystickButton14:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton14; 
//		break;
//	case InputData.Joystick.JoystickButton15:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton15; 
//		break;
//	case InputData.Joystick.JoystickButton16:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton16; 
//		break;
//	case InputData.Joystick.JoystickButton17:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton17; 
//		break;
//	case InputData.Joystick.JoystickButton18:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton18; 
//		break;
//	case InputData.Joystick.JoystickButton19:
//		RGT_PlayerPrefs.inputData.shiftDownJoystick = KeyCode.JoystickButton19;
//		break;
//	}
//	RGT_PlayerPrefs.inputData._lookBackJoystick = (InputData.Joystick)EditorGUILayout.EnumPopup ("Look Back", RGT_PlayerPrefs.inputData._lookBackJoystick);
//	switch(RGT_PlayerPrefs.inputData._lookBackJoystick){
//	case InputData.Joystick.None:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.None;
//		break;
//	case InputData.Joystick.JoystickButton0:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton0; 
//		break;
//	case InputData.Joystick.JoystickButton1:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton1; 
//		break;
//	case InputData.Joystick.JoystickButton2:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton2; 
//		break;
//	case InputData.Joystick.JoystickButton3:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton3; 
//		break;
//	case InputData.Joystick.JoystickButton4:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton4; 
//		break;
//	case InputData.Joystick.JoystickButton5:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton5; 
//		break;
//	case InputData.Joystick.JoystickButton6:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton6; 
//		break;
//	case InputData.Joystick.JoystickButton7:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton7; 
//		break;
//	case InputData.Joystick.JoystickButton8:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton8; 
//		break;
//	case InputData.Joystick.JoystickButton9:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton9; 
//		break;
//	case InputData.Joystick.JoystickButton10:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton10; 
//		break;
//	case InputData.Joystick.JoystickButton11:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton11; 
//		break;
//	case InputData.Joystick.JoystickButton12:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton12; 
//		break;
//	case InputData.Joystick.JoystickButton13:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton13; 
//		break;
//	case InputData.Joystick.JoystickButton14:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton14; 
//		break;
//	case InputData.Joystick.JoystickButton15:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton15; 
//		break;
//	case InputData.Joystick.JoystickButton16:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton16; 
//		break;
//	case InputData.Joystick.JoystickButton17:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton17; 
//		break;
//	case InputData.Joystick.JoystickButton18:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton18; 
//		break;
//	case InputData.Joystick.JoystickButton19:
//		RGT_PlayerPrefs.inputData.lookBackJoystick = KeyCode.JoystickButton19;
//		break;
//	}
//
//	EditorGUILayout.BeginVertical("Box");
//	EditorGUILayout.LabelField ("Mobile Settings", editorSkin.customStyles [0]);
//	EditorGUILayout.EndVertical();
//	RGT_PlayerPrefs.inputData.useMobileController = EditorGUILayout.Toggle("Use Mobile Controller", RGT_PlayerPrefs.inputData.useMobileController);
//	RGT_PlayerPrefs.inputData.mobileController = (GameObject) EditorGUILayout.ObjectField("Prefab", RGT_PlayerPrefs.inputData.mobileController, typeof (GameObject), false );
//
//	EditorUtility.SetDirty (RGT_PlayerPrefs.inputData);
//}