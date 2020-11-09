using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Interactions.NewInteractions
{
    [Serializable]
    class MagicBobEncounter : ConsoleInteraction
    {
        private bool visited = false; // has this place been visited?
        public IMagicBobStrategy Strategy { get; set; } // store strategy 
        public MagicBobEncounter(GameSession ses) : base(ses)
        {
            Name = "interaction1562";
            Strategy = new MagicBobNormalStrategy(ses); // start with default strategy
        }
        protected override void RunContent()
        {
            if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("MagicBobMean"))
            {
                Strategy = new MagicBobMeanStrategy(parentSession);
                visited = false;
            }
            else if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("MagicBobFriendly"))
            {
                Strategy = new MagicBobFriendlyStrategy(parentSession);
                visited = false;
            }
            Strategy.Execute(parentSession, visited); // execute strategy
            visited = true;
        }
    }
}
