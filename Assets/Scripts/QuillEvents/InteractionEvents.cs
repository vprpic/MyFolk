using MyFolk;
using MyFolk.UI;
using UnityEngine;

namespace EventCallbacks
{
    public class InteractableItemClickedEvent : Event<InteractableItemClickedEvent>
    {
        public Character character;

        public InteractableItem iitem;

        public Vector3 worldClickPoint;

        public Vector3 screenClickPoint;

        public InteractableItemClickedEvent() { }

        public InteractableItemClickedEvent(Character character, InteractableItem iitem, Vector3 wcp, Vector3 scp)
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
        public int queueIndex;
        public InteractionQueueElementUIClickEvent() { }
        public InteractionQueueElementUIClickEvent(InteractionQueueElementUI interactionQueueElementUI, int index)
        {
            this.interactionQueueElementUI = interactionQueueElementUI;
            this.queueIndex = index;
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

    public class InteractionDequeueEvent : Event<InteractionDequeueEvent>
    {
        public Interaction interaction;
        public InteractableItemClickedEvent interactableItemClickedEventInfo;
        public InteractionDequeueEvent() { }
        public InteractionDequeueEvent(Interaction interaction, InteractableItemClickedEvent interactableItemClickedEventInfo)
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