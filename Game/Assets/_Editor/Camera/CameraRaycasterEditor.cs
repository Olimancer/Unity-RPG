using UnityEditor;

// TODO: Consider changing to a property drawer

namespace Game.Cameras {
   [CustomEditor(typeof(CameraRaycaster))]

   public class CameraRaycasterEditor : Editor {
      bool _layerPrioritiesUnfolded = true; 

      // Certifies that both the required array size and current array size are identical
      void BindArraySize() {
         int currentArraySize = serializedObject.FindProperty("_layerPriorities.Array.size").intValue;
         int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize);
         if (requiredArraySize != currentArraySize) {
            serializedObject.FindProperty("_layerPriorities.Array.size").intValue = requiredArraySize;
         }
      }

      // Update the editor to show layer as names
      void BindArrayElements() {
         int currentArraySize = serializedObject.FindProperty("_layerPriorities.Array.size").intValue;
         for (int i = 0; i < currentArraySize; i++) {
            var property = serializedObject.FindProperty(string.Format("_layerPriorities.Array.data[{0}]", i));
            property.intValue = EditorGUILayout.LayerField(string.Format("Layer {0}:", i), property.intValue);
         }
      }

      public override void OnInspectorGUI() {
         serializedObject.Update(); // Serialize instance of CameraRaycaster

         _layerPrioritiesUnfolded = EditorGUILayout.Foldout(_layerPrioritiesUnfolded, "Layer Priorities");

         if (_layerPrioritiesUnfolded) {
            EditorGUI.indentLevel++; 
            BindArraySize();
            BindArrayElements();
            EditorGUI.indentLevel--;
         }

         serializedObject.ApplyModifiedProperties();
      }
   }
}

