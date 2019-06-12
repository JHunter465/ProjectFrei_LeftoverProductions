using UnityEditor;

namespace Editor {
	[CustomEditor(typeof(LevelManager))]
	public class CustomLevelManagerInspector : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			serializedObject.Update();
			
			serializedObject.ApplyModifiedProperties();
		}
	}
}