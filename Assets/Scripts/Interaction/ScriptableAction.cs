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

		public abstract bool CheckIfPossible(InteractableItemClickedEventInfo eventInfo, ActionCanceled actionCanceled);
		public abstract void StartAction(InteractableItemClickedEventInfo eventInfo, StartActionOver startActionOver, ActionCanceled actionCanceled);
		public abstract void PerformAction(InteractableItemClickedEventInfo eventInfo, PerformActionOver performActionOver, ActionCanceled actionCanceled);
		public abstract void EndAction(InteractableItemClickedEventInfo eventInfo, EndActionOver endActionOver, ActionCanceled actionCanceled);
	}
}