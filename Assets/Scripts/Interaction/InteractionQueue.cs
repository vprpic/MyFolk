using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
	public class InteractionQueue
	{
		public int maxInteractionsPossible = 10;
		public Character owner;
		public List<(Interaction, InteractableItemClickedEvent)> interactionQueue;
		private bool runningInteraction;
		public (Interaction, InteractableItemClickedEvent) currentInteraction;

		#region current action
		public ScriptableAction currentAction;
		public int currentActionIndex;
		public ActionState currentActionState;
		public ActionStateData currentActionStateData;
		//public float interactionTimer; //for timed actions
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
			//EventSystem.Current.RegisterListener<CharacterSelectedEvent>(OnCharacterSelected);
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
				//EventSystem.Current.RegisterListener<InteractionQueueElementUIClickEvent>(OnInteractionQueueElementUIClicked);
			}
			else
			{
				InteractionQueueElementUIClickEvent.UnregisterListener(OnInteractionQueueElementUIClicked);
			}
		}

		public void OnInteractionQueueElementUIClicked(InteractionQueueElementUIClickEvent eventInfo)
		{
			if (interactionQueue == null && interactionQueue.Count < 1)
				return;

			if(eventInfo.queueIndex == 0)
			{
				//it's the first item
				CurrentInteractionCancelled();
			}
			else if(eventInfo.queueIndex > 0)
			{
				if((eventInfo.queueIndex-1) > interactionQueue.Count - 1) //-1 bc interactionQueue doesn't hold the currently performing interaction
				{
					Debug.Log("Can't remove interaction from queue, index too large: "+
						eventInfo.interactionQueueElementUI.interaction.interactionName+" "+eventInfo.queueIndex);
					return;
				}
				interactionQueue.RemoveAt(eventInfo.queueIndex - 1);
			}
			else
			{
				//can't be negative
				return;
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
		public void SetNextInteraction()
		{
			RunInteraction(false);
			currentActionState = ActionState.NotStarted;
			currentActionIndex = 0;
			if(currentInteraction != (null, null) && Globals.ins.currentlySelectedCharacter.Equals(owner)) {
				(new InteractionDequeueEvent(currentInteraction.Item1, currentInteraction.Item2)).FireEvent();
			}
			currentInteraction = DequeueFirst();
			if(interactionQueue.Count == 0)
			{
				//Debug.Log("Interaction Queue is empty");
				return;
			}

			if (currentInteraction != (null, null) && !currentInteraction.Item1.CheckIfInteractionPossible(currentInteraction.Item2))
			{
				Debug.Log("Current interaction impossible, skipping to next.");
				SetNextInteraction();
			}
			if (currentInteraction.Item1.actions != null && currentInteraction.Item1.actions.Count > 0)
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
				this.currentActionState = ActionState.NotStarted;
			}
		}
		public void RunInteraction(bool run)
		{
			runningInteraction = run;
		}

		public void CurrentInteractionCancelled()
		{
			currentAction.CancelAction(currentActionStateData, EndActionOver, ActionCanceled);
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
					//Debug.Log("Interaction not running, but selected!");
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
							currentAction.StartAction(currentInteraction.Item2, SetCurrentActionStateData, StartActionOver, ActionCanceled);
							break;
						case ActionState.Starting:
							//The callback sets this
							break;
						case ActionState.Running:
							currentAction.PerformAction(currentActionStateData, SetCurrentActionStateData, PerformActionOver, ActionCanceled);
							break;
						case ActionState.Ending:
							currentAction.EndAction(currentActionStateData, EndActionOver, ActionCanceled);
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
		#endregion Action States
	}
}
