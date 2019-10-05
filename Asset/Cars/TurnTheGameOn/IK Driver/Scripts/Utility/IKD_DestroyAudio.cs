using UnityEngine;
using System.Collections;

namespace TurnTheGameOn.IKDriver {
	public class IKD_DestroyAudio : MonoBehaviour {

		void Update () {
			if(!GetComponent<AudioSource>().isPlaying){
				Destroy(gameObject);
			}
		}

	}
}