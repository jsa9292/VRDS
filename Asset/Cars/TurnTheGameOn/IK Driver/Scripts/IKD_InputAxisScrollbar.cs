using UnityEngine;
using System.Collections;

namespace TurnTheGameOn.IKDriver {
	public class IKD_InputAxisScrollbar : MonoBehaviour {

		public string axis;

		public void HandleInput(float value){
			IKD_CrossPlatformInputManager.SetAxis(axis, (value*2f) - 1f);
		}

	}
}