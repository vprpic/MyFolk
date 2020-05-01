using MyFolk;

namespace EventCallbacks
{
    public class CharacterSelectedEvent : Event<CharacterSelectedEvent>
    {
        public Character newCharacter;
        public Character oldCharacter;

        public CharacterSelectedEvent(Character oldCharacter, Character newCharacter)
        {
            this.oldCharacter = oldCharacter;
            this.newCharacter = newCharacter;
        }
    }

    public class CurrentCharacterNeedChangedEvent : Event<CurrentCharacterNeedChangedEvent>
    {
        public Need needChanged;
        public float amountChanged;

        public CurrentCharacterNeedChangedEvent(Need needChanged, float amountChanged)
        {
            this.needChanged = needChanged;
            this.amountChanged = amountChanged;
        }
    }
}