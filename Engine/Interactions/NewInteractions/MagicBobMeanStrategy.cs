using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Monsters;

namespace Game.Engine.Interactions.NewInteractions
{
    [Serializable]
    class MagicBobMeanStrategy : ListBoxInteraction, IMagicBobStrategy
    {
        public MagicBobMeanStrategy(GameSession parentSession) : base(parentSession)
        {
        }
        protected override void RunContent()
        {
            parentSession.SendText("\nYo yo, thanks for speeding Bob's work. He's mean sometimes, don't bother. U want a beer? ");
            int choice = GetListBoxChoice(new List<string>() { "Sure man, thanks!", "How much for it?","No no, I don't drink." });
            if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("MagicBobAppreciates"))
            {
                switch (choice)
                {
                    case 0:
                        parentSession.SendText("Gul gul gul...");
                        int choice0 = GetListBoxChoice(new List<string>() { "I feel weird man." });
                        if (choice0 == 0)
                        {
                            parentSession.SendText("\n* Bob shows up *\nHaha! Pranked ya buddy! You are now twice weaker! Bobby get him!");
                            parentSession.UpdateStatMultiply(2, 0.5);
                            parentSession.FightThisMonster(new AcidBlob(parentSession.currentPlayer.Level));
                            return;
                        }
                        break;
                    case 1:
                        parentSession.SendText("For you it's free. Bob wanted me to prank you, but I like you man. Cheers! ");
                        parentSession.UpdateStat(1, 10);
                        return;
                    case 2:
                        parentSession.SendText("Suit yourself. Bobby, he's all yours.");
                        parentSession.FightThisMonster(new RedBlob(parentSession.currentPlayer.Level));
                        return;
                }
            }
            else
            {
                switch (choice)
                {
                    case 0:
                        parentSession.SendText("Gul gul gul...");
                        int choice0 = GetListBoxChoice(new List<string>() { "I feel weird man." });
                        if (choice0 == 0)
                        {
                            parentSession.SendText("\n* Bob shows up *\nHaha! Pranked ya buddy! You are now twice weaker! Bobby get him!");
                            parentSession.UpdateStatMultiply(2,0.5);
                            parentSession.FightThisMonster(new AcidBlob(parentSession.currentPlayer.Level));
                            parentSession.SendText("\nYou think you're though punk? Bobo finish him!");
                            parentSession.FightThisMonster(new IronElemental(parentSession.currentPlayer.Level));
                            return;
                        }
                        break;
                    case 1:
                        parentSession.SendText("That'll be 15.");
                        int choice1 = GetListBoxChoice(new List<string>() { "Here you are.","15?? You crazy." });
                        if (choice1 == 0)
                        {
                            parentSession.UpdateStat(8, -15);
                            parentSession.SendText("Thank ya.\n* Bob shows up *\nHaha! Pranked ya buddy! You lost money and u gotta fight Bongo!");
                            parentSession.FightThisMonster(new AirElemental(parentSession.currentPlayer.Level));
                            return;
                        }
                        else
                        {
                            parentSession.SendText("Suit yourself. Bobby, he's all yours.");
                            parentSession.FightThisMonster(new AcidBlob(parentSession.currentPlayer.Level));
                            return;
                        }
                    case 2:
                        parentSession.SendText("Suit yourself. Bobby, he's all yours.");
                        parentSession.FightThisMonster(new AcidBlob(parentSession.currentPlayer.Level));
                        return;
                }
            }
        }
        public void Execute(GameSession parentSession, bool visited)
        {
            if (visited)
            {
                parentSession.SendText("\nYou've had enough man. Go home.");
            }
            else
            {
                parentSession.currentPlayer.ListOfQuestsExecutions.Remove("MagicBobMean");
                RunContent();
            }
        }
    }
}
