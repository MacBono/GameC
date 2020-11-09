using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Items.EpicItems
{
    [Serializable]
    class FridaySword : Sword
    {
        // Michael Friday's Sword - enables the wearer to have 50% chance to stun the enemy with any attack, while the sword is equipped
        public FridaySword() : base("item1562")
        {
            PrMod = 20;
            StrMod = 20;
            GoldValue = 150;
            PublicName = "Friday Sword *epic*";
            PublicTip = "Koooooooox.Kooooooooooooox.50% chance to freeze the enemy. He won't be able to attack!";
        }
        public override StatPackage ModifyOffensive(StatPackage pack, List<string> otherItems)
        {
            Random rng = new Random();
            if (rng.Next(1, 3) < 2)
            {
                pack.Stun = 1;
            }
            else
                pack.Stun = 0;
            return pack;
        }
    }
}
