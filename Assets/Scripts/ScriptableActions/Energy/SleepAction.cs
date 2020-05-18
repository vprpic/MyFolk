using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	public class SleepStateData : ActionStateData
	{
		public SleepStateData(InteractableItemClickedEvent eventInfo) : base(eventInfo)
		{
		}

		//public override void ResetValues()
		//{
		//	base.ResetValues();
		//	this.timer = 0f;
		//	this.currentFoodAmountAdded = 0f;
		//}
	}

	[CreateAssetMenu(menuName = "Actions/Energy/Sleep", fileName = "Sleep_Action")]
	public class SleepAction : ScriptableAction
	{
		public float sleepAmountToAddPerUpdate;

		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			return true;
		}

		public override bool LateCheckIfPossible(ActionStateData actionStateData)
		{
			if (actionStateData == null)
			{
				return false;
			}
			if (actionStateData.eventInfo.iitem.isCurrentlyBeingUsedBy != null)
			{
				return false;
			}
			return true;
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			SleepStateData asd = new SleepStateData(eventInfo);
			if (!LateCheckIfPossible(asd))
			{
				//NEEDS TO BE CALLED IN OREDER TO PROPERLY CANCEL THE ACTION
				CancelAction(asd, actionCanceled);
				return;
			}
			asd.eventInfo.iitem.isCurrentlyBeingUsedBy = eventInfo.character;
			returnCurrentInteractionState(asd);
			startActionOver();
		}

		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			SleepStateData asd = (SleepStateData)actionStateData;
			if(asd == null)
			{
				Debug.LogError("ASD is not SleepStateData");
				CancelAction(asd, actionCanceled);
				return;
			}
			asd.eventInfo.character.data.energy.AddToCurrentValue(this.sleepAmountToAddPerUpdate, asd.eventInfo.character.isSelected);
			if(asd.eventInfo.character.data.energy.currentValue == asd.eventInfo.character.data.energy.maxValue)
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
				//TO DO: error during canceling
				if (item.occupiedBy.Equals(actionStateData.eventInfo.character))
				{
					item.occupiedBy = null;
				}
			}
			actionCanceled();
		}

	}
}