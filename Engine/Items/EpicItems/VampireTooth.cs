using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Items.EpicItems
{
    [Serializable]
    class VampireTooth : Spear
    {
        // Vampire Tooth - grants +10 precision, +10 strength and gives 25% chance to heal for half the damgae dealt
        private int life;
        public VampireTooth() : base("item1563")
        {
            PrMod = 10;
            StrMod = 10;
            GoldValue = 70;
            PublicName = "Vampire Tooth *epic*";
            PublicTip = "25% chance to drain life(half the attack value)";
        }
        public override StatPackage ModifyOffensive(StatPackage pack, List<string> otherItems)
        {
            Random rng = new Random();
            if (rng.Next(1, 5) < 2)
            {
                life = pack.HealthDmg / 2;
                pack.CustomText += "\nYou leech life! You heal for " + life;
            }
            else
                life = 0;
            return pack;
        }
        public override void ApplyBuffs(Engine.CharacterClasses.Player currentPlayer, List<string> otherItems)
        {
            currentPlayer.HealthBuff += life;
            currentPlayer.PrecisionBuff += PrMod;
            currentPlayer.StrengthBuff += StrMod;
        }
    }
}
