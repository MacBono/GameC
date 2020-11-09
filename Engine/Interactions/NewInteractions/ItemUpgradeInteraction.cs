using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Skills;
using Game.Engine.Items;

namespace Game.Engine.Interactions
{
    // a special interaction used for learning skills

    [Serializable]
    class ItemUpgradeInteraction : ConsoleInteraction
    {
        private double playerChance;
        private List<Item> items;
        public ItemUpgradeInteraction(GameSession parentSession) : base(parentSession)
        {
            items = new List<Item>() { };
            Name = "interaction1560";
        }
        protected override void RunContent()
        {
            CheckForQuest();
            playerChance = (double)parentSession.currentPlayer.Precision * parentSession.currentPlayer.Level / (parentSession.currentPlayer.Level + 1);
            parentSession.SendText("\nWelcome! You may now try to upgrade your items - you have " + (int)playerChance + "% chance to increase it's statistics twice!!!\nIf you don't succeed, the item will be destroyed.");
            parentSession.SendText("Press I to see your items' stats, U to upgrade items or ENTER to leave.");
            while (true)
            {
                    string key = parentSession.GetValidKeyResponse(new List<string>() { "Return", "I", "U" }).Item1;
                    if (key == "Return") break;
                    else if (key == "I")
                    {
                        parentSession.ListAllItemsTips();
                        parentSession.SendText("Press I to see your items' stats, U to upgrade items or ENTER to leave.");

                    }
                    else
                    {

                        if (parentSession.currentPlayer.Gold >= parentSession.currentPlayer.Level * 0)
                        {
                            parentSession.UpdateStat(8, -1 * parentSession.currentPlayer.Level * 0);

                            parentSession.SendText("\nWhich item would you like to upgrade? ");
                            foreach (string name in parentSession.GetActiveItemNames())
                            {
                                items.Add(Index.ProduceSpecificItem(name));
                            }
                            for (int i = 0; i < parentSession.GetActiveItemNames().Count; i++)
                            {
                                parentSession.SendText((i + 1) + ". " + items[i].PublicName);
                            }
                            while (true)
                            {
                                string key2 = parentSession.GetValidKeyResponse(new List<string>() { "Return", "1", "2", "3", "4", "5" }).Item1;
                                if (key2 == "Return")
                                {
                                    return;
                                }
                                else
                                {
                                    Random rng = new Random();
                                    if (rng.Next(0, 101) <= playerChance)
                                    {
                                        if (key2 == "1") UpgradeItem(items[0]);
                                        else if (key2 == "2") UpgradeItem(items[1]);
                                        else if (key2 == "3") UpgradeItem(items[2]);
                                        else if (key2 == "4") UpgradeItem(items[3]);
                                        else if (key2 == "5") UpgradeItem(items[4]);
                                    }
                                    else
                                    {
                                        parentSession.SendText("You failed. Good luck next time! ");
                                    }

                                }
                            }
                        }
                        else parentSession.SendText("Sorry, you don't have enough gold to upgrade!");
                    }
            }
        }
        protected void CheckForQuest()
        {
            if (parentSession.currentPlayer.ListOfQuests.Contains("BobQuest"))
            {
                if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("BobQuestFreeUpgrade"))
                {
                    parentSession.SendText("\nWelcome! You may now try to upgrade your items - you have " + (int)playerChance + "% chance to increase it's statistics twice!!!\nIf you don't succeed, the item will be destroyed.");
                    parentSession.SendText("Press U to upgrade items or ENTER to leave.");
                    string key = parentSession.GetValidKeyResponse(new List<string>() { "Return", "U" }).Item1;
                    if (key == "Return") return;
                    else
                    {
                        parentSession.SendText("\nBob told me about the quest. First time its free. ");
                        parentSession.SendText("\nWhich item would you like to upgrade? ");
                        foreach (string name in parentSession.GetActiveItemNames())
                        {
                            items.Add(Index.ProduceSpecificItem(name));
                        }
                        for (int i = 0; i < parentSession.GetActiveItemNames().Count; i++)
                        {
                            parentSession.SendText((i + 1) + ". " + items[i].PublicName);
                        }
                        while (true)
                        {
                            string key2 = parentSession.GetValidKeyResponse(new List<string>() { "Return", "1", "2", "3", "4", "5" }).Item1;
                            if (key2 == "Return")
                            {
                                return;
                            }
                            else
                            {
                                parentSession.currentPlayer.ListOfQuestsExecutions.Remove("BobQuestFreeUpgrade");
                                Random rng = new Random();
                                if (rng.Next(0, 101) <= playerChance)
                                {
                                    if (key2 == "1") UpgradeItem(items[0]);
                                    else if (key2 == "2") UpgradeItem(items[1]);
                                    else if (key2 == "3") UpgradeItem(items[2]);
                                    else if (key2 == "4") UpgradeItem(items[3]);
                                    else if (key2 == "5") UpgradeItem(items[4]);
                                }
                                else
                                {
                                    parentSession.SendText("You failed. Good luck next time! ");
                                }
                                return;
                            }
                        }
                    }
                }
                else return;
            }
            else return;
        }
        protected void BobQuestUpgradeStatus()
        {
            if (parentSession.currentPlayer.ListOfQuests.Contains("BobQuest"))
            {
                if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("upgraded"))
                {
                    parentSession.currentPlayer.ListOfQuestsExecutions.Remove("upgraded");
                    parentSession.currentPlayer.ListOfQuestsExecutions.Add("toomuchupgraded");
                }
                else if (parentSession.currentPlayer.ListOfQuestsExecutions.Contains("toomuchupgraded")) { }
                else parentSession.currentPlayer.ListOfQuestsExecutions.Add("upgraded");
            }
        }
        protected void UpgradeItem(Item it)
        {
            it.ModifyMyStats(2);
            BobQuestUpgradeStatus();
            parentSession.SendText("You have successfully upgrade your weapon! All the stats are doubled!!!");
            parentSession.SendText(it.PrintStats());
        }
    }
}

