using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Editor {
	[CustomEditor(typeof(ItemWatcher))]
	public class CustomItemWatcherInspector : UnityEditor.Editor {
#if UNITY_EDITOR
		public override bool RequiresConstantRepaint() {
			return true;
		}
#endif

		public override void OnInspectorGUI() {
			ItemWatcher watcher = (ItemWatcher) target;

			if (!watcher || watcher.Items.Count == 0) {
				GUILayout.Label("Enter play mode or place InteractableItems in the scene");
				return;
			}

			foreach (InteractableItem item in watcher.Items) {
				GUILayout.BeginHorizontal();
				
				GUILayout.Label(item.name);
				GUI.enabled = false;
				GUILayout.Toggle(item.InContainer, "container");
				GUILayout.Toggle(watcher.IsVisible(item), "visible");
				GUI.enabled = true;
				
				GUILayout.EndHorizontal();
			}
		}
	}
}