using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	public class WalkToIntearctionPointStateData : ActionStateData
	{
		public InteractionPoint currentInteractionPoint;
		public bool hasSetTheWalkingPoint;
		public bool hasWalkingStopped;
		public ScriptableAction.ActionCanceled actionCanceled;

		public WalkToIntearctionPointStateData(InteractableItemClickedEvent eventInfo, ScriptableAction.ActionCanceled actionCanceled) : base(eventInfo)
		{
			this.hasSetTheWalkingPoint = false;
			this.hasWalkingStopped = false;
			this.actionCanceled = actionCanceled;
		}
	}

	[CreateAssetMenu(menuName = "Actions/Navigation/Walk To Interaction Point", fileName = "WalkToInteractionPoint_Action")]
	public class WalkToInteractionPointAction : ScriptableAction
	{
		public void StoppedMoving(ActionStateData actionStateData)
		{
			WalkToIntearctionPointStateData asd = (WalkToIntearctionPointStateData)actionStateData;
			if (asd == null || asd.currentInteractionPoint.Equals(Vector3.zero))
			{
				CancelAction(actionStateData, asd.actionCanceled);
			}
			asd.hasWalkingStopped = true;
		}

		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			NavMeshPath path = new NavMeshPath();
			InteractionPoint tempPoint = GetClosestPoint(eventInfo, false);
			if(tempPoint == null)
			{
				return false;
			}
			eventInfo.character.agent.CalculatePath(tempPoint.point, path);
			if (path.status == NavMeshPathStatus.PathComplete)
				return true;
			else
				return false;
		}

		public override bool LateCheckIfPossible(ActionStateData actionStateData)
		{
			NavMeshPath path = new NavMeshPath();
			InteractionPoint tempPoint = GetClosestPoint(actionStateData.eventInfo);
			if (tempPoint == null)
				return false;

			actionStateData.eventInfo.character.agent.CalculatePath(tempPoint.point, path);
			if (path.status == NavMeshPathStatus.PathComplete)
				return true;
			else
				return false;
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			WalkToIntearctionPointStateData asd = new WalkToIntearctionPointStateData(eventInfo, actionCanceled);
			if (!LateCheckIfPossible(asd))
			{
				CancelAction(asd, actionCanceled);
				return;
			}

			InteractionPoint tempPoint = GetClosestPoint(asd.eventInfo);
			tempPoint.occupiedBy = asd.eventInfo.character;
			asd.currentInteractionPoint = tempPoint;
			asd.hasSetTheWalkingPoint = true;
			returnCurrentInteractionState(asd);
			startActionOver();
		}

		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			WalkToIntearctionPointStateData asd = (WalkToIntearctionPointStateData)actionStateData;
			if (asd == null || asd.currentInteractionPoint.Equals(Vector3.zero))
			{
				CancelAction(actionStateData, actionCanceled);
			}

			if (asd.hasSetTheWalkingPoint && asd.eventInfo.character.motion.CanStartMoving())
			{
				asd.hasSetTheWalkingPoint = true;
				asd.eventInfo.character.motion.MoveTo(asd.currentInteractionPoint.point, StoppedMoving, asd);
			}

			if (asd.hasWalkingStopped)
			{
				performActionOver();
			}
		}
		public override void EndAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			WalkToIntearctionPointStateData asd = (WalkToIntearctionPointStateData)actionStateData;
			if (asd == null)
			{
				CancelAction(actionStateData, actionCanceled);
				return;
			}
			endActionOver();
		}

		public override void CancelAction(ActionStateData actionStateData, ActionCanceled actionCanceled)
		{
			if (actionStateData is WalkToIntearctionPointStateData)
			{
				WalkToIntearctionPointStateData asd = (WalkToIntearctionPointStateData)actionStateData;
				if (asd == null)
				{
					actionCanceled();
					return;
				}
				asd.currentInteractionPoint.occupiedBy = null;
			}
			actionStateData.eventInfo.character.motion.StopMoving();
			actionCanceled();
		}

		public InteractionPoint GetClosestPoint(InteractableItemClickedEvent eventInfo, bool isFree = true)
		{
			if(eventInfo.iitem.interactionPoints.Count < 1)
			{
				return null;
			}
			Vector3 point = eventInfo.worldClickPoint;
			List<InteractionPoint> ips =
				eventInfo.iitem.interactionPoints.OrderBy(a => Vector3.Distance(point, a.point)).ToList();
			if (isFree)
			{
				foreach (InteractionPoint item in ips)
				{
					if (item.occupiedBy == null)
					{
						return item;
					}
				}
			}
			else
			{
				if(ips.Count > 0)
					return ips[0];
			}
			return null;
		}
	}
}