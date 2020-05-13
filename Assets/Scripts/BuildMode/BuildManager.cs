using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Building
{
	public class BuildManager
	{
		public enum BuildTool
		{
			None,
			StraightWall,
			CurvedWall,
			Floor,
			Window,
			Door,
		}

		public BuildTool selectedBuildTool;
		private Camera mainCamera;
		private bool isHit;
		private RaycastHit whatIHit;
		private float range = 500f;

		#region straight wall

		#endregion straight wall

		internal void Init()
		{
			mainCamera = Camera.main;
			selectedBuildTool = BuildTool.None;
		}

		public void Update()
		{
			switch (selectedBuildTool)
			{
				case BuildTool.None:
					break;
				case BuildTool.StraightWall:
					CheckForBuildSurfaceHit();
					break;
				case BuildTool.CurvedWall:
					break;
				case BuildTool.Floor:
					break;
				case BuildTool.Window:
					break;
				case BuildTool.Door:
					break;
			}
		}

		private void CheckForBuildSurfaceHit()
		{
			BuildSurface buildSurface = null;
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			isHit = Physics.Raycast(ray, out whatIHit, range);
			if (isHit)
			{
				//currentEventInfo.screenClickPoint = Input.mousePosition;
				//currentEventInfo.hit = whatIHit;
				buildSurface = whatIHit.collider.GetComponent<BuildSurface>();

				if (buildSurface != null)
				{
					//if (whatIHit.distance <= buildSurface.RaycastRange)
					//{
					//	if (buildSurface == currentTargetIItem)
					//	{
					//		return;
					//	}
					//	else if (currentTargetIItem != null)
					//	{
					//		currentTargetIItem.OnEndHover();
					//		currentTargetIItem = buildSurface;
					//		currentTargetIItem.OnStartHover();
					//		return;
					//	}
					//	else
					//	{
					//		currentTargetIItem = buildSurface;
					//		currentTargetIItem.OnStartHover();
					//		return;
					//	}
					//}
					//else
					//{
					//	if (currentTargetIItem != null)
					//	{
					//		currentTargetIItem.OnEndHover();
					//		currentTargetIItem = null;
					//		return;
					//	}
					//}
					buildSurface.OnBuild();
				}
				else
				{
					//if (currentTargetIItem != null)
					//{
					//	currentTargetIItem.OnEndHover();
					//	currentTargetIItem = null;
					//	return;
					//}
				}
			}
			else
			{
				//if (currentTargetIItem != null)
				//{
				//	currentTargetIItem.OnEndHover();
				//	currentTargetIItem = null;
				//	return;
				//}
			}
		}

	}
}