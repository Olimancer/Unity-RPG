#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

// Control menu for the Generators.
// Will add menus in the "Edit" 
namespace Game.Utils.EnumNS {
	public class EnumBuilderCaller : EditorWindow {
		// Add a reference to your EnumFileBuilder script in this list
		static EnumFileBuilder[] _buildersList = {
			ScriptableObject.CreateInstance<LayerEnumBuilder>(),
		};


		[MenuItem("Edit/Generate Enums/All")]
		static void GenerateEnums() {
			foreach (EnumFileBuilder builder in _buildersList) {
				builder.GenerateEnum();
			}
			Debug.Log("Generation completed");
		}

		[MenuItem("Edit/Generate Enums/Layer")]
		static void GenerateLayer() {
			_buildersList[0].GenerateEnum();
		}
	}
}
#endif
