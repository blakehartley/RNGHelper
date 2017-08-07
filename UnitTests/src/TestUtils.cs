using FF12RNGHelper.Core;

namespace UnitTests
{
    public class TestUtils
    {
        public static Character GetDefaultCharacter()
        {
            return new Character(3, 23, Spells.Cure, false);
        }

        public static CharacterGroup GetDefaultCharacterGroup()
        {
            CharacterGroup group = new CharacterGroup();
            group.AddCharacter(GetDefaultCharacter());
            group.AddCharacter(GetDefaultCharacter());
            group.AddCharacter(GetDefaultCharacter());
            return group;
        }

        public static CharacterGroup GetComplexCharacterGroup()
        {
            CharacterGroup group = new CharacterGroup();
            group.AddCharacter(GetDefaultCharacter());
            group.AddCharacter(new Character(3, 23, Spells.Cure, true));
            group.AddCharacter(new Character(3, 23, Spells.Cura, false));
            return group;
        }

        public static CharacterGroup GetSimpleCharacterGroup()
        {
            CharacterGroup group = new CharacterGroup();
            group.AddCharacter(GetDefaultCharacter());
            return group;
        }
    }
}