using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor (typeof(ERTutorial))]
public class ERTutorialEditor : Editor  {

	public ERTutorial scr;
	

	public void OnEnable() 
	{
		scr = (ERTutorial)target;
	}

	public override void OnInspectorGUI() 
	{

		GUILayout.Space(25);

		GUILayout.Label("EasyRoads3D Tutorials: ");

		for(int i = 0; i < scr.links.Length; i++){
			if(GUILayout.Button (scr.linkText[i])){
			//	Debug.Log (scr.links[i]);
				Application.OpenURL (scr.links[i]);
			}
		}


		GUILayout.Space(50);

	}

	public  void OnSceneGUI() 
	{

	}

}
