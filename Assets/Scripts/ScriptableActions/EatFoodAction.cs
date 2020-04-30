using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	[CreateAssetMenu(menuName = "Actions/Get Food", fileName = "GetFood_Action")]
	public class EatFoodAction : ScriptableAction
	{
		public float maxFoodAmountToAdd;
		public float foodAmountToAddPerUpdate;

		public override bool CheckIfPossible(InteractableItemClickedEventInfo eventInfo)
		{
			return true;
		}

		public override void StartAction(InteractableItemClickedEventInfo eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			EatFoodStateData asd = new EatFoodStateData(eventInfo);
			returnCurrentInteractionState(asd);
			startActionOver();
		}

		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			EatFoodStateData asd = (EatFoodStateData)actionStateData;
			if(asd == null)
			{
				Debug.LogError("ASD is not EatFoodStateData");
				actionCanceled();
				return;
			}
			asd.currentFoodAmountAdded += this.foodAmountToAddPerUpdate;
			asd.eventInfo.character.data.hunger.AddToCurrentValue(this.foodAmountToAddPerUpdate, asd.eventInfo.character.isSelected);
			if(Mathf.Abs(asd.currentFoodAmountAdded) >= Mathf.Abs(this.maxFoodAmountToAdd))
				performActionOver();
		}

		public override void EndAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			endActionOver();
		}

		public override void CancelAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			actionCanceled();
		}
	}
}