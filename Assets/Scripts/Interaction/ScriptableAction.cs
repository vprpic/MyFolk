using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyFolk
{

	public enum ActionState
	{
		NotStarted,
		Starting,
		Running,
		Ending,
		Done
	}
	[System.Serializable]
	public abstract class ScriptableAction : ScriptableObject
	{
		public string actionName;
		//TODO:
		//public float attenuation;
		//Attenuation reduces the attractiveness of an interaction over a distance. The higher the attenuation the closer the Sim will have
		//to be before he feels like using the object.

		//TODO: save animations for each action

		#region Flags
		[SerializeField]
		private List<Flag> flags;

		public bool ContainsFlag(Flag flag)
		{
			foreach (Flag f in flags)
			{
				if (f.value.Equals(flag.value))
				{
					return true;
				}
			}
			return false;
		}

		//TODO: test
		public bool ContainsAllFlags(List<Flag> flagList)
		{
			bool contained = true;
			foreach (Flag flag1 in flagList)
			{
				bool currentContained = false;
				foreach (Flag flag2 in flags)
				{
					if (flag2.value.Equals(flag1.value))
					{
						currentContained = true;
						continue;
					}
				}
				if (!currentContained)
				{
					contained = false;
				}
			}
			return contained;
		}

		#endregion Flags

		#region Advertisement

		//TODO: when AI implementation

		#endregion Advertisement

		public delegate void StartActionOver(); //the interaction is done starting, you can call the start action
		public delegate void PerformActionOver(); //the interaction is done starting, you can call the end action
		public delegate void EndActionOver(); //the interaction is done starting, you can call the end action
		public delegate void ActionCanceled();
		public delegate void ReturnCurrentInteractionState(ActionStateData actionStateData);

		public abstract bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo);
		public abstract bool LateCheckIfPossible(ActionStateData actionStateData);
		public abstract void StartAction(InteractableItemClickedEvent eventInfo,
			ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled);
		public abstract void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState,
			PerformActionOver performActionOver, ActionCanceled actionCanceled);
		public abstract void EndAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled);
		public abstract void CancelAction(ActionStateData actionStateData, ActionCanceled actionCanceled);


		#region Checks

		public bool IsInRangeOfItem(Character character, Vector3 position, float range)
		{
			float distance = Vector3.Distance(character.transform.position, position);
			if (distance < range)
				return true;
			return false;
		}

		#endregion Checks
	}
}