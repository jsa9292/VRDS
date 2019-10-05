using UnityEngine;
using System.Collections;

namespace TurnTheGameOn.IKDriver{
	public static class IKD_StaticUtility {
				
		#region Main Methods	
		//IKD_UtilitySettings ScriptablObject that contains general settings
		private static IKD_UtilitySettings IKD_UtilitySettings;
		public static IKD_UtilitySettings m_IKD_UtilitySettings{
			get{ 
				if (IKD_UtilitySettings == null)
					IKD_UtilitySettings = Resources.Load ("IKD_UtilitySettings") as IKD_UtilitySettings;
				return IKD_UtilitySettings;
			}
		}
		#endregion
		
	}
}