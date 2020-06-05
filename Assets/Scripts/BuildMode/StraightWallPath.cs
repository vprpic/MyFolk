using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace MyFolk.Building
{
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class StraightWallPath : WallPath
	{
		//public MeshFilter meshFilter;
		public ProBuilderMesh proBuilderMesh;
		public MeshRenderer meshRenderer;
		public BoxCollider boxCollider;

		public StraightWallNode point1;
		public StraightWallNode point2;

		public GameObject point1GO;
		public GameObject point2GO;


		private void Awake()
		{
			this.proBuilderMesh = GetComponent<ProBuilderMesh>();
			this.meshRenderer = GetComponent<MeshRenderer>();
			this.boxCollider = GetComponent<BoxCollider>();
		}

		internal override void Draw(GameObject parent)
		{
			base.Draw(parent);
			point1GO = new GameObject("point1");
			point1GO.transform.SetParent(parent.transform);

			point2GO = new GameObject("point2");
			point2GO.transform.SetParent(parent.transform);
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

		internal override bool IsInRange(Vector3 point, out Vector3 nearestPoint, out WallNode snappedToNode)
		{
			snappedToNode = null;
			nearestPoint = Vector3.zero;
			if (this.point1 == null || this.point2 == null)
				return false;
			nearestPoint = NearestPointOnFiniteLine(point1.position, point2.position, point);
			if (Vector3.Distance(nearestPoint, point) < this.width * 0.5f)
			{
				if (Vector3.Distance(nearestPoint, point1.position) < this.width * 0.5f)
				{
					snappedToNode = point1;
					nearestPoint = point1.position;
				}
				else if (Vector3.Distance(nearestPoint, point2.position) < this.width * 0.5f)
				{
					snappedToNode = point2;
					nearestPoint = point2.position;
				}
				return true;
			}
			return false;
		}

		internal override bool IsInRange(Vector3 point)
		{
			Vector3 nearestPoint = NearestPointOnFiniteLine(point1.position, point2.position, point);
			if (Vector3.Distance(nearestPoint, point) < this.width * 0.5f)
			{
				if (Vector3.Distance(nearestPoint, point1.position) < this.width * 0.5f)
				{
					nearestPoint = point1.position;
				}
				else if (Vector3.Distance(nearestPoint, point2.position) < this.width * 0.5f)
				{
					nearestPoint = point2.position;
				}
				return true;
			}
			return false;
		}


		private List<Collider> colliders = new List<Collider>();
		public void ClearColliders()
		{
			colliders.Clear();
		}
		public List<Collider> GetColliders() { return colliders; }

		private void OnTriggerEnter(Collider other)
		{
			if (!colliders.Contains(other) && other.gameObject.layer != LayerMask.NameToLayer("BuildSurface"))
			{
				StraightWallPath wall = other.GetComponent<StraightWallPath>();
				if(wall!= null && (wall.point1 == null || wall.point2 == null))
				{
					Debug.LogError("sdfsdf");
				}
				if (!(wall != null && (IsInRange(wall.point1.position) || IsInRange(wall.point2.position))))
				{
					
					colliders.Add(other);
					Debug.Log("add collider");
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			colliders.Remove(other);
			Debug.Log("remove collider");
		}
	}
}