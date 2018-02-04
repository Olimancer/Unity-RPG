#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.IO;
using System.Text;

namespace Game.Utils.EnumNS {
	public class LayerTagEnumGenerator : EditorWindow {
		[MenuItem("Edit/Rebuild Tags And Layers Enums")]
		static void RebuildTagsAndLayersEnums() {
			var enumsPath = Application.dataPath + "/_Utils/Enums/";

			RebuildTagsFile(enumsPath + "Tags.cs");

			AssetDatabase.ImportAsset(enumsPath + "Tags.cs", ImportAssetOptions.ForceUpdate);
			AssetDatabase.ImportAsset(enumsPath + "Layers.cs", ImportAssetOptions.ForceUpdate);
		}

		static void RebuildTagsFile(string filePath) {
			StringBuilder sb = new StringBuilder();

			sb.Append("// This class is auto-generated, do not modify (TagsLayersEnumBuilder.cs)\n");
			sb.Append("public abstract class Tags {\n");

			var srcArr = UnityEditorInternal.InternalEditorUtility.tags;
			var tags = new String[srcArr.Length];
			Array.Copy(srcArr, tags, tags.Length);
			Array.Sort(tags, StringComparer.InvariantCultureIgnoreCase);

			for (int i = 0, n = tags.Length; i < n; ++i) {
				string tagName = tags[i];

				sb.Append("\tpublic const string " + tagName + " = \"" + tagName + "\";\n");
			}

			sb.Append("}\n");

			#if !UNITY_WEBPLAYER
				File.WriteAllText(filePath, sb.ToString());
			#endif
		}
	}
}
#endif
