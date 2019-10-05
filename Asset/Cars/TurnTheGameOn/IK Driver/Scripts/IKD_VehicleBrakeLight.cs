using UnityEngine;
using System.Collections;

namespace TurnTheGameOn.IKDriver{
	public class IKD_VehicleBrakeLight : MonoBehaviour {
		
		#region Public Variables
		public IKD_VehicleController vehicleController;

		public Color _colorReverse = Color.white;
		public Color _colorBrakeOn = Color.red;
		public Color _colorBrakeOff = Color.grey;
		public GameObject meshRendObject;
		public int materialIndex;
		public GameObject[] brakeLights;
		public GameObject[] reverseLights;
		#endregion

		#region Private Variables
		private Material brakeMaterial;
		private bool inputPressed;
		#endregion
		
		#region Main Methods	
		void Start(){
			brakeMaterial = meshRendObject.GetComponent<MeshRenderer> ().materials [materialIndex];
			brakeMaterial.EnableKeyword ("_EMISSION");
		}

		void Update(){
			if ( Input.GetAxis("Vertical") < 0 && vehicleController.reversing) {
				TurnOnReverseLights ();
				inputPressed = true;
			}
			else if ( Input.GetAxis("Vertical") < 0	) {
					TurnOnBrakeLights ();
					inputPressed = true;
			} else {
				Invoke("TurnOff", 0.5f);
				inputPressed = false;
			}
		}

		void TurnOff(){
			if (!inputPressed) {
				brakeMaterial.SetColor ("_EmissionColor", _colorBrakeOff);
				TurnOffReverseLights ();
				TurnOffBrakeLights ();
			}
		}

		void TurnOnBrakeLights () {
			TurnOffReverseLights ();
			brakeMaterial.SetColor ("_EmissionColor", _colorBrakeOn);
			for (int i = 0; i < brakeLights.Length; i++) {
				brakeLights [i].SetActive (true);
			}
		}

		void TurnOffBrakeLights () {
			for (int i = 0; i < brakeLights.Length; i++) {
				brakeLights [i].SetActive (false);
			}
		}

		void TurnOnReverseLights () {
			TurnOffBrakeLights ();
			brakeMaterial.SetColor ("_EmissionColor", _colorReverse);
			for (int i = 0; i < reverseLights.Length; i++) {
				reverseLights [i].SetActive (true);
			}
		}

		void TurnOffReverseLights () {
			for (int i = 0; i < reverseLights.Length; i++) {
				reverseLights [i].SetActive (false);
			}
		}
		#endregion
		
	}
}