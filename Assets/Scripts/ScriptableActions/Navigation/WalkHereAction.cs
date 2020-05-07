using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	[CreateAssetMenu(menuName = "Actions/Navigation/Walk Here", fileName = "WalkHere_Action")]
	public class WalkHereAction : ScriptableAction
	{
		public float minRadius;
		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			NavMeshPath path = new NavMeshPath();
			eventInfo.character.navMeshAgent.CalculatePath(eventInfo.worldClickPoint, path);
			if (path.status == NavMeshPathStatus.PathComplete)
				return true;
			else
			{
				if (Vector3.Distance(eventInfo.worldClickPoint, eventInfo.character.transform.position) < minRadius)
					return true;
				else
					return false;
			}
		}
		public override bool LateCheckIfPossible(ActionStateData actionStateData)
		{
			return EarlyCheckIfPossible(actionStateData.eventInfo);
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			ActionStateData asd = new ActionStateData(eventInfo);
			if (!LateCheckIfPossible(asd))
			{
				CancelAction(asd, actionCanceled);
				return;
			}
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
		public override void CancelAction(ActionStateData actionStateData, ActionCanceled actionCanceled)
		{
			actionStateData.eventInfo.character.navMeshAgent.ResetPath();
			actionCanceled();
		}
	}
}