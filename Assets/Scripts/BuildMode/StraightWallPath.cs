using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Building
{
	public class StraightWallPath : WallPath
	{
		public StraightWallNode point1;
		public StraightWallNode point2;

		public GameObject point1GO;
		public GameObject point2GO;

		//public StraightWallPath(Vector3 p1, Vector3 p2)
		//{
		//	this.point1 = new StraightWallNode();
		//	//this.point2 = p2;
		//}

		internal override void Draw(GameObject parent)
		{
			base.Draw(parent);
			point1GO = new GameObject("point1");
			point1GO.transform.SetParent(parent.transform);
			//point1GO.transform.position = point1;

			point2GO = new GameObject("point2");
			point2GO.transform.SetParent(parent.transform);
			//point2GO.transform.position = point2;
		}
		internal override void GizmosDraw()
		{
			base.GizmosDraw();
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(point1.position, 0.1f);
			Gizmos.DrawSphere(point2.position, 0.1f);
			Gizmos.color = Color.green;
			Gizmos.DrawLine(point1.position, point2.position);
		}
	}
}