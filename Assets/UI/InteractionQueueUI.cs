using EventCallbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.UI
{
	public class InteractionQueueUI : MonoBehaviour
	{
		public InteractionQueueElementUI interactionQueueElementUIPrefab;
		public List<InteractionQueueElementUI> UIElements;
		public InteractionQueueElementUI currentElement => UIElements != null && UIElements.Count > 0 ? UIElements[0] : null;

		public void Awake()
		{
			CharacterSelectedEvent.RegisterListener(OnCharacterSelected);
			InteractionEnqueueEvent.RegisterListener(OnEnqueuedInteraction);
			InteractionDequeuedFromCodeEvent.RegisterListener(OnDequeuedInteractionFromCode);
		}

		public void OnCharacterSelected(CharacterSelectedEvent characterSelectedEventInfo)
		{
			EnqueueElements(characterSelectedEventInfo.newCharacter.interactionQueue.interactionQueue);
		}

		private void EnqueueElements(List<(Interaction, InteractableItemClickedEvent)> currentQueue)
		{
			for (int i = this.transform.childCount - 1; i >= 0; i--)
			{
				GameObject.Destroy(this.transform.GetChild(i).gameObject);
			}
			UIElements.Clear();
			foreach (var item in currentQueue)
			{
				EnqueueElement(item);
			}
		}

		public void OnEnqueuedInteraction(InteractionEnqueueEvent interactionQueueEnqueuedEventInfo)
		{
			EnqueueElement(interactionQueueEnqueuedEventInfo);
		}
		public void OnDequeuedInteractionFromCode(InteractionDequeuedFromCodeEvent interactionDequeueEvent)
		{
			DequeueUIElement((interactionDequeueEvent.interaction, interactionDequeueEvent.interactableItemClickedEventInfo),interactionDequeueEvent.index);
		}

		public void EnqueueElement((Interaction, InteractableItemClickedEvent) interaction)
		{
			InteractionQueueElementUI newElement = Instantiate(interactionQueueElementUIPrefab) as InteractionQueueElementUI;
			newElement.transform.SetParent(transform, false);

			UIElements.Add(newElement);

			newElement.Init(this, interaction);
			newElement.Animate();
		}

		public void EnqueueElement(InteractionEnqueueEvent interaction)
		{
			EnqueueElement((interaction.interaction,interaction.interactableItemClickedEventInfo));
		}

		internal int GetQueueIndex(InteractionQueueElementUI interactionQueueElementUI)
		{
			return UIElements.IndexOf(interactionQueueElementUI);
		}

		public void DequeueUIElement((Interaction, InteractableItemClickedEvent) interaction, int index)
		{
			if (UIElements.Count > index && UIElements[index].interaction.Equals(interaction.Item1) 
				&& UIElements[index].interactableItemClickedEventInfo.Equals(interaction.Item2))
			{
				DequeueUIElement(UIElements[index]);
			}
			else
			{
				for (int i = UIElements.Count - 1; i >= 0; i--)
				{
					var item = UIElements[i];
					if (item.interaction.Equals(interaction.Item1) && item.interactableItemClickedEventInfo.Equals(interaction.Item2))
					{
						DequeueUIElement(UIElements[i]);
					}
				}
			}
		}

		internal int DequeueUIElement(InteractionQueueElementUI interactionQueueElementUI)
		{
			if (UIElements == null || UIElements.Count < 1)
				return -1;
			int i = GetQueueIndex(interactionQueueElementUI);
			UIElements.RemoveAt(i);
			GameObject.Destroy(interactionQueueElementUI.gameObject);
			return i;
		}
	}
}