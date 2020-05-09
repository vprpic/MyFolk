using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	[System.Serializable]
	[RequireComponent(typeof(InteractableItem))]
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(NavMeshObstacle))]
	public class CarriableItem : MonoBehaviour
	{
		public enum ItemPlacementType
		{
			Surface,
			Wall,
			Both,
			None
		}

		public enum ItemSizeType
		{
			Small, //OneHand
			Large //TwoHands
		}

		[HideInInspector]
		public Interaction pickUpInteraction;
		[HideInInspector]
		public Interaction putDownInteraction;
		[HideInInspector]
		private MeshRenderer meshRenderer;
		[HideInInspector]
		private Collider _collider;
		[HideInInspector]
		private NavMeshObstacle navMeshObstacle;

		public ItemPlacementType itemPlacementType;
		public ItemSizeType itemSizeType;

		private void Start()
		{
			InteractableItem ii = gameObject.GetComponent<InteractableItem>();
			if(ii == null)
			{
				Debug.LogError("ii == null: " + this.name);
			}
			ii.ItemPlacementType = ItemPlacementType.None;
			this.meshRenderer = gameObject.GetComponent<MeshRenderer>();
			this._collider = gameObject.GetComponent<Collider>();
			this.navMeshObstacle = gameObject.GetComponent<NavMeshObstacle>();
			this.navMeshObstacle.carving = true;
			this.navMeshObstacle.carveOnlyStationary = true;

			if (ii.data != null)
			{
				this.pickUpInteraction = Globals.ins.data.pickUpInteraction;
				this.putDownInteraction = Globals.ins.data.putDownInteraction;
				if (this.pickUpInteraction != null)
				{
					ii.AddInteraction(this.pickUpInteraction);
					//Debug.Log("added interaction");
				}
				else
					Debug.Log("Pick up interaction not set");
				if (this.putDownInteraction == null)
					Debug.Log("Put down interaction not set");
			}
			else
			{
				Debug.Log("ii.data == null");
			}
		}

		public CarriableItem PickUpItem(Character character)
		{
			bool pickedUp = false;
			switch (this.itemSizeType)
			{
				case ItemSizeType.Small:
					pickedUp = character.PickUpOneFreeHand(this);
					break;
				case ItemSizeType.Large:
					pickedUp = character.PickUpItemBothHands(this);
					break;
			}
			if (pickedUp)
			{
				this.HideObject();
				return this;
			}
			return null;
		}

		public CarriableItem PutItemDown(Character character, InteractableItem iitem, RaycastHit hit)
		{
			bool possible = CanPlaceItem(iitem, hit);
			if (possible)
			{
				if (hit.normal.y <= 0.1f && hit.normal.y >= -0.1f)
					transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
				this.transform.position = hit.point;
				character.PutItemDown(this);
				this.ShowObject();
				character.RemoveInteraction(this.putDownInteraction);
				return this;
			}
			return null;
		}

		public bool CanPlaceItem(InteractableItem iitem, RaycastHit hit)
		{
			bool possible = false;
			switch (this.itemPlacementType)
			{
				case ItemPlacementType.Both:
					if ((iitem.ItemPlacementType == ItemPlacementType.Surface || iitem.ItemPlacementType == ItemPlacementType.Wall
						|| iitem.ItemPlacementType == ItemPlacementType.Both))
						possible = true;
					break;
				case ItemPlacementType.Surface:
					if ((iitem.ItemPlacementType == ItemPlacementType.Surface || iitem.ItemPlacementType == ItemPlacementType.Both) && hit.normal.y >= 0.9f)
						possible = true;
					break;
				case ItemPlacementType.Wall:
					Debug.Log(hit.normal);
					if ((iitem.ItemPlacementType == ItemPlacementType.Wall || iitem.ItemPlacementType == ItemPlacementType.Both)
						&& hit.normal.y <= 0.1f && hit.normal.y >= -0.1f)
						possible = true;
					break;
				case ItemPlacementType.None:
					possible = false;
					break;
			}
			return possible;
		}

		public bool CanBeCarriedBy(Character character)
		{
			bool can = false;
			switch (this.itemSizeType)
			{
				case ItemSizeType.Large:
					can = character.AreAllHandsFree();
					break;
				case ItemSizeType.Small:
					can = character.IsOneHandFree();
					break;
			}
			return can;
		}

		private void HideObject()
		{
			this.meshRenderer.enabled = false;
			this._collider.enabled = false;
			this.navMeshObstacle.carving = false;
		}
		private void ShowObject()
		{
			this.meshRenderer.enabled = true;
			this._collider.enabled = true;
			this.navMeshObstacle.carving = true;
		}
	}
}
