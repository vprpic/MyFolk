using MyFolk.FlexibleUI;
using MyFolk.UI;

namespace EventCallbacks
{
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
}