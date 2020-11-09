using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Monsters;

namespace Game.Engine.Interactions.NewInteractions
{
    [Serializable]
    class MagicBobFriendlyStrategy : ListBoxInteraction, IMagicBobStrategy
    {
        public MagicBobFriendlyStrategy(GameSession parentSession) : base(parentSession)
        {
        }
        protected override void RunContent()
        {
            parentSession.SendText("\nYo yo, thanks for speeding Bob's work. U want a beer? ");
            int choice = GetListBoxChoice(new List<string>() { "Sure man, thanks!", "How much for it?", "No no, I don't drink." });
            if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("MagicBobAppreciates"))
            {
                switch (choice)
                {
                    case 0:
                        parentSession.SendText("Gul gul gul...");
                        int choice0 = GetListBoxChoice(new List<string>() { "Aaaa. Thats what I needed!" });
                        if (choice0 == 0)
                        {
                            parentSession.SendText("Cheers! Your HP increased by 20!");
                            parentSession.UpdateStat(1,20);
                            parentSession.currentPlayer.ListOfQuestsExecutions.Remove("MagicBobAppreciates");
                            return;
                        }
                        break;
                    case 1:
                        parentSession.SendText("For you it's free. Cheers! \nYour Strength and Magic Power increased by 20!");
                        parentSession.UpdateStat(2, 20);
                        parentSession.UpdateStat(5, 20);
                        parentSession.currentPlayer.ListOfQuestsExecutions.Remove("MagicBobAppreciates");
                        return;
                    case 2:
                        parentSession.SendText("Suit yourself. It will wait for ya.");
                        parentSession.currentPlayer.ListOfQuestsExecutions.Add("MagicBobBeer");
                        return;
                }
            }
            else
            {
                switch (choice)
                {
                    case 0:
                        parentSession.SendText("Gul gul gul...");
                        int choice0 = GetListBoxChoice(new List<string>() { "Aaaa. Thats what I needed!" });
                        if (choice0 == 0)
                        {
                            parentSession.SendText("Cheers! Your HP increased by 10!");
                            parentSession.UpdateStat(1, 20);
                            return;
                        }
                        break;
                    case 1:
                        parentSession.SendText("For you it's free. Cheers! \nYour Strength and Magic Power increased by 20!");
                        parentSession.UpdateStat(2, 10);
                        parentSession.UpdateStat(5, 10);
                        return;
                    case 2:
                        parentSession.SendText("Suit yourself. I'll drink it myself. Cheers!");
                        return;
                }
            }
        }
        public void Execute(GameSession parentSession, bool visited)
        {
            if (visited)
            {
                if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("MagicBobBeer"))
                {
                    parentSession.currentPlayer.ListOfQuestsExecutions.Remove("MagicBobBeer");
                    RunContent();
                }
                else { parentSession.SendText("\nNothing more from me man. See ya."); }
            }
            else
            {
                parentSession.currentPlayer.ListOfQuestsExecutions.Remove("MagicBobFriendly");
                RunContent();
            }
        }
    }
}
