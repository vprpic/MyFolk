using MyFolk;
using MyFolk.UI;
using UnityEngine;

namespace EventCallbacks
{
    public class InteractableItemClickedEventInfo : EventInfo
    {
        public Character character;

        public InteractableItem iitem;

        public Vector3 worldClickPoint;

        public Vector3 screenClickPoint;

        public InteractableItemClickedEventInfo() { }

        public InteractableItemClickedEventInfo(Character character, InteractableItem iitem, Vector3 wcp, Vector3 scp)
        {
            this.character = character;
            this.EventDescription = iitem.itemName + " was clicked";
            this.iitem = iitem;
            this.worldClickPoint = wcp;
            this.screenClickPoint = scp;
        }
    }
    #region Interaction Queue
    public class InteractionQueueElementUIClickEventInfo : EventInfo
    {
        public InteractionQueueElementUI interactionQueueElementUI;
        public int queueIndex;
        public InteractionQueueElementUIClickEventInfo() { }
        public InteractionQueueElementUIClickEventInfo(InteractionQueueElementUI interactionQueueElementUI, int index)
        {
            this.interactionQueueElementUI = interactionQueueElementUI;
            this.queueIndex = index;
            this.EventDescription = "Interaction queue element was clicked: " + interactionQueueElementUI.name;
        }
    }

    public class InteractionEnqueueEventInfo : EventInfo
    {
        public Interaction interaction;
        public InteractableItemClickedEventInfo interactableItemClickedEventInfo;
        public InteractionEnqueueEventInfo() { }
        public InteractionEnqueueEventInfo(Interaction interaction, InteractableItemClickedEventInfo interactableItemClickedEventInfo)
        {
            this.interaction = interaction;
            this.interactableItemClickedEventInfo = interactableItemClickedEventInfo;
            this.EventDescription = "Interaction enqueued: " + interaction.interactionName;
        }
    }

    public class InteractionDequeueEventInfo : EventInfo
    {
        public Interaction interaction;
        public InteractableItemClickedEventInfo interactableItemClickedEventInfo;
        public InteractionDequeueEventInfo() { }
        public InteractionDequeueEventInfo(Interaction interaction, InteractableItemClickedEventInfo interactableItemClickedEventInfo)
        {
            this.interaction = interaction;
            this.interactableItemClickedEventInfo = interactableItemClickedEventInfo;
            this.EventDescription = "Interaction dequeued: " + interaction.interactionName;
        }
    }

    public class ActiveInteractionChangedEventInfo : EventInfo
    {
        public Interaction interaction;
        public InteractableItemClickedEventInfo interactableItemClickedEventInfo;
        public ActiveInteractionChangedEventInfo() { }
        public ActiveInteractionChangedEventInfo(Interaction interaction, InteractableItemClickedEventInfo interactableItemClickedEventInfo)
        {
            this.interaction = interaction;
            this.interactableItemClickedEventInfo = interactableItemClickedEventInfo;
            this.EventDescription = "Active interaction changed to: " + interaction.interactionName;
        }
    }
    #endregion Interaction Queue

}