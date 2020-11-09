using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class RedBlob : Monster
    {
        public RedBlob(int blobLevel)
        {
            Health = 10 + 10 * blobLevel;
            Strength = 10 + 2*blobLevel;
            Armor = 10;
            Precision = 60;
            MagicPower = 0;
            Stamina = 100;
            XPValue = 10 + 2*blobLevel;
            Name = "monster1560";
            BattleGreetings = "Bulg! Bulg!"; 
        }
        public override List<StatPackage> BattleMove()
        {
            if (stun == 1)
                return new List<StatPackage>() { new StatPackage("stab", 0, "Enemy stunned. Can't attack.") };
            else
            {
                if (Stamina > 0)
                {
                    Stamina -= 10;
                    return new List<StatPackage>() { new StatPackage("stab", 10 + Strength, (Precision + Strength) / 10, "Red Blob uses Burf! (" + (10 + Strength) + " damage)") };
                }
                else
                {
                    return new List<StatPackage>() { new StatPackage("none", 0, "Blob has no energy to attack anymore! End it fast...") };
                }
            }
        }
    }
}
