using KHSave.Attributes;
using KHSave.Lib1.Types;

namespace KHSave.Lib1.Attributes
{
    public class BlueprintGummiAttribute : InfoAttribute
    {
        public GameTypes GameType { get; set; }

        public BlueprintGummiAttribute(string info, GameTypes gameType) : base(info)
        {
            GameType = gameType;
        }
    }
}
