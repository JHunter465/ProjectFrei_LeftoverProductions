using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GuardEyes))]
public class EyeLineDrawer : Editor {
//	private void OnSceneGUI() {
//		GuardEyes eyes = target as GuardEyes;
//		if (!eyes) return;
//		
//		Transform t = eyes.transform;
//
//		Vector3 position = t.position;
//		Vector3 up = t.up;
//		Vector3 forward = t.forward;
//		
//		Handles.color = Color.red;
//		Handles.DrawLine(position, position + Quaternion.AngleAxis(eyes.FieldOfView / 2, up) * forward);
//		Handles.DrawLine(position, position + Quaternion.AngleAxis(-eyes.FieldOfView / 2, up) * forward);
//		
//		Handles.color = Color.blue;
//		Handles.DrawLine(position, position + forward);
//	}
}