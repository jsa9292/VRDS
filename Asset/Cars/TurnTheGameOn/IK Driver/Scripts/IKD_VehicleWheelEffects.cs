using UnityEngine;
using System.Collections;

namespace TurnTheGameOn.IKDriver{
	public class IKD_VehicleWheelEffects : MonoBehaviour {
		
		#region Public Variables
		public Transform SkidTrailPrefab;
		public static Transform skidTrailsDetachedParent;
		public ParticleSystem skidParticles;
		public bool skidding { get; private set; }
		public bool PlayingAudio { get; private set; }
		#endregion
		
		#region Public Variables
		private AudioSource audioSource;
		private Transform skidTrail;
		private WheelCollider wheelCollider;
		#endregion
		
		#region Main Methods	
		void Start(){
			wheelCollider = GetComponent<WheelCollider>();
			audioSource = GetComponent<AudioSource>();
			PlayingAudio = false;
			if (skidTrailsDetachedParent == null){
				skidTrailsDetachedParent = new GameObject("Skid Trails - Detached").transform;
			}
		}
		#endregion
		
		#region Utility Methods
		public void EmitTyreSmoke()	{
			if (skidParticles != null) {
				skidParticles.transform.position = transform.position - transform.up * wheelCollider.radius;
				skidParticles.Emit (1);
				if (!skidding) {
					StartCoroutine (StartSkidTrail ());
				}
			}
		}

		public void PlayAudio(){
			if (audioSource.enabled) {
				audioSource.Play ();
				PlayingAudio = true;
			}
		}

		public void StopAudio(){
			audioSource.Stop();
			PlayingAudio = false;
		}

		public IEnumerator StartSkidTrail(){
			skidding = true;
			skidTrail = Instantiate(SkidTrailPrefab);
			while (skidTrail == null){
				yield return null;
			}
			skidTrail.parent = transform;
			skidTrail.localPosition = -Vector3.up*wheelCollider.radius;
		}

		public void EndSkidTrail(){
			if (!skidding){
				return;
			}
			skidding = false;
			skidTrail.parent = skidTrailsDetachedParent;
			Destroy(skidTrail.gameObject, 10);
		}
		#endregion
		
	}
}