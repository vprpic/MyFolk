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
		public MeshCubeBuilder meshCubeBuilder;
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
			this.meshCubeBuilder = new MeshCubeBuilder();
			this.meshCubeBuilder.xSize = this.meshCubeBuilder.ySize = 2;
			this.meshCubeBuilder.zSize = 3;
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

						StraightWallPath wall = GameObject.Instantiate(Globals.ins.straightWallPathPrefab, buildSurface.transform);
						node1.position = point1.point;
						node2.position = point2.point;

						wall.point1 = node1;
						wall.point2 = node2;

						node1.wallsConnectedToThis.Add(wall);
						node2.wallsConnectedToThis.Add(wall);

						wall.transform.position = node1.position;
						//int temp = (int)Vector3.Distance(node1.position, node2.position);
						//if(temp < 2)
						//	this.meshCubeBuilder.xSize = 2;
						//else
						//	this.meshCubeBuilder.xSize = temp;

						//temp = (int)wall.height;
						//if (temp < 2)
						//	this.meshCubeBuilder.ySize = 2;
						//else
						//	this.meshCubeBuilder.ySize = temp;


						//temp = (int)wall.width;

						//if (temp < 2)
						//	this.meshCubeBuilder.zSize = 2;
						//else
						//	this.meshCubeBuilder.zSize = temp;

						this.meshCubeBuilder.Generate(ref wall.meshFilter, wall.width, wall.height, Vector3.Distance(node1.position, node2.position));
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