
namespace AimReactionAPI.Models
{
    public class GameDescription
    {
        public GameDescription(string gameName, string gameDescr, GameType gameType)
        {
            GameName = gameName;
            GameDescr = gameDescr;
            GameType = gameType;
        }

        public string GameName { get; private set; }
        public string GameDescr { get; private set; }
        public GameType GameType {get; private set;}
    }
}
