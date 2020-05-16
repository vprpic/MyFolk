using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateSkinnedMeshRendererColliders : MonoBehaviour
{
	public static void UpdateAllSkinnedMeshRendererColliders()
	{
		List<SkinnedMeshRenderer> smrs = FindObjectsOfType<SkinnedMeshRenderer>().ToList();

		foreach (SkinnedMeshRenderer renderer in smrs)
		{
			MeshCollider collider = renderer.gameObject.GetComponent<MeshCollider>();
			if(collider == null)
			{
				Debug.Log("No Mesh Collider!");
				continue;
			}
			if(collider.sharedMesh == null)
			{
				collider.sharedMesh = new Mesh();
			}
			collider.sharedMesh.Clear();
			Mesh colliderMesh = collider.sharedMesh;
			renderer.BakeMesh(colliderMesh);
			collider.sharedMesh = colliderMesh;
		}
	}
}
