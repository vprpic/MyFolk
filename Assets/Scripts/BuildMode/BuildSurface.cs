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

		public void AddWall(WallPath wall)
		{
			wallPaths.Add(wall);
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
	}
}