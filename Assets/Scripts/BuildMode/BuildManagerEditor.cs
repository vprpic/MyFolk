using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace MyFolk.Building
{
	[CustomEditor(typeof(BuildSurface))]
	public class BuildManagerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			BuildSurface bs = target as BuildSurface;
			if (GUILayout.Button("Merge Wall Meshes"))
			{
				bs.MergeMeshes();
			}
		}

	}
}
