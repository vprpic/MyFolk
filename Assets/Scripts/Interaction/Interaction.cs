using EventCallbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyFolk.ScriptableAction;

namespace MyFolk
{
	[System.Serializable]
	[CreateAssetMenu(menuName = "Interactions/Interaction")]
	public class Interaction : ScriptableObject
	{
		public string interactionName;
		public Sprite sprite;
		public List<ScriptableAction> actions = new List<ScriptableAction>();

		internal void RunCurrentInteraction(Action currentInteractionCompleted, Action currentInteractionCancelled)
		{
			//Debug.Log("RunCurrentInteraction: "+this.actionName);
		}

		//public ScriptableAction StartInteraction(Action<int> setNextAction)
		//{
		//	ScriptableAction sa = null;
		//	if (actions != null && actions.Count > 0)
		//	{
		//		sa = actions[0];
		//		sa.StartAction();
		//		if(actions.Count > 1)
		//		{
		//			setNextAction.Invoke(1);
		//		}
		//	}

		//	return sa;
		//}
		public ScriptableAction GetFirstAction()
		{
			if (actions.Count > 0)
				return actions[0];
			else
				Debug.Log("Interaction actions are empty!");
			return null;
		}

		public ScriptableAction GetActionAfter(int index, out int newIndex)
		{
			newIndex = index;
			ScriptableAction sa = null;
			if (actions != null && actions.Count > index + 1)
			{
				newIndex++;
				sa = actions[newIndex];
			}
			else
			{
				newIndex = -1;
			}

			return sa;
		}

		public bool CheckIfInteractionPossible(InteractableItemClickedEventInfo info, ActionCanceled actionCanceled)
		{
			foreach (ScriptableAction action in actions)
			{
				if (!action.CheckIfPossible(info, actionCanceled))
				{
					return false;
				}
			}
			return true;
		}
	}
}