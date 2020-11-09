using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Interactions.NewInteractions;

namespace Game.Engine.Interactions.InteractionFactories
{
    [Serializable]
    class BobMagicBobFactory : InteractionFactory
    {
        public List<Interaction> CreateInteractionsGroup(GameSession parentSession)
        {
            // Bob and Magic Bob must always appear together in the game world
            MagicBobEncounter magicBob = new MagicBobEncounter(parentSession);
            BobInteraction bob = new BobInteraction(parentSession);
            return new List<Interaction>() { magicBob, bob };
        }
    }
}
