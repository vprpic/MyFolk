using EventCallbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyFolk.ScriptableAction;

namespace MyFolk
{
	[System.Serializable]
	[CreateAssetMenu(menuName = "Interaction")]
	public class Interaction : ScriptableObject
	{
		public string interactionName;
		public Sprite radialButtonSprite;
		public List<ScriptableAction> actions = new List<ScriptableAction>();

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

		public bool CheckIfInteractionPossible(InteractableItemClickedEvent info)
		{
			foreach (ScriptableAction action in actions)
			{
				if(action == null)
				{
					Debug.LogError("Action not set correctly for interaction: " + this.interactionName);
					return false;
				}
				if (!action.CheckIfPossible(info))
				{
					return false;
				}
			}
			return true;
		}
	}
}