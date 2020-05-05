﻿using MyFolk;
using MyFolk.UI;
using UnityEngine;

namespace EventCallbacks
{
    public class InteractableItemClickedEvent : Event<InteractableItemClickedEvent>
    {
        public readonly int id;

        public Character character;

        public InteractableItem iitem;

        public Vector3 worldClickPoint;

        public Vector3 screenClickPoint;


        public InteractableItemClickedEvent()
        {
            this.id = Globals.ins.data.interactableItemClickedEventsCount++;
        }

        public InteractableItemClickedEvent(Character character, InteractableItem iitem, Vector3 wcp, Vector3 scp) : this()
        {
            this.character = character;
            this.EventDescription = iitem.itemName + " was clicked";
            this.iitem = iitem;
            this.worldClickPoint = wcp;
            this.screenClickPoint = scp;
        }
    }
    #region Interaction Queue
    public class InteractionQueueElementUIClickEvent : Event<InteractionQueueElementUIClickEvent>
    {
        public InteractionQueueElementUI interactionQueueElementUI;
        public InteractionQueueElementUIClickEvent() { }
        public InteractionQueueElementUIClickEvent(InteractionQueueElementUI interactionQueueElementUI)
        {
            this.interactionQueueElementUI = interactionQueueElementUI;
            this.EventDescription = "Interaction queue element was clicked: " + interactionQueueElementUI.name;
        }
    }

    public class InteractionEnqueueEvent : Event<InteractionEnqueueEvent>
    {
        public Interaction interaction;
        public InteractableItemClickedEvent interactableItemClickedEventInfo;
        public InteractionEnqueueEvent() { }
        public InteractionEnqueueEvent(Interaction interaction, InteractableItemClickedEvent interactableItemClickedEventInfo)
        {
            this.interaction = interaction;
            this.interactableItemClickedEventInfo = interactableItemClickedEventInfo;
            this.EventDescription = "Interaction enqueued: " + interaction.interactionName;
        }
    }

    public class InteractionDequeuedFromCodeEvent : Event<InteractionDequeuedFromCodeEvent>
    {
        public Interaction interaction;
        public InteractableItemClickedEvent interactableItemClickedEventInfo;
        public InteractionDequeuedFromCodeEvent() { }
        public InteractionDequeuedFromCodeEvent(Interaction interaction, InteractableItemClickedEvent interactableItemClickedEventInfo)
        {
            this.interaction = interaction;
            this.interactableItemClickedEventInfo = interactableItemClickedEventInfo;
            this.EventDescription = "Interaction dequeued: " + interaction.interactionName;
        }
    }

    public class ActiveInteractionChangedEvent : Event<ActiveInteractionChangedEvent>
    {
        public Interaction interaction;
        public InteractableItemClickedEvent interactableItemClickedEventInfo;
        public ActiveInteractionChangedEvent() { }
        public ActiveInteractionChangedEvent(Interaction interaction, InteractableItemClickedEvent interactableItemClickedEventInfo)
        {
            this.interaction = interaction;
            this.interactableItemClickedEventInfo = interactableItemClickedEventInfo;
            this.EventDescription = "Active interaction changed to: " + interaction.interactionName;
        }
    }
    #endregion Interaction Queue

}