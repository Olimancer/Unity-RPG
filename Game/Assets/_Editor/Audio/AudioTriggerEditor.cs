using UnityEditor;
using UnityEngine;

namespace Game.Audio {
   [CustomEditor(typeof(AudioTrigger))]
   public class AudioTriggerEditor : Editor {

      public override void OnInspectorGUI() {
         DrawDefaultInspector();

         serializedObject.Update();
         EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Layer Filter", "Only GameObject belonging to this layer will be able to activate the trigger"), EditorStyles.boldLabel, GUILayout.MaxWidth(EditorGUIUtility.labelWidth-3));
            var property = serializedObject.FindProperty("_layerFilter");
            property.intValue = EditorGUILayout.LayerField(property.intValue);
         EditorGUILayout.EndHorizontal();
         serializedObject.ApplyModifiedProperties(); 
      }
   }
}

