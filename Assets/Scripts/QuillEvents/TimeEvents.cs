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

    public class DayPassedEvent : Event<DayPassedEvent>
    {
        public int newDay;
        public int oldDay;

        public DayPassedEvent(int oldDay, int newDay)
        {
            this.oldDay = oldDay;
            this.newDay = newDay;
        }
    }
    public class MonthPassedEvent : Event<MonthPassedEvent>
    {
        public int newMonth;
        public int oldMonth;

        public MonthPassedEvent(int oldMonth, int newMonth)
        {
            this.oldMonth = oldMonth;
            this.newMonth = newMonth;
        }
    }
    public class YearPassedEvent : Event<YearPassedEvent>
    {
        public int newYear;
        public int oldYear;

        public YearPassedEvent(int oldYear, int newYear)
        {
            this.oldYear = oldYear;
            this.newYear = newYear;
        }
    }

}