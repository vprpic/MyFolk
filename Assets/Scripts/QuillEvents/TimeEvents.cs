using MyFolk;
using MyFolk.Time;

namespace EventCallbacks
{
    public class GameModeChangedEvent : Event<GameModeChangedEvent>
    {
        public GameMode newGameMode;
        public GameMode oldGameMode;

        public GameModeChangedEvent(GameMode oldGameMode, GameMode newGameMode)
        {
            this.oldGameMode = oldGameMode;
            this.newGameMode = newGameMode;
        }
    }

    public class TimeScaleChangedEvent : Event<TimeScaleChangedEvent>
    {
        public float newTimeScale;
        public float oldTimeScale;

        public TimeScaleChangedEvent(float oldTimeScale, float newTimeScale)
        {
            this.oldTimeScale = oldTimeScale;
            this.newTimeScale = newTimeScale;
        }
    }

}