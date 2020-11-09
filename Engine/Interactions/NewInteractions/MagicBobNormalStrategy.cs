using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Interactions.NewInteractions
{
    [Serializable]
    class MagicBobNormalStrategy : ListBoxInteraction,IMagicBobStrategy
    {
        public MagicBobNormalStrategy(GameSession parentSession) : base(parentSession)
        {
        }
        protected override void RunContent()
        {
            if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("MagicBobAppreciates"))
            {
                parentSession.SendText("\nHi again. Nothin changed, still chillin, still waitin for my friend Bob to finish his work.");
                parentSession.SendText("If u meet him, make sure he hurries up. See ya.");
            }
            else
            {
                parentSession.SendText("\nHi there. I'm just chillin u know, waitin for my friend Bob to finish his work.");
                int choice = GetListBoxChoice(new List<string>() { "I feel ya. Can I do something for you though?", "Chili. Enjoy yourself." });
                switch (choice)
                {
                    case 0:
                        parentSession.SendText("Nah dawg. But I appreciate ya. If u meet Bob, make sure he hurries up. See ya.");
                        parentSession.currentPlayer.ListOfQuestsExecutions.Add("MagicBobAppreciates");
                        int choice0 = GetListBoxChoice(new List<string>() { "Sure man, see ya." });
                        if (choice0 == 0)
                        {
                            return;
                        }
                        break;
                    case 1:
                        parentSession.SendText("If u meet him, make sure he hurries up. See ya.");
                        return;
                }
            }
        }
        public void Execute(GameSession parentSession, bool visited)
        {
            if (visited)
            {
                parentSession.SendText("\nHi again. Nothin changed, still chillin, still waitin for my friend Bob to finish his work.");
                parentSession.SendText("If u meet him, make sure he hurries up. See ya.");
            }
            else
            {
                RunContent();
            }
        }
    }
}
