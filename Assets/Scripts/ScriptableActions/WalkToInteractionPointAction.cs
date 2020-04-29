using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	[CreateAssetMenu(menuName = "Interactions/Actions/Walk To Interaction Point", fileName = "WalkToInteractionPoint_Action")]
	public class WalkToInteractionPointAction : ScriptableAction
	{
		public override bool CheckIfPossible(InteractableItemClickedEventInfo eventInfo, ActionCanceled actionCanceled)
		{
			NavMeshPath path = new NavMeshPath();
			eventInfo.character.navMeshAgent.CalculatePath(eventInfo.worldClickPoint, path);
			if (path.status == NavMeshPathStatus.PathComplete)
				return true;
			else
				return false;
		}

		public override void StartAction(InteractableItemClickedEventInfo eventInfo, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			//Debug.Log("Started walking: " + eventInfo.worldClickPoint.ToString());
			eventInfo.character.navMeshAgent.SetDestination(eventInfo.worldClickPoint);
			startActionOver.Invoke();
		}

		public override void PerformAction(InteractableItemClickedEventInfo eventInfo, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			NavMeshAgent agent = eventInfo.character.navMeshAgent;
			if (!agent.pathPending && !agent.hasPath)
			{
				Debug.Log("I have reached my destination! " + eventInfo.worldClickPoint.ToString());
				performActionOver.Invoke();
			}
		}
		public override void EndAction(InteractableItemClickedEventInfo eventInfo, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			//Debug.Log("Ended walking");
			endActionOver.Invoke();
		}

		public override void CancelAction(InteractableItemClickedEventInfo eventInfo, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			actionCanceled.Invoke();
		}
	}
}