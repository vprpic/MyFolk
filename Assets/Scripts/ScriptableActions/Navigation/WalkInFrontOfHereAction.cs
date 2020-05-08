using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	[CreateAssetMenu(menuName = "Actions/Navigation/Walk In Front Of Here", fileName = "WalkInFrontOfHere_Action")]
	public class WalkInFrontOfHereAction : ScriptableAction
	{
		public float minRadius;
		public float stoppingRadius;
		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			NavMeshPath path = new NavMeshPath();
			eventInfo.character.agent.CalculatePath(eventInfo.worldClickPoint, path);
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
			eventInfo.character.agent.SetDestination(eventInfo.worldClickPoint);
			returnCurrentInteractionState(asd);
			startActionOver();
		}
		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			NavMeshAgent agent = actionStateData.eventInfo.character.agent;

			//if (agent.remainingDistance > agent.stoppingDistance + stoppingRadius)
			//{
			//	actionStateData.eventInfo.character.thirdPersonCharacter.Move(agent.desiredVelocity, false, false);
			//}
			//else
			//{
			//	actionStateData.eventInfo.character.thirdPersonCharacter.Move(Vector3.zero, false, false);
			//	agent.ResetPath();
			//	performActionOver();
			//}


			if (agent != null && (agent.remainingDistance < stoppingRadius + agent.stoppingDistance || !agent.pathPending && !agent.hasPath))
			{
				agent.ResetPath();
				agent.velocity = Vector3.zero;
				performActionOver();
			}
		}
		public override void EndAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			NavMeshAgent agent = actionStateData.eventInfo.character.agent;
			if (agent != null && (agent.pathPending || agent.hasPath))
			{
				agent.ResetPath();
			}
			endActionOver();
		}
		public override void CancelAction(ActionStateData actionStateData, ActionCanceled actionCanceled)
		{
			actionStateData.eventInfo.character.agent.ResetPath();
			actionCanceled();
		}
	}
}