using UnityEngine;
using UnityEditor;
using System.Collections;

namespace TurnTheGameOn.IKDriver {
	[CustomEditor(typeof(IKD_IKDriver))]
	public class Editor_IKD_IKDriver : Editor {

		float minValue = -1.0f;
		float minLimit = -3.0f;
		float maxValue = 1.0f;
		float maxLimit = 3.0f;

		float minValueSpeed = 0.001f;
		float minLimitSpeed = 0f;
		float maxValueSpeed = 5.0f;
		float maxLimitSpeed = 10.0f;

		bool showCurrentIKDriverTargets;
		bool showCurrentIKTargetObjects;
		bool showIKSteeringWheelTargets;
		bool showOtherIKTargetObjects;

		public override void OnInspectorGUI(){

			IKD_IKDriver ikd_IKDriver = (IKD_IKDriver)target;
			minValue = ikd_IKDriver.maxLookLeft;
			maxValue = ikd_IKDriver.maxLookRight;
			minValueSpeed = ikd_IKDriver.minLookSpeed;
			maxValueSpeed = ikd_IKDriver.maxLookSpeed;
			EditorGUILayout.BeginVertical("Box");
			///
			SerializedProperty ikActive = serializedObject.FindProperty("ikActive");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(ikActive, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty mobile = serializedObject.FindProperty("mobile");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(mobile, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty steeringAxis = serializedObject.FindProperty("steeringAxis");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(steeringAxis, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty throttleAxis = serializedObject.FindProperty("throttleAxis");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(throttleAxis, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();


			///
			EditorGUILayout.BeginVertical("Box");
			///
			SerializedProperty enableShifting = serializedObject.FindProperty("enableShifting");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(enableShifting, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty shift = serializedObject.FindProperty("shift");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(shift, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty gearText = serializedObject.FindProperty("gearText");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(gearText, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			EditorGUILayout.EndVertical();


			EditorGUILayout.BeginVertical("Box");
			///
			SerializedProperty brakeFoot = serializedObject.FindProperty("brakeFoot");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(brakeFoot, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty gasFoot = serializedObject.FindProperty("gasFoot");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(gasFoot, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			EditorGUILayout.EndVertical();



			///
			EditorGUILayout.BeginVertical("Box");
			SerializedProperty steeringTargets = serializedObject.FindProperty("steeringTargets");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(steeringTargets, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			if (ikd_IKDriver.steeringTargets == IKD_IKDriver.SteeringTargets.All) {
				SerializedProperty steeringWheelRotation = serializedObject.FindProperty ("steeringWheelRotation");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringWheelRotation, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
			} else if(ikd_IKDriver.steeringTargets == IKD_IKDriver.SteeringTargets.Two){
				///
				SerializedProperty steeringWheelRotationTwoTargets = serializedObject.FindProperty ("steeringWheelRotationTwoTargets");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringWheelRotationTwoTargets, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
			}
			///
			SerializedProperty steeringRotationSpeed = serializedObject.FindProperty("steeringRotationSpeed");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(steeringRotationSpeed, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty wheelShake = serializedObject.FindProperty("wheelShake");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(wheelShake, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty defaultSteering = serializedObject.FindProperty("defaultSteering");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(defaultSteering, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty steeringWheel = serializedObject.FindProperty("steeringWheel");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(steeringWheel, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty wheelCollider = serializedObject.FindProperty("wheelCollider");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(wheelCollider, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			EditorGUILayout.EndVertical();

			///
			EditorGUILayout.BeginVertical("Box");
			ikd_IKDriver.avatarPosition = EditorGUILayout.Vector3Field("Avatar Position", ikd_IKDriver.avatarPosition);
			EditorGUILayout.EndVertical();

			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.LabelField ("IK Head Look Settings");

			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField ("Look Range", GUILayout.MaxWidth(Screen.width * 0.3f));

			EditorGUILayout.MinMaxSlider( ref minValue, ref maxValue, minLimit, maxLimit );
			EditorGUILayout.EndHorizontal();
			minValue = EditorGUILayout.FloatField ("Max Look Left", minValue);
			maxValue = EditorGUILayout.FloatField ("Max Look Right", maxValue);
			EditorGUI.BeginChangeCheck();
			ikd_IKDriver.maxLookLeft = minValue;
			EditorGUI.BeginChangeCheck();
			ikd_IKDriver.maxLookRight = maxValue;
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			///
			SerializedProperty defaultLookXPos = serializedObject.FindProperty("defaultLookXPos");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(defaultLookXPos, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			EditorGUILayout.EndVertical();

			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField ("Look Speed", GUILayout.MaxWidth(Screen.width * 0.3f));
			EditorGUILayout.MinMaxSlider( ref minValueSpeed, ref maxValueSpeed, minLimitSpeed, maxLimitSpeed );
			EditorGUILayout.EndHorizontal();
			minValueSpeed = EditorGUILayout.FloatField ("Steer Look Speed", minValueSpeed);
			maxValueSpeed = EditorGUILayout.FloatField ("Snap Back Speed", maxValueSpeed);
			ikd_IKDriver.minLookSpeed = minValueSpeed;
			ikd_IKDriver.maxLookSpeed = maxValueSpeed;
			EditorGUILayout.EndVertical();
			//EditorGUILayout.LabelField ("Right: " + maxValue.ToString("F3"), GUILayout.MaxWidth(Screen.width * 0.3f));
			//EditorGUILayout.EndHorizontal();








			EditorGUILayout.EndVertical();


			EditorGUILayout.BeginVertical("Box");
			showCurrentIKDriverTargets = EditorGUILayout.Foldout (showCurrentIKDriverTargets, "Current IK Driver Targets");
			if (showCurrentIKDriverTargets) {
				EditorGUILayout.BeginVertical ("Box");
				///Steering Wheel Target W
				SerializedProperty targetRightHandIK = serializedObject.FindProperty ("targetRightHandIK");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (targetRightHandIK, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
				///Steering Wheel Target W
				SerializedProperty rightHandTarget = serializedObject.FindProperty ("rightHandTarget");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (rightHandTarget, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
				///Steering Wheel Target W
				SerializedProperty targetLeftHandIK = serializedObject.FindProperty ("targetLeftHandIK");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (targetLeftHandIK, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				///Steering Wheel Target W
				SerializedProperty targetRightFootIK = serializedObject.FindProperty ("targetRightFootIK");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (targetRightFootIK, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				///Steering Wheel Target W
				SerializedProperty targetLeftFootIK = serializedObject.FindProperty ("targetLeftFootIK");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (targetLeftFootIK, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
				EditorGUILayout.EndVertical ();
			}

			showCurrentIKTargetObjects = EditorGUILayout.Foldout (showCurrentIKTargetObjects, "Current IK Target Objects");
			if (showCurrentIKTargetObjects) {
				EditorGUILayout.BeginVertical ("Box");
				///
				SerializedProperty lookObj = serializedObject.FindProperty ("lookObj");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (lookObj, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	
				///
				SerializedProperty rightHandObj = serializedObject.FindProperty ("rightHandObj");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (rightHandObj, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
				///
				SerializedProperty leftHandObj = serializedObject.FindProperty ("leftHandObj");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (leftHandObj, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	
				///
				SerializedProperty leftFootObj = serializedObject.FindProperty ("leftFootObj");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (leftFootObj, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	
				///
				SerializedProperty rightFootObj = serializedObject.FindProperty ("rightFootObj");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (rightFootObj, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	
				EditorGUILayout.EndVertical ();
			}

			showIKSteeringWheelTargets = EditorGUILayout.Foldout (showIKSteeringWheelTargets, "IK Steering Wheel Targets");
			if (showIKSteeringWheelTargets) {
				EditorGUILayout.BeginVertical ("Box");
				///Steering Wheel Target W
				SerializedProperty steeringW = serializedObject.FindProperty ("steeringW");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringW, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();

				///Steering Wheel Target NW
				SerializedProperty steeringNW = serializedObject.FindProperty ("steeringNW");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringNW, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	

				///Steering Wheel Target N
				SerializedProperty steeringN = serializedObject.FindProperty ("steeringN");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringN, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	

				///Steering Wheel Target NE
				SerializedProperty steeringNE = serializedObject.FindProperty ("steeringNE");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringNE, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	

				///Steering Wheel Target E
				SerializedProperty steeringE = serializedObject.FindProperty ("steeringE");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringE, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	

				///Steering Wheel Target SE
				SerializedProperty steeringSE = serializedObject.FindProperty ("steeringSE");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringSE, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	

				///Steering Wheel Target S
				SerializedProperty steeringS = serializedObject.FindProperty ("steeringS");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringS, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	

				///Steering Wheel Target SW
				SerializedProperty steeringSW = serializedObject.FindProperty ("steeringSW");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (steeringSW, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	
				EditorGUILayout.EndVertical ();
			}


			showOtherIKTargetObjects = EditorGUILayout.Foldout (showOtherIKTargetObjects, "Other IK Target Objects");
			if (showOtherIKTargetObjects) {
				///
				EditorGUILayout.BeginVertical ("Box");
				SerializedProperty shiftObj = serializedObject.FindProperty ("shiftObj");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (shiftObj, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();	
				///
				SerializedProperty footBrake = serializedObject.FindProperty ("footBrake");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (footBrake, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
				///
				SerializedProperty footGas = serializedObject.FindProperty ("footGas");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (footGas, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
				///
				SerializedProperty leftFootIdle = serializedObject.FindProperty ("leftFootIdle");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (leftFootIdle, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
				///
				SerializedProperty leftFootClutch = serializedObject.FindProperty ("leftFootClutch");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (leftFootClutch, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
				///
				SerializedProperty rightFootIdle = serializedObject.FindProperty ("rightFootIdle");
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (rightFootIdle, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();					

				EditorGUILayout.EndVertical ();
			}
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
		}

	}
}