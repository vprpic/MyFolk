using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	public class OpenFridgeAndGrabFoodStateData : ActionStateData
	{
		public bool startedWait;
		public OpenFridgeAndGrabFoodStateData(InteractableItemClickedEvent eventInfo) : base(eventInfo)
		{
		}
	}

	[CreateAssetMenu(menuName = "Actions/Hunger/Open Fridge and Grab Food", fileName = "OpenFridgeAndGrabFood_Action")]
	public class OpenFridgeAndGrabFoodAction : ScriptableAction
	{

		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			return true;
		}

		public override bool LateCheckIfPossible(ActionStateData actionStateData)
		{
			if (actionStateData == null)
				return false;
			if (actionStateData.eventInfo.iitem.isCurrentlyBeingUsedBy != null)
				return false;

			if (!this.IsInRangeOfItem(actionStateData.eventInfo.character, actionStateData.eventInfo.iitem.transform.position, 10f))
				return false;
			if (!actionStateData.eventInfo.character.IsOneHandFree())
				return false;

			return true;
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			OpenFridgeAndGrabFoodStateData asd = new OpenFridgeAndGrabFoodStateData(eventInfo);
			asd.startedWait = false;
			if (!LateCheckIfPossible(asd))
			{
				//NEEDS TO BE CALLED IN OREDER TO PROPERLY CANCEL THE ACTION
				CancelAction(asd, actionCanceled);
				return;
			}
			Animator a = eventInfo.iitem.GetComponent<Animator>();
			if (a != null)
				a.SetBool("StartOpenDoor", true);
			asd.eventInfo.iitem.isCurrentlyBeingUsedBy = eventInfo.character;
			returnCurrentInteractionState(asd);
			startActionOver();
		}

		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			OpenFridgeAndGrabFoodStateData asd = (OpenFridgeAndGrabFoodStateData)actionStateData;
			if (asd == null)
			{
				Debug.LogError("ASD is not OpenFridgeAndGrabFoodStateData");
				CancelAction(actionStateData, actionCanceled);
				return;
			}
			//asd.eventInfo.character.data.energy.AddToCurrentValue(this.sleepAmountToAddPerUpdate, asd.eventInfo.character.isSelected);
			//if(asd.eventInfo.character.data.energy.currentValue == asd.eventInfo.character.data.energy.maxValue)
			//performActionOver();
			if (!asd.startedWait)
			{
				Globals.ins.timeManager.WaitForSeconds(5f, WaitingOver, performActionOver);
				asd.startedWait = true;
			}
		}
		public void WaitingOver(PerformActionOver performActionOver)
		{
			performActionOver();
		}

		public override void EndAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			if (actionStateData == null)
			{
				CancelAction(actionStateData, actionCanceled);
				return;
			}
			if (actionStateData.eventInfo.iitem.isCurrentlyBeingUsedBy != null && 
				actionStateData.eventInfo.iitem.isCurrentlyBeingUsedBy.Equals(actionStateData.eventInfo.character))
			{
				actionStateData.eventInfo.iitem.isCurrentlyBeingUsedBy = null;
			}
			foreach (var item in actionStateData.eventInfo.iitem.interactionPoints)
			{
				if(item.occupiedBy != null && item.occupiedBy.Equals(actionStateData.eventInfo.character))
				{
					item.occupiedBy = null;
				}
			}
			Animator a = actionStateData.eventInfo.iitem.GetComponent<Animator>();
			if (a != null)
				a.SetBool("StartOpenDoor", false);
			endActionOver();
		}

		public override void CancelAction(ActionStateData actionStateData, ActionCanceled actionCanceled)
		{
			if (actionStateData == null)
			{
				actionCanceled();
				return;
			}
			if (actionStateData.eventInfo.iitem.isCurrentlyBeingUsedBy.Equals(actionStateData.eventInfo.character))
			{
				actionStateData.eventInfo.iitem.isCurrentlyBeingUsedBy = null;
			}
			foreach (var item in actionStateData.eventInfo.iitem.interactionPoints)
			{
				if (item.occupiedBy.Equals(actionStateData.eventInfo.character))
				{
					item.occupiedBy = null;
				}
			}
			Globals.ins.timeManager.CancelWaiting();
			actionCanceled();
		}

	}
}