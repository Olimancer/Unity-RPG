using UnityEditor;
using UnityEngine;

namespace Game.Audio {
   [CustomEditor(typeof(AudioTrigger))]

   public class AudioTriggerEditor : Editor {

      public override void OnInspectorGUI() {
        // serializedObject.Update();

         EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Layer Filter ", EditorStyles.boldLabel, GUILayout.MaxWidth(EditorGUIUtility.labelWidth-2));
            var property = serializedObject.FindProperty("_layerFilter");
            property.intValue = EditorGUILayout.LayerField(property.intValue);
         EditorGUILayout.EndHorizontal();
         serializedObject.ApplyModifiedProperties();

         DrawDefaultInspector();
         
      }
   }
}

