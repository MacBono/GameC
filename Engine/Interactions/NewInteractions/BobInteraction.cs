using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Skills;
using Game.Engine.Items;
using Game.Engine.Items.EpicItems;
using Game.Engine.Monsters;

namespace Game.Engine.Interactions.NewInteractions
{
    // a special interaction used for learning skills

    [Serializable]
    class BobInteraction : ListBoxInteraction
    {
        private int questType = 0; // depends on the answers player will encounter different results
        private int onQuest = 0;
        public BobInteraction(GameSession parentSession) : base(parentSession)
        {
            Name = "interaction1561";
        }
        protected override void RunContent()
        {
            CheckQuest();
            if (onQuest == 1) // on the quest
            {
                parentSession.SendText("\nHi there, have you completed my challenge? ");
                int choice0 = GetListBoxChoice(new List<string>() { "Sure I did!", "Sorry, not yet." });
                if (choice0 == 0)
                {
                    parentSession.SendText("Let's see...");
                    if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("upgraded"))
                    {
                        parentSession.currentPlayer.ListOfQuests.Remove("BobQuest");
                        parentSession.currentPlayer.ListOfQuestsExecutions.Remove("upgraded");
                        parentSession.currentPlayer.ListOfQuestsExecutions.Add("MagicBobFriendly");
                        if (questType == 1)
                        {
                            parentSession.AddThisItem(new FridaySword());
                            parentSession.SendText("\nGood job! Here is your reward! \nYou obtained an *epic* sword!!!");
                            return;
                        }
                        else
                        {
                            parentSession.AddThisItem(new BasicSword());
                            parentSession.SendText("\nGood job! Here is your reward! \nYou obtained a regular sword...");
                            return;
                        }
                    }
                    else if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("toomuchupgraded"))
                    {
                        parentSession.currentPlayer.ListOfQuests.Remove("BobQuest");
                        parentSession.currentPlayer.ListOfQuestsExecutions.Remove("toomuchupgraded");
                        parentSession.currentPlayer.ListOfQuestsExecutions.Add("MagicBobFriendly");
                        parentSession.SendText("\nYou were too greedy! Your reward is a fight!");
                        if (questType == 1)
                        {
                            parentSession.FightThisMonster(new RedBlob(parentSession.currentPlayer.Level));
                        }
                        else
                        {
                            parentSession.FightThisMonster(new EarthElemental(parentSession.currentPlayer.Level));
                        }
                        parentSession.SendText("\nHaha pranked ya. My new hobby with my homie Magic Bob. You should check him out.");
                        return;
                    }
                    parentSession.SendText("\nYou liar. Go away!");
                    parentSession.currentPlayer.ListOfQuests.Remove("BobQuest");
                    parentSession.currentPlayer.ListOfQuestsExecutions.Add("MagicBobMean");
                    return;
                }
                else parentSession.SendText("Well come back when you do.");
                return;
            }
            if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("MagicBobMean")) // negative visit
            {
                parentSession.SendText("\nOh, you again. I no longer do challenges. I moved to pranks now. Doing them with my homie Magic Bob. He ain't a liar like you.");
                return;
            }
            if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("MagicBobFriendly")) // already visited this place more than two times
            {
                parentSession.SendText("\nOh, hello. Thanks for coming, but I no longer do challenges. I moved to pranks now. Doing them with my homie Magic Bob. You should look for him ;)");
                return;
            }
            // standard encounter
            parentSession.SendText("\nHello adventurer. My name is Bob. Do you want to face a challenge?");
            // get player choice
            int choice = GetListBoxChoice(new List<string>() { "Oh yea, what is it?", "What will I get from it?", "Yea, sorry." });
            switch (choice)
            {
                case 0:
                    parentSession.SendText("I would like you to successuly upgrade one of your weapons. But only ONCE!! Otherwise, it will be useless to me.");
                    int choice0 = GetListBoxChoice(new List<string>() { "Sure, I'm on it.", "Sorry, not this time." });
                    if (choice0 == 0)
                    {
                        parentSession.currentPlayer.ListOfQuests.Add("BobQuest");
                        parentSession.SendText("You will have the first try free. Good luck.");
                        parentSession.currentPlayer.ListOfQuestsExecutions.Add("BobQuestFreeUpgrade");
                        questType = 1;
                    }
                    else parentSession.SendText("Well come back, maybe next time.");
                    break;
                case 1:
                    parentSession.SendText("I will reward you with an epic sword.");
                    int choice1 = GetListBoxChoice(new List<string>() { "Alrighty. What's the task?", "Sorry, that's not good enough." });
                    if (choice1 == 0)
                    {
                        parentSession.SendText("I would like you to successuly upgrade one of your weapons. But only ONCE!! Otherwise, it will be useless to me.");
                        int choice1_1 = GetListBoxChoice(new List<string>() { "Sure, I'm on it.", "Sorry, not this time." });
                        if (choice1_1 == 0)
                        {
                            parentSession.currentPlayer.ListOfQuests.Add("BobQuest");
                            parentSession.SendText("Good luck.");
                            questType = 2;
                        }
                        else parentSession.SendText("Well come back, maybe next time.");
                    }
                    else parentSession.SendText("People these days... go away then!");
                    break;
                default:
                    parentSession.SendText("Go away then!");
                    break;
            }
        }
        public void CheckQuest()
        {
            if (parentSession.currentPlayer.ListOfQuests.Contains("BobQuest")) onQuest = 1;
            else onQuest = 0;
        }
    }
}

