using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	[System.Serializable]
	[RequireComponent(typeof(NavMeshAgent))]
	public class Character : MonoBehaviour
	{
		public CharacterData data;
		public NavMeshAgent navMeshAgent;
		public InteractionQueue interactionQueue;
		public bool isSelected => data.isSelected;

		private void Awake()
		{
			navMeshAgent = GetComponent<NavMeshAgent>();
			interactionQueue = new InteractionQueue(this);
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
		public bool AreBothHandsFree()
		{
			if (this.data.leftHand != null && this.data.rightHand != null)
			{
				return true;
			}
			return false;
		}

		public void PickUpItemBothHands(GameObject gameObject)
		{
			this.data.leftHand = gameObject;
			this.data.rightHand = gameObject;
		}
		#endregion Hands
	}
}