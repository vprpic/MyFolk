using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyFolk.Building {
	/// <summary>
	/// Contains all the walls built on it
	/// </summary>
	[System.Serializable]
	public class BuildSurface : MonoBehaviour
	{
		public List<WallPath> wallPaths = new List<WallPath>();
		public List<WallNode> wallNodes = new List<WallNode>();
		public Mesh planeMesh;
		public MeshFilter myMeshFilter;
		public Mesh mesh;
		private void Awake()
		{
			myMeshFilter = GetComponent<MeshFilter>();
			planeMesh = myMeshFilter.mesh;
		}
		public void AddWall(WallPath wall)
		{
			if(!wallPaths.Contains(wall))
				wallPaths.Add(wall);
		} 

		public void AddNode(WallNode node)
		{
			if (!wallNodes.Contains(node))
				wallNodes.Add(node);
		}

		public void OnBuild()
		{
			foreach (WallPath wall in wallPaths)
			{
				wall.Draw(this.gameObject);
			}
		}
		private void OnDrawGizmos()
		{
			foreach (WallPath wall in wallPaths)
			{
				wall.GizmosDraw();
			}
		}

		public void MergeMeshes()
		{
			mesh = myMeshFilter.sharedMesh;
			if(mesh == null)
			{
				mesh = new Mesh();
				myMeshFilter.sharedMesh = mesh;
			}
			else
			{
				mesh.Clear();
			}

			MeshFilter[] filters = GetComponentsInChildren<MeshFilter>(false);
			Debug.Log("Merging " + (filters.Length - 1) + " meshes");

			List<CombineInstance> combiners = new List<CombineInstance>();

			foreach (MeshFilter filter in filters)
			{
				if (filter == myMeshFilter)
					continue;
				CombineInstance ci = new CombineInstance();
				ci.mesh = filter.sharedMesh;
				ci.subMeshIndex = 0;
				ci.transform = Matrix4x4.identity;
				combiners.Add(ci);
			}
			mesh.CombineMeshes(combiners.ToArray(), true);
		}
	}
}