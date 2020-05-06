using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	public class PickUpStateData : ActionStateData
	{
		public HeldableItem item;
		public PickUpStateData(InteractableItemClickedEvent eventInfo) : base(eventInfo)
		{
			this.item = eventInfo.iitem.GetComponent<HeldableItem>();
		}
	}

	[CreateAssetMenu(menuName = "Actions/Pick up", fileName = "PickUp_Action")]
	public class PickUpAction : ScriptableAction
	{
		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			return eventInfo.iitem.GetComponent<HeldableItem>() != null;
		}

		public override bool LateCheckIfPossible(ActionStateData actionStateData)
		{
			return actionStateData.eventInfo.character.AreBothHandsFree();
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			PickUpStateData asd = new PickUpStateData(eventInfo);
			if (!LateCheckIfPossible(asd) || asd.item == null)
			{
				CancelAction(asd, actionCanceled);
				return;
			}
			returnCurrentInteractionState(asd);
			startActionOver();
		}

		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			PickUpStateData asd = (PickUpStateData)actionStateData;
			if(asd == null)
			{
				Debug.LogError("ASD is not PickUpStateData");
				CancelAction(actionStateData, actionCanceled);
				return;
			}
			asd.eventInfo.character.PickUpItemBothHands(asd.item.PickUpItem());
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