using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
	[System.Serializable]
	public class InteractionQueue
	{
		public int maxInteractionsPossible = 10;
		public Character owner;
		public List<(Interaction, InteractableItemClickedEvent)> interactionQueue;
		private bool runningInteraction;
		public (Interaction, InteractableItemClickedEvent) CurrentInteraction => interactionQueue.Count > 0 ? interactionQueue[0] : (null, null);

		#region current action
		public ScriptableAction CurrentAction => CurrentInteraction != (null, null) && currentActionIndex >= 0 
			&& CurrentInteraction.Item1.actions.Count > currentActionIndex?
			CurrentInteraction.Item1.actions[currentActionIndex] : null;
		public int currentActionIndex;
		public ActionState currentActionState;
		public ActionStateData currentActionStateData;
		#endregion current action


		
		public InteractionQueue(Character owner)
		{
			this.owner = owner;
			Init();
		}
		public void Init()
		{
			interactionQueue = new List<(Interaction, InteractableItemClickedEvent)>();
			runningInteraction = false;
			currentActionIndex = 0;
			currentActionState = ActionState.NotStarted;
			CharacterSelectedEvent.RegisterListener(OnCharacterSelected);
		}

		public void EnqueueInteraction(Interaction interaction, InteractableItemClickedEvent eventInfo)
		{
			if(interactionQueue.Count >= 10)
			{
				//TODO: give feedback to player
				Debug.Log("max interaction capacity reached.");
				return;
			}
			interactionQueue.Add((interaction, eventInfo));
			(new InteractionEnqueueEvent(interaction, eventInfo)).FireEvent();
		}

		public void OnCharacterSelected(CharacterSelectedEvent eventInfo)
		{
			if (owner.Equals(eventInfo.newCharacter))
			{
				InteractionQueueElementUIClickEvent.RegisterListener(OnInteractionQueueElementUIClicked);
			}
			else
			{
				InteractionQueueElementUIClickEvent.UnregisterListener(OnInteractionQueueElementUIClicked);
			}
		}

		public void OnInteractionQueueElementUIClicked(InteractionQueueElementUIClickEvent eventInfo)
		{
			if (!this.owner.Equals(eventInfo.interactionQueueElementUI.interactableItemClickedEventInfo.character))
			{
				Debug.Log("not owner");
				return;
			}
			if (interactionQueue == null && interactionQueue.Count < 1)
				return;

			(Interaction, InteractableItemClickedEvent) temp = interactionQueue.Find(
				a => a.Item2.id == eventInfo.interactionQueueElementUI.interactableItemClickedEventInfo.id);
			if(temp == (null, null))
			{
				return;
			}
			if(temp.Item2.id == CurrentInteraction.Item2.id)
			{
				CurrentInteractionPlayerCanceled();
			}
			else
			{
				interactionQueue.Remove(temp);
			}
		}

		public (Interaction, InteractableItemClickedEvent) DequeueFirst()
		{
			InteractableItemClickedEvent info = null;
			Interaction i = null;
			if (interactionQueue != null && interactionQueue.Count > 0)
			{
				i = interactionQueue[0].Item1;
				info = interactionQueue[0].Item2;
				interactionQueue.RemoveAt(0);
			}
			return (i, info);
		}
		public void SetNextInteraction(bool playerCanceled = false)
		{
			if(!playerCanceled && CurrentInteraction != (null, null) && Globals.ins.currentlySelectedCharacter.Equals(owner)) {
				(new InteractionDequeuedFromCodeEvent(CurrentInteraction.Item1, CurrentInteraction.Item2)).FireEvent();
			}
			RunInteraction(false);
			currentActionState = ActionState.NotStarted;
			currentActionIndex = 0;
			DequeueFirst();
			if(interactionQueue.Count == 0)
			{
				return;
			}

			if (CurrentInteraction != (null, null) && !CurrentInteraction.Item1.CheckIfInteractionPossible(CurrentInteraction.Item2))
			{
				Debug.Log("Current interaction impossible, skipping to next.");
				SetNextInteraction();
			}
			RunInteraction(true);
		}

		public void SetNextAction()
		{
			int newIndex;
			CurrentInteraction.Item1.GetActionAfter(currentActionIndex, out newIndex);
			if (newIndex == -1)
			{
				CurrentInteractionCompleted();
			}
			else
			{
				this.currentActionIndex = newIndex;
				this.currentActionState = ActionState.NotStarted;
			}
		}
		public void RunInteraction(bool run)
		{
			runningInteraction = run;
		}

		public void CurrentInteractionCancelled()
		{
			if(CurrentAction == null)
			{
				Debug.Log("currentAction == null, can't cancel it");
			}
			CurrentAction.CancelAction(currentActionStateData, ActionPlayerCanceled);
		}

		public void CurrentInteractionCompleted(bool isPlayerCanceled = false)
		{
			SetNextInteraction(isPlayerCanceled);
		}

		public void CurrentInteractionPlayerCanceled()
		{
			CurrentInteractionCancelled();
		}

		public void Update()
		{
			if (CurrentInteraction == (null, null))
			{
				SetNextInteraction();
			}
			else if (CurrentAction == null)
			{
				SetNextAction();
			}
			else
			{
				switch (currentActionState)
				{
					case ActionState.NotStarted:
						StartedAction();
						CurrentAction.StartAction(CurrentInteraction.Item2, SetCurrentActionStateData, StartActionOver, ActionCanceled);
						break;
					case ActionState.Starting:
						//The callback sets this
						break;
					case ActionState.Running:
						CurrentAction.PerformAction(currentActionStateData, SetCurrentActionStateData, PerformActionOver, ActionCanceled);
						break;
					case ActionState.Ending:
						CurrentAction.EndAction(currentActionStateData, EndActionOver, ActionCanceled);
						break;
					case ActionState.Done:
						SetNextAction();
						break;
					default:
						break;
				}
			}
		}


		#region Action States

		public void SetCurrentActionStateData(ActionStateData actionStateData)
		{
			this.currentActionStateData = actionStateData;
		}

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

		public void ActionCanceled()
		{
			CurrentInteractionCompleted();
		}
		public void ActionPlayerCanceled()
		{
			CurrentInteractionCompleted(true);
		}
		#endregion Action States
	}
}
