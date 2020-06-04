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
		private Vector3 toolTipPosition;
		private float range = 500f;
		private float nodeNWallRange = 2f;

		private BuildSurface buildSurface;
		private LayerMask buildSurfaceMask;
		private LayerMask wallMask;

		#region straight wall build
		public GameObject toolTipPreview;
		public MeshCubeBuilder meshCubeBuilder;
		private Vector3 point1;
		private Vector3 point2;
		private bool isSnappingPoint1;
		private bool isSnappingPoint2;
		private WallPath snappedToWall;
		private WallPath snappedToWallPoint1;
		private WallPath snappedToWallPoint2;
		private WallNode snappedToNode;
		private WallNode snappedToNodePoint1;
		private WallNode snappedToNodePoint2;

		private StraightWallPath ghostWall;
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

			buildSurfaceMask = LayerMask.GetMask("BuildSurface");
			wallMask = LayerMask.GetMask("Wall");

			ghostWall = GameObject.Instantiate(Globals.ins.data.straightWallPathPrefab);
			ghostWall.point1 = new StraightWallNode();
			ghostWall.point2 = new StraightWallNode();
			ghostWall.gameObject.SetActive(false);
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

		private void CheckForBuildSurfaceHit()
		{
			buildSurface = null;
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			isHit = Physics.Raycast(ray, out whatIHit, range, buildSurfaceMask);
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
					toolTipPosition = whatIHit.point;

					//wallIsHit = Physics.sp (ray, out whatIHit, range, buildSurfaceMask);
					Collider[] hitWalls = Physics.OverlapSphere(toolTipPosition, nodeNWallRange, wallMask);
					Vector3 pointInWall;
					//Debug.Log("hitwalls: " + hitWalls.Length);
					if (hitWalls.Length != 0 && IsInRangeOfWalls(hitWalls, whatIHit.point, out snappedToWall, out pointInWall, out snappedToNode))
					{
						toolTipPosition = pointInWall;
					}
					else
					{
						snappedToWall = null;
						snappedToNode = null;
					}
					if (Input.GetMouseButtonDown(0))
					{
						//Debug.Log("clicked on build surface");
						//buildSurface.wallPaths.Add();
						//buildSurface.OnBuild();
						//GameObject go = new GameObject("point1");
						//go.transform.SetParent(buildSurface.gameObject.transform);
						//go.transform.position = whatIHit.point;
						point1 = toolTipPosition;
						ghostWall.point1.position = point1;
						ghostWall.transform.position = toolTipPosition;
						ghostWall.ClearColliders();
						ghostWall.gameObject.SetActive(true);
						if (snappedToWall != null)
						{
							isSnappingPoint1 = true;
							snappedToWallPoint1 = snappedToWall;
							if (snappedToNode != null)
								snappedToNodePoint1 = snappedToNode;
							else
								snappedToNodePoint1 = null;
						}
						else
						{
							isSnappingPoint1 = false;
							snappedToWallPoint1 = null;
							snappedToNodePoint1 = null;
						}
					}
					if (Input.GetMouseButton(0))
					{
						ghostWall.point2.position = toolTipPosition;
						this.meshCubeBuilder.Generate(ref ghostWall.meshFilter, ghostWall.width, ghostWall.height, Vector3.Distance(ghostWall.point1.position, ghostWall.point2.position));
						ghostWall.transform.LookAt(ghostWall.point2.position);
						ghostWall.boxCollider.size = new Vector3(ghostWall.width, ghostWall.height, Vector3.Distance(ghostWall.point1.position, ghostWall.point2.position));
						ghostWall.boxCollider.center = new Vector3(0, ghostWall.boxCollider.size.y * 0.5f, ghostWall.boxCollider.size.z * 0.5f);

						if (ghostWall.GetColliders().Count > 0 && !isSnappingPoint1)
						{
							ghostWall.meshRenderer.material = Globals.ins.data.invalidGhostWallMaterial;
						}
						else
						{
							ghostWall.meshRenderer.material = Globals.ins.data.ghostWallMaterial;
						}
					}
					if (Input.GetMouseButtonUp(0))
					{
						ghostWall.gameObject.SetActive(false);

						point2 = toolTipPosition;
						if (snappedToWall != null)
						{
							isSnappingPoint2 = true;
							snappedToWallPoint2 = snappedToWall;
							if (snappedToNode != null)
								snappedToNodePoint2 = snappedToNode;
							else
								snappedToNodePoint2 = null;
						}
						else
						{
							isSnappingPoint2 = false;
							snappedToWallPoint2 = null;
							snappedToNodePoint2 = null;
						}

						if (ghostWall.GetColliders().Count > 0 && !isSnappingPoint1 && !isSnappingPoint2)
						{
							//reset current settings
							snappedToWall = null;
							snappedToWallPoint1 = null;
							snappedToWallPoint2 = null;
							snappedToNode = null;
							snappedToNodePoint1 = null;
							snappedToNodePoint2 = null;
							isSnappingPoint1 = isSnappingPoint2 = false;
							ghostWall.ClearColliders();
						}
						else
						{

							StraightWallNode node1 = null, node2 = null;

							//Wall setup
							if (snappedToWallPoint1 != null && snappedToWallPoint1 is StraightWallPath && snappedToNodePoint1 == null)
							{
								SplitTheStraightWall((StraightWallPath)snappedToWallPoint1, point1, out node1);
							}
							if (snappedToWallPoint2 != null && snappedToWallPoint2 is StraightWallPath && snappedToNodePoint2 == null)
							{
								SplitTheStraightWall((StraightWallPath)snappedToWallPoint2, point2, out node2);
							}


							//Node setup
							if (node1 != null)
							{
								if (snappedToNodePoint1 != null && snappedToNodePoint1 is StraightWallNode)
								{
									node1 = (StraightWallNode)snappedToNodePoint1;
								}
								else
								{
									node1 = new StraightWallNode();
									buildSurface.AddNode(node1);
								}
							}
							else
							{
								node1 = new StraightWallNode();
								buildSurface.AddNode(node1);
							}
							if (node2 != null)
							{
								if (snappedToNodePoint2 != null && snappedToNodePoint2 is StraightWallNode)
								{
									node2 = (StraightWallNode)snappedToNodePoint2;
								}
								else
								{
									node2 = new StraightWallNode();
									buildSurface.AddNode(node2);
								}
							}
							else
							{
								node2 = new StraightWallNode();
								buildSurface.AddNode(node2);
							}

							StraightWallPath wall = GameObject.Instantiate(Globals.ins.data.straightWallPathPrefab, buildSurface.transform);
							node1.position = point1;
							node2.position = point2;

							wall.point1 = node1;
							wall.point2 = node2;

							node1.wallsConnectedToThis.Add(wall);
							node2.wallsConnectedToThis.Add(wall);

							wall.transform.position = node1.position;

							wall.transform.LookAt(node2.position);
							this.meshCubeBuilder.Generate(ref wall.meshFilter, wall.width, wall.height, Vector3.Distance(node1.position, node2.position));
							wall.meshRenderer.material = Globals.ins.data.builtWallMaterial;
							wall.boxCollider.size = new Vector3(wall.width, wall.height, Vector3.Distance(wall.point1.position, wall.point2.position));
							wall.boxCollider.center = new Vector3(0, wall.boxCollider.size.y * 0.5f, wall.boxCollider.size.z * 0.5f);
							buildSurface.AddWall(wall);
						}
					}
				}
			}
			else
			{
				toolTipPreview.SetActive(false);
			}
		}

		private void SplitTheStraightWall(StraightWallPath splittingWall, Vector3 point, out StraightWallNode node)
		{
			node = new StraightWallNode();
			node.position = point;

			//second wall
			StraightWallPath secondWall = GameObject.Instantiate(Globals.ins.data.straightWallPathPrefab, buildSurface.transform);
			secondWall.point1 = node;
			secondWall.point2 = splittingWall.point2;
			secondWall.height = splittingWall.height;
			secondWall.width = splittingWall.width;

			node.wallsConnectedToThis.Add(secondWall);
			splittingWall.point2.wallsConnectedToThis.Add(secondWall);

			secondWall.transform.position = node.position;
			secondWall.transform.LookAt(secondWall.point2.position);

			this.meshCubeBuilder.Generate(ref secondWall.meshFilter, secondWall.width, secondWall.height, Vector3.Distance(secondWall.point1.position, secondWall.point2.position));
			secondWall.boxCollider.size = new Vector3(secondWall.width, secondWall.height, Vector3.Distance(secondWall.point1.position, secondWall.point2.position));
			secondWall.boxCollider.center = new Vector3(0, secondWall.boxCollider.size.y * 0.5f, secondWall.boxCollider.size.z * 0.5f);
			secondWall.meshRenderer.material = splittingWall.meshRenderer.material;
			buildSurface.AddWall(secondWall);

			secondWall.point2.wallsConnectedToThis.Remove(splittingWall);

			//first wall
			splittingWall.point2 = secondWall.point1;
			splittingWall.point2.wallsConnectedToThis.Add(splittingWall);
			splittingWall.transform.LookAt(splittingWall.point2.position);

			this.meshCubeBuilder.Generate(ref splittingWall.meshFilter, splittingWall.width, splittingWall.height, Vector3.Distance(splittingWall.point1.position, splittingWall.point2.position));
			splittingWall.boxCollider.size = new Vector3(splittingWall.width, splittingWall.height, Vector3.Distance(splittingWall.point1.position, splittingWall.point2.position));
			splittingWall.boxCollider.center = new Vector3(0, splittingWall.boxCollider.size.y * 0.5f, splittingWall.boxCollider.size.z * 0.5f);
		}


		internal void FixedUpdate()
		{
			if (buildSurface != null)
			{
				toolTipPreview.SetActive(true);
				toolTipPreview.transform.position = toolTipPosition;
			}
			else
			{
				toolTipPreview.SetActive(true);
			}
		}

		private bool IsInRangeOfWalls(Collider[] colliders, Vector3 point, out WallPath snappedToWall, out Vector3 pointInWall, out WallNode snappedToNode)
		{
			foreach (Collider c in colliders)
			{
				snappedToWall = c.gameObject.GetComponent<WallPath>();
				if (snappedToWall.IsInRange(point, out pointInWall, out snappedToNode))
				{
					return true;
				}
			}
			snappedToNode = null;
			pointInWall = Vector3.zero;
			snappedToWall = null;
			return false;
		}

		//public Vector3 NearestPointOnFiniteLine(Vector3 start, Vector3 end, Vector3 pnt)
		//{
		//	var line = (end - start);
		//	var len = line.magnitude;
		//	line.Normalize();

		//	var v = pnt - start;
		//	var d = Vector3.Dot(v, line);
		//	d = Mathf.Clamp(d, 0f, len);
		//	return start + line * d;
		//}

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