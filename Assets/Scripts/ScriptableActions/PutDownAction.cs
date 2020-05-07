using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	public class PutDownStateData : ActionStateData
	{
		public CarriableItem item;
		public PutDownStateData(InteractableItemClickedEvent eventInfo, CarriableItem item) : base(eventInfo)
		{
			this.item = item;
		}
	}

	[CreateAssetMenu(menuName = "Actions/Put down", fileName = "PutDown_Action")]
	public class PutDownAction : ScriptableAction
	{
		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			//Debug.Log("place normal: " + eventInfo.hit.normal.ToString());
			if(eventInfo.iitem.ItemPlacementType.Equals(CarriableItem.ItemPlacementType.None))
			{
				return false;
			}
			if (eventInfo.character.AreAllHandsFree())
			{
				return false;
			}
			return true;
			
		}

		public override bool LateCheckIfPossible(ActionStateData actionStateData)
		{
			if(!EarlyCheckIfPossible(actionStateData.eventInfo))
			{
				return false;
			}
			PutDownStateData asd = (PutDownStateData)actionStateData;
			if (asd == null)
			{
				Debug.Log("ASD is not PutDownStateData");
				return false;
			}
			if(asd.item != null)
				return asd.item.CanPlaceItem(asd.eventInfo.iitem, asd.eventInfo.hit);
			return false;
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			CarriableItem.ItemPlacementType clickedOn = CarriableItem.ItemPlacementType.None;

			if (eventInfo.hit.normal.y <= 0.1f && eventInfo.hit.normal.y >= -0.1f)
				clickedOn = CarriableItem.ItemPlacementType.Wall;
			else if (eventInfo.hit.normal.y >= 0.9f)
				clickedOn = CarriableItem.ItemPlacementType.Surface;

			PutDownStateData asd = new PutDownStateData(eventInfo, eventInfo.character.GetPlaceableCarriableItem(clickedOn));
			if(asd.item == null || !LateCheckIfPossible(asd))
			{
				CancelAction(asd, actionCanceled);
				return;
			}
			returnCurrentInteractionState(asd);
			startActionOver();
		}

		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			PutDownStateData asd = (PutDownStateData)actionStateData;
			if (asd == null)
			{
				Debug.Log("ASD is not PutDownStateData");
				CancelAction(asd, actionCanceled);
				return;
			}
			if (asd.item != null)
				asd.item.PutItemDown(asd.eventInfo.character, asd.eventInfo.iitem, asd.eventInfo.hit);
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