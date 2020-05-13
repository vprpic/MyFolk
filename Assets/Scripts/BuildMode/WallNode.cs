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