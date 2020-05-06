using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	[CreateAssetMenu(menuName = "Actions/Put down", fileName = "PutDown_Action")]
	public class PutDownAction : ScriptableAction
	{
		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			bool possible = true;
			if(eventInfo.hit.normal.y < 0.9f)
				possible = false;
			if (eventInfo.character.GetHeldItemBothHands() == null)
				possible = false;
			return possible;
		}

		public override bool LateCheckIfPossible(ActionStateData actionStateData)
		{
			return actionStateData.eventInfo.character.GetHeldItemBothHands() != null;
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			ActionStateData asd = new ActionStateData(eventInfo);
			if (!LateCheckIfPossible(asd))
			{
				CancelAction(asd, actionCanceled);
				return;
			}
			returnCurrentInteractionState(asd);
			startActionOver();
		}

		public override void PerformAction(ActionStateData asd, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			if (asd == null)
			{
				CancelAction(asd, actionCanceled);
				return;
			}
			HeldableItem hi = asd.eventInfo.character.PutDownBothHandsSameItem();
			if (hi != null)
				hi.PutItemDown(asd.eventInfo.worldClickPoint);
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