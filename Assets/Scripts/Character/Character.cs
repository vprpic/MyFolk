using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace MyFolk
{
	[System.Serializable]
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(LookAt))]
	[RequireComponent(typeof(LocomotionSimpleAgent))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class Character : MonoBehaviour
	{
		public CharacterData data;
		public NavMeshAgent agent;
		public InteractionQueue interactionQueue;
		public LocomotionSimpleAgent motion;

		public CarriableItem leftHand;
		public CarriableItem rightHand;
		public CarriableItem bothHands;
		public bool isSelected => data.isSelected;

		public List<Interaction> tempCharacterInteractions;

		private void Awake()
		{
			agent = GetComponent<NavMeshAgent>();
			motion = GetComponent<LocomotionSimpleAgent>();
			interactionQueue = new InteractionQueue(this);
			if (this.data == null)
				Debug.LogError("Data not set for character: " + this.name);
		}

		private void Start()
		{
			Globals.ins.allCharacters.Add(this);
		}

		private void Update()
		{
			interactionQueue.Update();
			NeedsUpdate();
		}

		private void NeedsUpdate()
		{
			data.hunger.AddToCurrentValue(-Globals.ins.data.hungerDecreaseRate, isSelected);
			data.fun.AddToCurrentValue(-Globals.ins.data.funDecreaseRate, isSelected);
			data.social.AddToCurrentValue(-Globals.ins.data.socialDecreaseRate, isSelected);
			data.hygiene.AddToCurrentValue(-Globals.ins.data.hygieneDecreaseRate, isSelected);
			data.energy.AddToCurrentValue(-Globals.ins.data.energyDecreaseRate, isSelected);
			data.bladder.AddToCurrentValue(-Globals.ins.data.bladderDecreaseRate, isSelected);
			data.fulfillment.AddToCurrentValue(-Globals.ins.data.fulfillmentDecreaseRate, isSelected);
			data.health.AddToCurrentValue(-Globals.ins.data.healthDecreaseRate, isSelected);
			data.comfort.AddToCurrentValue(-Globals.ins.data.comfortDecreaseRate, isSelected);
	}

		
		public void SayYourName()
		{
			Debug.Log(data.characterFirstName + " " + data.characterLastName);
		}

		#region Hands
		public bool AreAllHandsFree()
		{
			if(this.bothHands && this.leftHand == null && this.rightHand == null)
			{
				return true;
			}
			return false;
		}



		//public bool AreBothHandsFree()
		//{
		//	if(this.bothHands == null)
		//	{
		//		return true;
		//	}
		//	//if (this.leftHand == null && this.rightHand == null)
		//	//{
		//	//	return true;
		//	//}
		//	return false;
		//}
		public bool IsOneHandFree()
		{
			if (this.leftHand == null || this.rightHand == null)
			{
				return true;
			}
			return false;
		}

		public bool PickUpItemBothHands(CarriableItem carriableItem)
		{
			if(this.bothHands == null && this.leftHand == null && this.rightHand == null)
			{
				this.bothHands = carriableItem;
				if (!this.tempCharacterInteractions.Contains(carriableItem.putDownInteraction))
					this.tempCharacterInteractions.Add(carriableItem.putDownInteraction);
				return true;
			}
			return false;
		}
		public bool PickUpOneFreeHand(CarriableItem carriableItem)
		{
			bool pickedUp = false;
			if (this.bothHands == null)
			{
				if (this.rightHand == null)
				{
					this.rightHand = carriableItem;
					pickedUp = true;
				}
				else if (this.leftHand == null)
				{
					this.leftHand = carriableItem;
					pickedUp = true;
				}
			}
			if(!this.tempCharacterInteractions.Contains(carriableItem.putDownInteraction))
				this.tempCharacterInteractions.Add(carriableItem.putDownInteraction);
			return pickedUp;
		}

		public CarriableItem PutDownBothHandsSameItem()
		{
			CarriableItem item;
			if (this.leftHand != null)
			{
				item = this.leftHand;
				this.leftHand = null;
				if (this.rightHand.Equals(item))
					this.rightHand = null;
			}
			else
			{
				item = this.rightHand;
				this.rightHand = null;
			}
			this.tempCharacterInteractions.Remove(item.putDownInteraction);
			return item;
		}
		internal void PutItemDown(CarriableItem carriableItem)
		{
			if (this.bothHands != null && this.bothHands.Equals(carriableItem))
			{
				this.tempCharacterInteractions.Remove(carriableItem.putDownInteraction);
				this.bothHands = null;
			}
			if (this.leftHand != null && this.leftHand.Equals(carriableItem))
			{
				if(this.rightHand == null)
					this.RemoveInteraction(carriableItem.putDownInteraction);
				this.leftHand = null;
			}
			if (this.rightHand != null && this.rightHand.Equals(carriableItem))
			{
				if (this.leftHand == null)
					this.RemoveInteraction(carriableItem.putDownInteraction);
				this.rightHand = null;
			}
		}

		public CarriableItem GetPlaceableCarriableItem(CarriableItem.ItemPlacementType placingOnItemOfType)
		{
			switch (placingOnItemOfType)
			{
				case CarriableItem.ItemPlacementType.None:
					return GetCarriedItemOfType(CarriableItem.ItemPlacementType.None);
				case CarriableItem.ItemPlacementType.Both:
					CarriableItem ci = GetCarriedItemOfType(CarriableItem.ItemPlacementType.Surface);
					if( ci == null)
						ci = GetCarriedItemOfType(CarriableItem.ItemPlacementType.Wall);
					if (ci != null)
						return ci;
					break;
				case CarriableItem.ItemPlacementType.Surface:
					CarriableItem cii = GetCarriedItemOfType(CarriableItem.ItemPlacementType.Surface);
					if (cii == null)
						cii = GetCarriedItemOfType(CarriableItem.ItemPlacementType.Both);
					if (cii != null)
						return cii;
					break;
				case CarriableItem.ItemPlacementType.Wall:
					CarriableItem ciii = GetCarriedItemOfType(CarriableItem.ItemPlacementType.Wall);
					if (ciii == null)
						ciii = GetCarriedItemOfType(CarriableItem.ItemPlacementType.Both);
					if (ciii != null)
						return ciii;
					break;
			}
			return null;
		}

		private CarriableItem GetCarriedItemOfType(CarriableItem.ItemPlacementType type)
		{
			if (this.bothHands != null && this.bothHands.itemPlacementType.Equals(type))
			{
				return this.bothHands;
			}
			else if (this.rightHand != null && this.rightHand.itemPlacementType.Equals(type))
			{
				return this.rightHand;
			}
			else if (this.leftHand != null && this.leftHand.itemPlacementType.Equals(type))
			{
				return this.leftHand;
			}
			return null;
		}
		#endregion Hands

		public void AddInteraction(Interaction interaction)
		{
			this.tempCharacterInteractions.Add(interaction);
		}
		public bool RemoveInteraction(Interaction interaction)
		{
			return this.tempCharacterInteractions.Remove(interaction);
		}
	}
}