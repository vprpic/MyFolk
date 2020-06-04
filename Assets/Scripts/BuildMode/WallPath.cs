using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Building
{
	[System.Serializable]
	public class WallPath : MonoBehaviour
	{
		public float height;
		public float width;

		internal virtual void Draw(GameObject parent) { }

		internal virtual void GizmosDraw() { }

		internal virtual bool IsInRange(Vector3 point)
		{
			return false;
		}

		internal virtual bool IsInRange(Vector3 point, out Vector3 nearestPoint, out WallNode snappedToNode)
		{
			snappedToNode = null;
			nearestPoint = Vector3.zero;
			return false;
		}

		public static Vector3 NearestPointOnFiniteLine(Vector3 start, Vector3 end, Vector3 pnt)
		{
			var line = (end - start);
			var len = line.magnitude;
			line.Normalize();

			var v = pnt - start;
			var d = Vector3.Dot(v, line);
			d = Mathf.Clamp(d, 0f, len);
			return start + line * d;
		}

	}
}