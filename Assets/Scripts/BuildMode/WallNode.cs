using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Building
{
	public class WallNode
	{
		public List<WallPath> wallsConnectedToThis;
		public Vector3 position;
	}
}