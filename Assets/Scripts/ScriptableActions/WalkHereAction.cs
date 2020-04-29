using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	[CreateAssetMenu(menuName = "Interactions/Actions/Walk Here", fileName = "WalkHere_Action")]
	public class WalkHereAction : ScriptableAction
	{
		public override bool CheckIfPossible(InteractableItemClickedEventInfo eventInfo)
		{
			NavMeshPath path = new NavMeshPath();
			eventInfo.character.navMeshAgent.CalculatePath(eventInfo.worldClickPoint, path);
			if (path.status == NavMeshPathStatus.PathComplete)
				return true;
			else
				return false;
		}
		public override void StartAction(InteractableItemClickedEventInfo eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			ActionStateData asd = new ActionStateData(eventInfo);
			eventInfo.character.navMeshAgent.SetDestination(eventInfo.worldClickPoint);
			returnCurrentInteractionState(asd);
			startActionOver();
		}
		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			NavMeshAgent agent = actionStateData.eventInfo.character.navMeshAgent;
			if (!agent.pathPending && !agent.hasPath)
			{
				performActionOver();
			}
		}
		public override void EndAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			endActionOver();
		}
		public override void CancelAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			actionStateData.eventInfo.character.navMeshAgent.ResetPath();
			actionCanceled();
		}
	}
}