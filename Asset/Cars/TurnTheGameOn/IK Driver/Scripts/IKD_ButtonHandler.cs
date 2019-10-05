using UnityEngine;
using System.Collections;

namespace TurnTheGameOn.IKDriver {
	public class IKD_ButtonHandler : MonoBehaviour{

		public string Name;

		public void SetDownState(){
			IKD_CrossPlatformInputManager.SetButtonDown(Name);
		}

		public void SetUpState(){
			IKD_CrossPlatformInputManager.SetButtonUp(Name);
		}

		public void SetAxisPositiveState(){
			IKD_CrossPlatformInputManager.SetAxisPositive(Name);
		}

		public void SetAxisNeutralState(){
			IKD_CrossPlatformInputManager.SetAxisZero(Name);
		}

		public void SetAxisNegativeState(){
			IKD_CrossPlatformInputManager.SetAxisNegative(Name);
		}
	}
}