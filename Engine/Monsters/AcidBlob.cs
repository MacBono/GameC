using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class AcidBlob : Monster
    {
        private int healthCPy;
        private int half;
        public AcidBlob(int blobLevel)
        {
            Health = 50 + 10 * blobLevel;
            healthCPy = Health;
            Strength = 10 + 3 * blobLevel;
            Armor = 40;
            Precision = 60;
            MagicPower = 0;
            Stamina = 150;
            XPValue = 20 + 4 * blobLevel;
            Name = "monster1561";
            BattleGreetings = "I am the result of an unhealthy diet!!";
        }
        public override List<StatPackage> BattleMove()
        {
            if (stun == 1)
                return new List<StatPackage>() { new StatPackage("stab", 0, "Enemy stunned. Can't attack.") };
            else
            {
                if (Stamina > 0)
                {
                    if (Health < healthCPy / 2 && half == 0)
                    {
                        Armor += 20;
                        half = 1;
                        return new List<StatPackage>()
                    {
                        new StatPackage("stab", 0, "You enraged the Blob! It burps to increase his Armor (by 20) and gathers bile for next attack!!!"),
                        new StatPackage("stab", 10 + Strength, (Precision + Strength) / 10, "Acid Blob uses Giga Burf! (" + (10 + Strength) + " damage)"),
                        new StatPackage("poison", 15, "Bile burns your body (15 poison damage)")
                    };

                    }
                    else
                    {
                        Stamina -= 15;
                        return new List<StatPackage>()
                    {
                        new StatPackage("stab", 10 + Strength, (Precision + Strength) / 10, "Acid Blob uses Big Burf! (" + (10 + Strength) + " damage)"),
                        new StatPackage("poison", 7, "Bile burns your body (7 poison damage)")
                    };
                    }
                }
                else
                {
                    return new List<StatPackage>() { new StatPackage("none", 0, "Blob has no energy to attack anymore! End it fast...") };
                }
            }
        }
    }
}
