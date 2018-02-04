#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text;

namespace Game.Utils.EnumNS {
	public class LayerEnumBuilder : EnumFileBuilder {
		// Build the enum base on the layer settings of the project
		void LayerBuilder(StringBuilder stringBuilder) {
			stringBuilder.Append("\t// Generated with LayerEnumBuilder.cs\n");
			var layers = UnityEditorInternal.InternalEditorUtility.layers;

			for (int i = 0; i < layers.Length; i++) {
				stringBuilder.Append("\t" + GetVariableName(layers[i]) + " = " + LayerMask.NameToLayer(layers[i]) + ",\n");
			}
		}
		
		override public void GenerateEnum() {
			BuildFile("Layer", LayerBuilder);
		}
	}
}
#endif
