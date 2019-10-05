using UnityEngine;
using System.Collections;

namespace TurnTheGameOn.IKDriver{
	public class IKD_DetatchOnAwake : MonoBehaviour {
		
		#region Main Methods	
		void Awake () {
			transform.SetParent (null);
			Destroy (this);
		}
		#endregion
		
	}
}