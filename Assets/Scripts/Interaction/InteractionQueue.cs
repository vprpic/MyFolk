using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
	public class InteractionQueue
	{
		private List<(Interaction, InteractableItemClickedEventInfo)> interactionQueue;
		private bool runningInteraction;
		public (Interaction, InteractableItemClickedEventInfo) currentInteraction;
		public ScriptableAction currentAction;
		public int currentActionIndex;
		public ActionState currentActionState;

		public InteractionQueue()
		{
			Init();
		}
		public void Init()
		{
			interactionQueue = new List<(Interaction, InteractableItemClickedEventInfo)>();
			runningInteraction = false;
			currentActionIndex = 0;
			currentActionState = ActionState.NotStarted;
		}

		public void EnqueueInteraction(Interaction interaction, InteractableItemClickedEventInfo eventInfo)
		{
			interactionQueue.Add((interaction, eventInfo));
		}

		public void RemoveFromQueue(int index)
		{
			interactionQueue.RemoveAt(index);
		}

		public (Interaction, InteractableItemClickedEventInfo) DequeueFirst()
		{
			InteractableItemClickedEventInfo info = null;
			Interaction i = null;
			if (interactionQueue != null && interactionQueue.Count > 0)
			{
				i = interactionQueue[0].Item1;
				info = interactionQueue[0].Item2;
				interactionQueue.RemoveAt(0);
			}
			return (i, info);
		}
		public void SetNextInteraction()
		{
			RunInteraction(false);
			currentActionState = ActionState.NotStarted;
			currentActionIndex = 0;
			currentInteraction = DequeueFirst();
			if(interactionQueue.Count == 0)
			{
				Debug.Log("Interaction Queue is empty");
				return;
			}

			if (currentInteraction != (null, null) && !currentInteraction.Item1.CheckIfInteractionPossible(currentInteraction.Item2, ActionCancelled))
			{
				Debug.Log("Current interaction impossible, skipping to next.");
				SetNextInteraction();
			}
			if(currentInteraction.Item1.actions != null && currentInteraction.Item1.actions.Count > 0)
				currentAction = currentInteraction.Item1.actions[0];
			RunInteraction(true);
		}

		public void SetNextAction()
		{
			int newIndex;
			currentAction = currentInteraction.Item1.GetActionAfter(currentActionIndex, out newIndex);
			if (newIndex == -1)
			{
				CurrentInteractionCompleted();
			}
			else
			{
				this.currentActionIndex = newIndex;
			}
		}
		public void RunInteraction(bool run)
		{
			runningInteraction = run;
		}

		public void CurrentInteractionCancelled()
		{
			CurrentInteractionCompleted();
		}

		public void CurrentInteractionCompleted()
		{
			SetNextInteraction();
			RunInteraction(true);
		}

		public void Update()
		{
			if (!runningInteraction)
			{
				if (currentInteraction.Item1 != null && currentInteraction.Item2 != null)
				{
					//currentAction = currentInteraction.Item1.StartInteraction(SetNextAction);
					//why am I not running?
					Debug.Log("Interaction not running, but selected!");
					RunInteraction(true);
				}
				else
				{
					SetNextInteraction();
					//Debug.Log("No currently active interaction.");
				}
			}
			else
			{
				if(currentInteraction == (null, null))
				{
					RunInteraction(false);
				}
				else if (currentAction == null)
				{
					currentAction = currentInteraction.Item1.GetFirstAction();
					currentActionIndex = 0;
				}
				else
				{
					switch (currentActionState)
					{
						case ActionState.NotStarted:
							StartedAction();
							currentAction.StartAction(currentInteraction.Item2, StartActionOver, ActionCancelled);
							break;
						case ActionState.Starting:
							//The callback sets this
							break;
						case ActionState.Running:
							currentAction.PerformAction(currentInteraction.Item2, PerformActionOver, ActionCancelled);
							break;
						case ActionState.Ending:
							currentAction.EndAction(currentInteraction.Item2, EndActionOver, ActionCancelled);
							break;
						case ActionState.Done:
							SetNextAction();
							break;
						default:
							break;
					}
				}
				//currentInteraction.RunCurrentInteraction(CurrentInteractionCompleted, CurrentInteractionCancelled);
			}
		}


		#region Action States

		public void StartedAction()
		{
			this.currentActionState = ActionState.Starting;
		}
		public void StartActionOver()
		{
			this.currentActionState = ActionState.Running;
		}

		public void PerformActionOver()
		{
			this.currentActionState = ActionState.Ending;
		}
		public void EndActionOver()
		{
			this.currentActionState = ActionState.Done;
		}

		public void ActionCancelled()
		{
			this.CurrentInteractionCancelled();
		}
		#endregion Action States
	}
}
