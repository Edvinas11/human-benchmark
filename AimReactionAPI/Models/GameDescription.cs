namespace AimReactionAPI.Models
{
    public class GameDescription
    {
        public GameDescription(string gameName, string gameDescr)
        {
            GameName = gameName;
            GameDescr = gameDescr;
        }

        public string GameName { get; set; }
        public string GameDescr { get; set; }
    }
}
