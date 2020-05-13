using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Building
{
	public class WallPath
	{
		public float height;
		public float width;

		internal virtual void Draw(GameObject parent) { }
	}
}