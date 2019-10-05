using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TurnTheGameOn.IKDriver{
	public class IKD_MakeScriptableObject : MonoBehaviour {
				
		#region Main Methods	
		[MenuItem("Assets/Create/IKD_UtilitySettings")]
		public static void CreatePlayableVehicles(){
			IKD_UtilitySettings asset = ScriptableObject.CreateInstance<IKD_UtilitySettings>();
			AssetDatabase.CreateAsset (asset, "Assets/IKD_UtilitySettings.asset");
			AssetDatabase.SaveAssets ();
			EditorUtility.FocusProjectWindow ();
			Selection.activeObject = asset;
		}
		#endregion
		
	}
}