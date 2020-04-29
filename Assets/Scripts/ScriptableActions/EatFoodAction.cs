using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	[CreateAssetMenu(menuName = "Interactions/Actions/Get Food", fileName = "GetFood_Action")]
	public class EatFoodAction : ScriptableAction
	{
		public float foodAmount;

		public override bool CheckIfPossible(InteractableItemClickedEventInfo eventInfo)
		{
			return true;
		}

		public override void StartAction(InteractableItemClickedEventInfo eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			ActionStateData asd = new ActionStateData(eventInfo);
			returnCurrentInteractionState(asd);
			startActionOver();
		}

		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			actionStateData.eventInfo.character.data.Hunger.baseValue += foodAmount;

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