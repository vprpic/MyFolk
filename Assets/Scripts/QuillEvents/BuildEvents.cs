using MyFolk.Building;

namespace EventCallbacks
{
    public class SetBuildToolEvent : Event<SetBuildToolEvent>
    {
        public BuildTool newBuildTool;
        public SetBuildToolEvent() { }
        public SetBuildToolEvent(BuildTool newTool)
        {
            this.newBuildTool = newTool;
        }
    }
}