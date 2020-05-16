using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpdateSkinnedMeshRendererColliders))]
public class UpdateSkinnedMeshRendererCollidersEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Set Meshes for All Renderers"))
		{
			UpdateSkinnedMeshRendererColliders.UpdateAllSkinnedMeshRendererColliders();
		}
	}
}
