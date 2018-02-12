#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

// Control menu for the Generators.
// Will add menus in the custom "Tools" menu
namespace Game.Utils.Enums {
   public class EnumBuilderCaller : EditorWindow {
      // Add a reference to your EnumFileBuilder script in this list
      static EnumFileBuilder[] _buildersList = {
         ScriptableObject.CreateInstance<LayerEnumBuilder>(),
      };


      [MenuItem("Tools/Generate Enums/All")]
      static void GenerateEnums() {
         foreach (EnumFileBuilder builder in _buildersList) {
            builder.GenerateEnum();
         }
         Debug.Log("Generation completed");
      }

      [MenuItem("Tools/Generate Enums/Layer")]
      static void GenerateLayer() {
         _buildersList[0].GenerateEnum();
      }
   }
}
#endif
