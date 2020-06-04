using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Building
{
	[System.Serializable]
	public class WallNode
	{
		public List<WallPath> wallsConnectedToThis;
		public Vector3 position;

		public WallNode()
		{
			wallsConnectedToThis = new List<WallPath>();
		}

		internal bool IsInRange(Vector3 point)
		{
			foreach (WallPath wall in wallsConnectedToThis)
			{
				float dist = Vector3.Distance(point, position);
				if (dist < wall.width * 0.5f)
					return true;
			}
			return false;
		}
	}

	public class StraightWallNode : WallNode
	{
		//[SerializeField]
		//public StraightWallNode node1;
		//[SerializeField]
		//public StraightWallNode node2;

		public StraightWallNode()
		{
			wallsConnectedToThis = new List<WallPath>();
		}

	}
}