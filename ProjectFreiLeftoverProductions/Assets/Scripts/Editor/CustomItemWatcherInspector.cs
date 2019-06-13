using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Editor {
	[CustomEditor(typeof(ItemWatcher))]
	public class CustomItemWatcherInspector : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			serializedObject.Update();
			
			serializedObject.ApplyModifiedProperties();
		}
	}
}