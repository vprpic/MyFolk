using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Building
{
	public class StraightWallPath : WallPath
	{
		public Vector3 point1 = new Vector3(1,1,1);
		public Vector3 point2 = new Vector3(1,1,2);

		public GameObject point1GO;
		public GameObject point2GO;

		internal override void Draw(GameObject parent)
		{
			base.Draw(parent);
			point1GO = new GameObject("point1");
			point1GO.transform.SetParent(parent.transform);
			point1GO.transform.position = point1;

			point2GO = new GameObject("point2");
			point2GO.transform.SetParent(parent.transform);
			point2GO.transform.position = point2;
		}
	}
}