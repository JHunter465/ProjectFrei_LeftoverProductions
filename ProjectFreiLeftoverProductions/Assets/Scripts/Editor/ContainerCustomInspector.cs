using UnityEditor;
using UnityEngine;

namespace Editor {
	[CustomEditor(typeof(Container))]
	public class ContainerCustomInspector : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			serializedObject.Update();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("lm"), new GUIContent("Linear Mapping"));

			SerializedProperty bcProp = serializedObject.FindProperty("bc");
			EditorGUILayout.PropertyField(bcProp, new GUIContent("Check Mode"));

			EditorGUI.indentLevel++;
		
			if (bcProp.enumValueIndex == (int) Container.BooleanCheck.Equal) {
				EditorGUILayout.PropertyField(serializedObject.FindProperty("value"));
			}
			else {
				if (bcProp.enumValueIndex == (int) Container.BooleanCheck.LargerThan || 
				    bcProp.enumValueIndex == (int) Container.BooleanCheck.SmallerThan) {
					EditorGUILayout.PropertyField(serializedObject.FindProperty("value"));
				}
				else if (bcProp.enumValueIndex == (int) Container.BooleanCheck.InRange || 
				         bcProp.enumValueIndex == (int) Container.BooleanCheck.OutRange) {
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.PropertyField(serializedObject.FindProperty("rangeMin"), new GUIContent("Min"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty("rangeMax"), new GUIContent("Max"));
					EditorGUILayout.EndHorizontal();
				}
			
				EditorGUILayout.PropertyField(serializedObject.FindProperty("inclusive"));
			}

			EditorGUI.indentLevel--;
		
			serializedObject.ApplyModifiedProperties();
		}
	}
}