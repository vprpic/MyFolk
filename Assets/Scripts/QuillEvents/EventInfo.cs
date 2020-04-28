using MyFolk;
using MyFolk.FlexibleUI;
using MyFolk.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    [System.Serializable]
    public abstract class EventInfo
    {
        public string EventDescription;
    }

    public class DebugEventInfo : EventInfo
    {
        public int VerbosityLevel;
    }

    public class FlexibleUIEnterExitEventInfo : EventInfo
    {
        public FlexibleUI flexibleUI;
        public bool isHovering;
        public FlexibleUIEnterExitEventInfo() { }
        public FlexibleUIEnterExitEventInfo(FlexibleUI flexibleUI, bool isHovering)
        {
            this.flexibleUI = flexibleUI;
            this.isHovering = isHovering;
            this.EventDescription = isHovering ? "The mouse IS hovering over this UI" : "The mouse IS NOT hovering over this UI";
        }
    }
    public class RadialButtonClickEventInfo : EventInfo
    {
        public RadialButtonUI radialButtonUI;
        public RadialButtonClickEventInfo() { }
        public RadialButtonClickEventInfo(RadialButtonUI radialButton)
        {
            this.radialButtonUI = radialButton;
            this.EventDescription = "Radial button was clicked: " + radialButton.text;
        }
    }
    #region Interaction Queue
    public class InteractionQueueElementUIClickEventInfo : EventInfo
    {
        public InteractionQueueElementUI interactionQueueElementUI;
        public InteractionQueueElementUIClickEventInfo() { }
        public InteractionQueueElementUIClickEventInfo(InteractionQueueElementUI interactionQueueElementUI)
        {
            this.interactionQueueElementUI = interactionQueueElementUI;
            this.EventDescription = "Radial button was clicked: " + interactionQueueElementUI.name;
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

    public class InteractableItemClickedEventInfo : EventInfo
    {
        public InteractableItem iitem;

        public Vector3 worldClickPoint;

        public Vector3 screenClickPoint;

        public InteractableItemClickedEventInfo() { }

        public InteractableItemClickedEventInfo(InteractableItem iitem, Vector3 wcp, Vector3 scp)
        {
            this.EventDescription = iitem.itemName + " was clicked";
            this.iitem = iitem;
            this.worldClickPoint = wcp;
            this.screenClickPoint = scp;
        }
    }

    public class CharacterSelectedEventInfo : EventInfo
    {
        public Character newCharacter;
        public Character oldCharacter;

        public CharacterSelectedEventInfo(Character oldCharacter, Character newCharacter)
        {
            this.oldCharacter = oldCharacter;
            this.newCharacter = newCharacter;
        }
    }
}