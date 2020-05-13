using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	public class EatFoodStateData : ActionStateData
	{
		public float currentFoodAmountAdded;
		public float timer;

		public EatFoodStateData(InteractableItemClickedEvent eventInfo) : base(eventInfo)
		{
		}

		//public override void ResetValues()
		//{
		//	base.ResetValues();
		//	this.timer = 0f;
		//	this.currentFoodAmountAdded = 0f;
		//}
	}

	[CreateAssetMenu(menuName = "Actions/Hunger/Get Food", fileName = "GetFood_Action")]
	public class EatFoodAction : ScriptableAction
	{
		public float maxFoodAmountToAdd;
		public float foodAmountToAddPerUpdate;

		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			return true;
		}

		public override bool LateCheckIfPossible(ActionStateData actionStateData)
		{
			return true;
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			EatFoodStateData asd = new EatFoodStateData(eventInfo);
			if (!LateCheckIfPossible(asd))
			{
				CancelAction(asd, actionCanceled);
				return;
			}
			returnCurrentInteractionState(asd);
			startActionOver();
		}

		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			EatFoodStateData asd = (EatFoodStateData)actionStateData;
			if(asd == null)
			{
				Debug.LogError("ASD is not EatFoodStateData");
				CancelAction(actionStateData, actionCanceled);
				return;
			}
			asd.currentFoodAmountAdded += this.foodAmountToAddPerUpdate;
			asd.eventInfo.character.data.hunger.AddToCurrentValue(this.foodAmountToAddPerUpdate, asd.eventInfo.character.isSelected);
			if(Mathf.Abs(asd.currentFoodAmountAdded) >= Mathf.Abs(this.maxFoodAmountToAdd) || asd.eventInfo.character.data.hunger.currentValue >= asd.eventInfo.character.data.hunger.maxValue)
				performActionOver();
		}

		public override void EndAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			endActionOver();
		}

		public override void CancelAction(ActionStateData actionStateData, ActionCanceled actionCanceled)
		{
			actionCanceled();
		}

	}
}