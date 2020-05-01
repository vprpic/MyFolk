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
		public InteractionQueueElementUI currentElement => UIElements[0];

		public void Awake()
		{
			//EventCallbacks.EventSystem.Current.RegisterListener<CharacterSelectedEvent>(OnCharacterSelected);
			CharacterSelectedEvent.RegisterListener(OnCharacterSelected);
			//EventCallbacks.EventSystem.Current.RegisterListener<InteractionEnqueueEvent>(OnEnqueuedInteraction);
			InteractionEnqueueEvent.RegisterListener(OnEnqueuedInteraction);
			//EventCallbacks.EventSystem.Current.RegisterListener<InteractionDequeueEvent>(OnDequeuedInteraction);
			InteractionDequeueEvent.RegisterListener(OnDequeuedInteraction);
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
		public void OnDequeuedInteraction(InteractionDequeueEvent interactionDequeueEventInfo)
		{
			DequeueElement(interactionDequeueEventInfo);
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
			//int i = UIElements.FindIndex(a => a == interactionQueueElementUI);
			//Debug.Log("index: " + i);
			//return i;
			return UIElements.IndexOf(interactionQueueElementUI);
		}

		public void DequeueElement((Interaction, InteractableItemClickedEvent) interaction)
		{
			for (int i = UIElements.Count - 1; i >= 0; i--)
			{
				var item = UIElements[i];
				if(item.interaction.Equals(interaction.Item1) && item.interactableItemClickedEventInfo.Equals(interaction.Item2))
				{
					UIElements.RemoveAt(i);//(UIElements.Find(a => a.interaction.Equals(interaction.Item1) && a.interactableItemClickedEventInfo.Equals(interaction.Item2)));
					if(item != null)
						GameObject.Destroy(item.gameObject);
					return;
				}
				if(item == null)
				{
					UIElements.RemoveAt(i);
				}
			}
		}

		internal int DequeueElement(InteractionQueueElementUI interactionQueueElementUI)
		{
			if (UIElements == null || UIElements.Count < 1)
				return -1;
			int i = GetQueueIndex(interactionQueueElementUI);
			UIElements.RemoveAt(i);
			GameObject.Destroy(interactionQueueElementUI.gameObject);
			return i;
		}

		public void DequeueElement(InteractionDequeueEvent interaction)
		{
			DequeueElement((interaction.interaction, interaction.interactableItemClickedEventInfo));
		}
	}
}