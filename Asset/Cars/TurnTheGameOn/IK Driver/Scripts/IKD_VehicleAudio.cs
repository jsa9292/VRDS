using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TurnTheGameOn.IKDriver{
	[RequireComponent(typeof (IKD_VehicleController))]
	public class IKD_VehicleAudio : MonoBehaviour {

		// This script reads some of the car's current properties and plays sounds accordingly.
		// The engine sound can be a simple single clip which is looped and pitched, or it
		// can be a crossfaded blend of four clips which represent the timbre of the engine
		// at different RPM and Throttle state.

		// the engine clips should all be a steady pitch, not rising or falling.

		// when using four channel engine crossfading, the four clips should be:
		// lowAccelClip : The engine at low revs, with throttle open (i.e. begining acceleration at very low speed)
		// highAccelClip : Thenengine at high revs, with throttle open (i.e. accelerating, but almost at max speed)
		// lowDecelClip : The engine at low revs, with throttle at minimum (i.e. idling or engine-braking at very low speed)
		// highDecelClip : Thenengine at high revs, with throttle at minimum (i.e. engine-braking at very high speed)

		// For proper crossfading, the clips pitches should all match, with an octave offset between low and high.

		#region Public Variables
		public enum EngineAudioOptions{
			Simple, // Simple style audio
			FourChannel // four Channel audio
		}

		public EngineAudioOptions engineSoundStyle = EngineAudioOptions.FourChannel;// Set the default audio options to be four channel
		public AudioSource shiftSource;
		public AudioClip lowAccelClip;                                              // Audio clip for low acceleration
		public AudioClip lowDecelClip;                                              // Audio clip for low deceleration
		public AudioClip highAccelClip;                                             // Audio clip for high acceleration
		public AudioClip highDecelClip;                                             // Audio clip for high deceleration
		public float pitchMultiplier = 1f;                                          // Used for altering the pitch of audio clips
		public float lowPitchMin = 1f;                                              // The lowest possible pitch for the low sounds
		public float lowPitchMax = 6f;                                              // The highest possible pitch for the low sounds
		public float highPitchMultiplier = 0.25f;                                   // Used for altering the pitch of high sounds
		public float maxRolloffDistance = 500;                                      // The maximum distance where rollof starts to take place
		public float dopplerLevel = 1;                                              // The mount of doppler effect used in the audio
		public bool useDoppler = true;                                              // Toggle for using doppler
		public bool fadeIn;
		public bool fadeOut;
		public float fadeInVolume = 0.0f;
		public float fadeOutVolume = 1.0f;
		#endregion
		
		#region Private Variables
		private string gearCheck;
		private AudioSource lowAccel; // Source for the low acceleration sounds
		private AudioSource lowDecel; // Source for the low deceleration sounds
		private AudioSource lhighAccel; // Source for the high acceleration sounds
		private AudioSource lhighDecel; // Source for the high deceleration sounds
		private bool startedSound; // flag for knowing if we have started sounds
		private IKD_VehicleController carController; // Reference to car we are controlling
		float camDist;
		AudioSource sourceRef;
		Text gearText;
		#endregion
		
		#region Main Methods
		void Start () {
			if (gearText == null) {
				gearText = GameObject.Find ("Gear Text").GetComponent<Text>();
			}
			fadeIn = false;
			fadeOut = false;
		}

		void Update(){
			if (shiftSource != null) {
				if (carController != null && gearText != null && gearCheck != gearText.text) {
					gearCheck = gearText.text;
					shiftSource.Play ();
				}
			}
			if(sourceRef && sourceRef.volume != 1.0f && fadeIn){
				fadeInVolume += 0.1f * Time.deltaTime * 4f;
				lhighAccel.volume = fadeInVolume;
			}
			if(sourceRef && fadeOut){
				fadeOutVolume -= 0.1f * Time.deltaTime * 4f;
				sourceRef.volume = fadeOutVolume;
				if(sourceRef.volume <= 0.0f){
					//Destroy all audio sources on this object:
					foreach (var source in GetComponents<AudioSource>()){
						fadeOut = false;
						Destroy(source);
						sourceRef = null;
					}
				}
			}
			// get the distance to main camera
			if (Camera.main) camDist = (Camera.main.transform.position - transform.position).sqrMagnitude;

			// stop sound if the object is beyond the maximum roll off distance
			if (startedSound && camDist > maxRolloffDistance*maxRolloffDistance){
				StopSound();
			}

			// start the sound if not playing and it is nearer than the maximum distance
			if (!startedSound && camDist < maxRolloffDistance*maxRolloffDistance)	{
				StartSound();
			}

			if (startedSound)	{
				// The pitch is interpolated between the min and max values, according to the car's revs.
				float pitch = ULerp(lowPitchMin, lowPitchMax, carController.Revs);

				// clamp to minimum pitch (note, not clamped to max for high revs while burning out)
				pitch = Mathf.Min(lowPitchMax, pitch);

				if (engineSoundStyle == EngineAudioOptions.Simple){
					// for 1 channel engine sound, it's oh so simple:
					lhighAccel.pitch = pitch*pitchMultiplier*highPitchMultiplier;
					lhighAccel.dopplerLevel = useDoppler ? dopplerLevel : 0;
					lhighAccel.volume = 1;// * fadeInVolume;
				}
				else{
					// for 4 channel engine sound, it's a little more complex:

					// adjust the pitches based on the multipliers
					lowAccel.pitch = pitch*pitchMultiplier;
					lowDecel.pitch = pitch*pitchMultiplier;
					lhighAccel.pitch = pitch*highPitchMultiplier*pitchMultiplier;
					lhighDecel.pitch = pitch*highPitchMultiplier*pitchMultiplier;

					// get values for fading the sounds based on the acceleration
					float accFade = Mathf.Abs(carController.AccelInput);
					float decFade = 1 - accFade;

					// get the high fade value based on the cars revs
					float highFade = Mathf.InverseLerp(0.2f, 0.8f, carController.Revs);
					float lowFade = 1 - highFade;

					// adjust the values to be more realistic
					highFade = 1 - ((1 - highFade)*(1 - highFade));
					lowFade = 1 - ((1 - lowFade)*(1 - lowFade));
					accFade = 1 - ((1 - accFade)*(1 - accFade));
					decFade = 1 - ((1 - decFade)*(1 - decFade));

					// adjust the source volumes based on the fade values
					lowAccel.volume = lowFade*accFade * fadeInVolume;
					lowDecel.volume = lowFade*decFade * fadeInVolume;
					lhighAccel.volume = highFade*accFade * fadeInVolume;
					lhighDecel.volume = highFade*decFade * fadeInVolume;

					// adjust the doppler levels
					lhighAccel.dopplerLevel = useDoppler ? dopplerLevel : 0;
					lowAccel.dopplerLevel = useDoppler ? dopplerLevel : 0;
					lhighDecel.dopplerLevel = useDoppler ? dopplerLevel : 0;
					lowDecel.dopplerLevel = useDoppler ? dopplerLevel : 0;
				}
			}
		}
		#endregion
		
		#region Utility Methods
		void StartSound(){
			fadeOut = false;
			// get the carcontroller ( this will not be null as we have require component)
			carController = GetComponent<IKD_VehicleController>();

			// setup the simple audio source
			lhighAccel = SetUpEngineAudioSource(highAccelClip);

			// if we have four channel audio setup the four audio sources
			if (engineSoundStyle == EngineAudioOptions.FourChannel)
			{
				lowAccel = SetUpEngineAudioSource(lowAccelClip);
				lowDecel = SetUpEngineAudioSource(lowDecelClip);
				lhighDecel = SetUpEngineAudioSource(highDecelClip);
			}
			fadeInVolume = 0f;
			// flag that we have started the sounds playing
			startedSound = true;
			fadeIn = true;
		}

		void StopSound(){
			fadeIn = false;
			//Destroy all audio sources on this object:
			foreach (var source in GetComponents<AudioSource>()){
				Destroy(source);
			}
			startedSound = false;
			fadeOut = true;
		}

		// sets up and adds new audio source to the gane object
		private AudioSource SetUpEngineAudioSource(AudioClip clip){
			// create the new audio source component on the game object and set up its properties
			AudioSource source = gameObject.AddComponent<AudioSource>();
			sourceRef = source;
			source.clip = clip;
			source.volume = 0;
			source.loop = true;

			// start the clip from a random point
			source.time = Random.Range(0f, clip.length);
			source.Play();
			source.minDistance = 5;
			source.maxDistance = maxRolloffDistance;
			source.dopplerLevel = 0;
			return source;
		}

		// unclamped versions of Lerp and Inverse Lerp, to allow value to exceed the from-to range
		private static float ULerp(float from, float to, float value){
			return (1.0f - value)*from + value*to;
		}
		#endregion
		
	}
}