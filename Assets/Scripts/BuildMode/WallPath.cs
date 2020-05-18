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
	}
}