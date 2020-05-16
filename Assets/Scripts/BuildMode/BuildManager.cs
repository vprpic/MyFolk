using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Building
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

	public class BuildManager
	{

		public BuildTool currentBuildTool;
		private Camera mainCamera;
		private bool isHit;
		private RaycastHit whatIHit;
		private float range = 500f;

		private BuildSurface buildSurface;

		#region straight wall build
		public GameObject toolTipPreview;
		private RaycastHit point1;
		private RaycastHit point2;
		#endregion straight wall build


		internal void Init()
		{
			mainCamera = Camera.main;
			currentBuildTool = BuildTool.None;
			toolTipPreview = GameObject.Instantiate(Globals.ins.data.buildTooltipPreviewPrefab);
			toolTipPreview.SetActive(false);
			EventCallbacks.SetBuildToolEvent.RegisterListener(OnBuildToolSet);
		}

		public void Update()
		{
			switch (currentBuildTool)
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

		private void HandleClick()
		{
			
		}

		private void CheckForBuildSurfaceHit()
		{
			buildSurface = null;
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			isHit = Physics.Raycast(ray, out whatIHit, range);
			if (isHit)
			{
				//currentEventInfo.screenClickPoint = Input.mousePosition;
				//currentEventInfo.hit = whatIHit;
				buildSurface = whatIHit.collider.GetComponent<BuildSurface>();

				if (buildSurface != null)
				{
					//if(!toolTipPreview.activeSelf)
					//toolTipPreview.SetActive(true);
					//toolTipPreview.transform.position = whatIHit.point;
					if (Input.GetMouseButtonDown(0))
					{
						Debug.Log("clicked on build surface");
						//buildSurface.wallPaths.Add();
						//buildSurface.OnBuild();
						//GameObject go = new GameObject("point1");
						//go.transform.SetParent(buildSurface.gameObject.transform);
						//go.transform.position = whatIHit.point;
						point1 = whatIHit;
					}
					if (Input.GetMouseButton(0))
					{
					}
					if (Input.GetMouseButtonUp(0))
					{
						//GameObject go = new GameObject("point2");
						//go.transform.SetParent(buildSurface.gameObject.transform);
						//go.transform.position = whatIHit.point;
						point2 = whatIHit;

						StraightWallNode node1 = new StraightWallNode();
						StraightWallNode node2 = new StraightWallNode();

						StraightWallPath wall = new StraightWallPath();
						node1.position = point1.point;
						node2.position = point2.point;

						wall.point1 = node1;
						wall.point2 = node2;

						node1.wallsConnectedToThis.Add(wall);
						node2.wallsConnectedToThis.Add(wall);

						buildSurface.AddWall(wall);
					}
				}
			}
			//else
			//{
			//	toolTipPreview.SetActive(false);
			//}
		}

		internal void FixedUpdate()
		{
			if (buildSurface != null)
			{
				toolTipPreview.SetActive(true);
				toolTipPreview.transform.position = whatIHit.point;
			}
			else
			{
				toolTipPreview.SetActive(true);
			}
		}

		#region events
		public void OnBuildToolSet(EventCallbacks.SetBuildToolEvent eventInfo)
		{
			if (this.currentBuildTool.Equals(eventInfo.newBuildTool))
				return;
			this.currentBuildTool = eventInfo.newBuildTool;
		}
		#endregion events

	}
}