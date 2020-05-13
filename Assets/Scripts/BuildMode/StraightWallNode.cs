using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Building
{
	[System.Serializable]
	public class StraightWallNode : WallNode
	{
		[SerializeField]
		public StraightWallNode node1;
		[SerializeField]
		public StraightWallNode node2;


	}
}