using MyFolk;

namespace EventCallbacks
{
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

    public class CurrentCharacterNeedChangedEventInfo : EventInfo
    {
        public Need needChanged;
        public float amountChanged;

        public CurrentCharacterNeedChangedEventInfo(Need needChanged, float amountChanged)
        {
            this.needChanged = needChanged;
            this.amountChanged = amountChanged;
        }
    }
}