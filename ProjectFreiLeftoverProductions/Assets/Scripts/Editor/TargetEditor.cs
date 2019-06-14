using UnityEditor;
using UnityEngine;

namespace editor {
	[CustomEditor(typeof(BorderControl))]
	public class TargetEditor : UnityEditor.Editor {
		private void OnSceneGUI() {
			Target t = target as Target;

			if (t) {
				Handles.color = Color.red;

				Vector3 pos = t.transform.position;
				Handles.DrawLine(pos, pos - Vector3.up * pos.y);
			}
		}
	}
}