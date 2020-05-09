using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	public class WalkInFrontOfHereStateData : ActionStateData
	{
		public Vector3 walkToPoint;
		public bool hasSetTheWalkingPoint;
		public bool hasWalkingStopped;
		public ScriptableAction.ActionCanceled actionCanceled;
		public WalkInFrontOfHereStateData(InteractableItemClickedEvent eventInfo, ScriptableAction.ActionCanceled actionCanceled) : base(eventInfo)
		{
			this.walkToPoint = Vector3.zero;
			this.hasSetTheWalkingPoint = false;
			this.hasWalkingStopped = false;
			this.actionCanceled = actionCanceled;
		}
	}


	[CreateAssetMenu(menuName = "Actions/Navigation/Walk In Front Of Here", fileName = "WalkInFrontOfHere_Action")]
	public class WalkInFrontOfHereAction : ScriptableAction
	{
		public float minRadius;
		//public float stoppingRadius;

		public void StoppedMoving(ActionStateData actionStateData)
		{
			WalkInFrontOfHereStateData asd = (WalkInFrontOfHereStateData)actionStateData;
			if (asd == null || asd.walkToPoint.Equals(Vector3.zero))
			{
				CancelAction(actionStateData, asd.actionCanceled);
			}
			asd.hasWalkingStopped = true;
		}


		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			NavMeshHit hit;
			if (NavMesh.SamplePosition(eventInfo.worldClickPoint, out hit, minRadius, NavMesh.AllAreas))
			{
				NavMeshPath path = new NavMeshPath();
				eventInfo.character.agent.CalculatePath(hit.position, path);
				if (path.status == NavMeshPathStatus.PathComplete)
					return true;
				else
				{
					//if (Vector3.Distance(eventInfo.worldClickPoint, eventInfo.character.transform.position) < minRadius)
					//	return true;
					//else
					return false;
				}
			}
			return false;
		}
		public override bool LateCheckIfPossible(ActionStateData actionStateData)
		{
			return EarlyCheckIfPossible(actionStateData.eventInfo);
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			WalkInFrontOfHereStateData asd = new WalkInFrontOfHereStateData(eventInfo, actionCanceled);
			//if (!LateCheckIfPossible(asd))
			//{
			//	CancelAction(asd, actionCanceled);
			//	return;
			//}

			NavMeshHit hit;
			if (NavMesh.SamplePosition(eventInfo.worldClickPoint, out hit, minRadius, NavMesh.AllAreas))
			{
				asd.walkToPoint = hit.position;
				asd.hasSetTheWalkingPoint = true;
				returnCurrentInteractionState(asd);
				startActionOver();
			}
			else
			{
				CancelAction(asd, actionCanceled);
			}
		}

		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			WalkInFrontOfHereStateData asd = (WalkInFrontOfHereStateData)actionStateData;
			if (asd == null || asd.walkToPoint.Equals(Vector3.zero))
			{
				CancelAction(actionStateData, actionCanceled);
			}

			if (asd.hasSetTheWalkingPoint && asd.eventInfo.character.motion.CanStartMoving())
			{
				asd.hasSetTheWalkingPoint = true;
				asd.eventInfo.character.motion.MoveTo(asd.walkToPoint, StoppedMoving, asd);
			}

			if (asd.hasWalkingStopped)
			{
				performActionOver();
			}

			//if (asd.hasSetTheWalkingPoint)
			//{
			//	NavMeshAgent agent = asd.eventInfo.character.agent;
			//	if (agent != null && (agent.remainingDistance < /*stoppingRadius +*/ agent.stoppingDistance || !agent.pathPending && !agent.hasPath))
			//	{
			//		actionStateData.eventInfo.character.motion.StopMoving();
			//		performActionOver();
			//	}
			//}
		}
		public override void EndAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			//NavMeshAgent agent = actionStateData.eventInfo.character.agent;
			//if (agent != null && (agent.pathPending || agent.hasPath))
			//{
			//	actionStateData.eventInfo.character.motion.StopMoving();
			//}
			endActionOver();
		}
		public override void CancelAction(ActionStateData actionStateData, ActionCanceled actionCanceled)
		{
			actionStateData.eventInfo.character.motion.StopMoving();
			actionCanceled();
		}


	}
}