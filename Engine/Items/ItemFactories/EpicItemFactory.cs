using System;
using System.Collections.Generic;
using Game.Engine.Items.BasicArmor;
using Game.Engine.Items.EpicItems;


namespace Game.Engine.Items.ItemFactories
{
    [Serializable]
    class EpicItemFactory : ItemFactory
    {
        // this factory generates *epic* items - these are the one's I have added
        public Item CreateItem()
        {
            List<Item> basicArmor = new List<Item>()
            {
                new AntiPoisonRobe(),
                new BurnProtectionAmulet(),
                new VampireTooth(),
                new FridaySword()
            };
            return basicArmor[Index.RNG(0, basicArmor.Count)];
        }
        public Item CreateNonMagicItem()
        {
            List<Item> basicArmor = new List<Item>()
            {
                new AntiPoisonRobe(),
                new VampireTooth(),
                new FridaySword()
            };
            return basicArmor[Index.RNG(0, basicArmor.Count)];
        }
        public Item CreateNonWeaponItem()
        {
            List<Item> basicArmor = new List<Item>()
            {
                new AntiPoisonRobe(),
            };
            return basicArmor[Index.RNG(0, basicArmor.Count)];
        }
    }
}
