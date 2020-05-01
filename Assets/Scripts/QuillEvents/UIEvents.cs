using MyFolk.FlexibleUI;
using MyFolk.UI;

namespace EventCallbacks
{
    public class FlexibleUIEnterExitEvent : Event<FlexibleUIEnterExitEvent>
    {
        public FlexibleUI flexibleUI;
        public bool isHovering;
        public FlexibleUIEnterExitEvent() { }
        public FlexibleUIEnterExitEvent(FlexibleUI flexibleUI, bool isHovering)
        {
            this.flexibleUI = flexibleUI;
            this.isHovering = isHovering;
            this.EventDescription = isHovering ? "The mouse IS hovering over this UI" : "The mouse IS NOT hovering over this UI";
        }
    }
    public class RadialButtonClickEvent : Event<RadialButtonClickEvent>
    {
        public RadialButtonUI radialButtonUI;
        public RadialButtonClickEvent() { }
        public RadialButtonClickEvent(RadialButtonUI radialButton)
        {
            this.radialButtonUI = radialButton;
            this.EventDescription = "Radial button was clicked: " + radialButton.text;
        }
    }
}