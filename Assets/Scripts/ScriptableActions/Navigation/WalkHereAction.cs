using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	public class WalkHereStateData : ActionStateData
	{
		public Vector3 walkToPoint;
		public bool hasSetTheWalkingPoint;
		public bool hasWalkingStopped;
		public ScriptableAction.ActionCanceled actionCanceled;
		public WalkHereStateData(InteractableItemClickedEvent eventInfo, ScriptableAction.ActionCanceled actionCanceled) : base(eventInfo)
		{
			this.walkToPoint = Vector3.zero;
			this.hasSetTheWalkingPoint = false;
			this.hasWalkingStopped = false;
			this.actionCanceled = actionCanceled;
		}
	}

	[CreateAssetMenu(menuName = "Actions/Navigation/Walk Here", fileName = "WalkHere_Action")]
	public class WalkHereAction : ScriptableAction
	{
		public float minRadius;

		public void StoppedMoving(ActionStateData actionStateData)
		{
			WalkHereStateData asd = (WalkHereStateData)actionStateData;
			if (asd == null || asd.walkToPoint.Equals(Vector3.zero))
			{
				CancelAction(actionStateData, asd.actionCanceled);
			}
			asd.hasWalkingStopped = true;
		}


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
			WalkHereStateData asd = new WalkHereStateData(eventInfo, actionCanceled);
			if (!LateCheckIfPossible(asd))
			{
				CancelAction(asd, actionCanceled);
				return;
			}

			asd.walkToPoint = eventInfo.worldClickPoint;
			asd.hasSetTheWalkingPoint = true;
			returnCurrentInteractionState(asd);
			startActionOver();
		}
		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			WalkHereStateData asd = (WalkHereStateData)actionStateData;
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
		}
		public override void EndAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			endActionOver();
		}
		public override void CancelAction(ActionStateData actionStateData, ActionCanceled actionCanceled)
		{
			actionStateData.eventInfo.character.motion.StopMoving();
			actionCanceled();
		}
	}
}