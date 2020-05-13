using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyFolk.Building {
	/// <summary>
	/// Contains all the walls built on it
	/// </summary>
	public class BuildSurface : MonoBehaviour
	{
		public List<WallPath> wallPaths;

		public void OnBuild()
		{
			foreach (WallPath wall in wallPaths)
			{
				wall.Draw(this.gameObject);
			}
		}
	}
}