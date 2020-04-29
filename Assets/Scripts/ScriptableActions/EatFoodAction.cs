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

		public override bool CheckIfPossible(InteractableItemClickedEventInfo eventInfo, ActionCanceled actionCanceled)
		{
			return true;
		}

		public override void StartAction(InteractableItemClickedEventInfo eventInfo, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			startActionOver.Invoke();
		}

		public override void PerformAction(InteractableItemClickedEventInfo eventInfo, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			eventInfo.character.data.Hunger.baseValue += foodAmount;

			performActionOver.Invoke();
		}
		public override void EndAction(InteractableItemClickedEventInfo eventInfo, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			endActionOver.Invoke();
		}

		public override void CancelAction(InteractableItemClickedEventInfo eventInfo, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			actionCanceled.Invoke();
		}
	}
}