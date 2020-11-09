using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class FireElemental : Monster
    {
        private int turn;
        private string info;
        public FireElemental(int elemLevel)
        {
            Health = 30 + 10 * elemLevel;
            turn = 0;
            Strength = 0;
            Armor = 50;
            Precision = 60;
            MagicPower = 10 + 2 * elemLevel;
            Stamina = 100;
            XPValue = 10 + 3 * elemLevel;
            Name = "monster1563";
            BattleGreetings = "You shall burn!!!";
        }
        public override void React(List<StatPackage> packs) // receive the result of your opponent's action
        {
            foreach (StatPackage pack in packs)
            {
                if (pack.DamageType == "fire" || pack.DamageType == "burn")
                {
                    info = "This unit is immune to it's element's type of damage.";
                }
                else if ( pack.DamageType == "water" || pack.DamageType == "air" || pack.DamageType == "earth")
                {
                    if (pack.HealthDmg < MagicPower / 2)
                        Health -= 0;
                    else
                        Health -= (pack.HealthDmg - MagicPower / 2);
                }
                else if (pack.DamageType == "incised" || pack.DamageType == "stab")
                {
                    if (pack.HealthDmg < Armor / 10)
                        Health -= 0;
                    else
                        Health -= (pack.HealthDmg - Armor / 10);
                }
                else
                {
                    Health -= pack.HealthDmg;
                }
                Strength -= pack.StrengthDmg;
                Armor -= pack.ArmorDmg;
                Precision -= pack.PrecisionDmg;
                MagicPower -= pack.MagicPowerDmg;
                stun = pack.Stun;

            }
        }
        public override List<StatPackage> BattleMove()
        {
            if (info != null)
                return new List<StatPackage>() { new StatPackage("stab", 0, info) };
            if (stun == 1)
                return new List<StatPackage>() { new StatPackage("stab", 0, "Enemy stunned. Can't attack.") };
            else
                {
                    turn++;
                    if (Stamina > 0)
                    {
                        if (turn % 3 == 0)
                        {
                            MagicPower += 10;
                            return new List<StatPackage>()
                    {
                        new StatPackage("stab", 0, "The Fire Elemental Explodes. He increases his Magic Power (by 20)!"),
                    };

                        }
                        else
                        {
                            Stamina -= 10;
                            return new List<StatPackage>()
                    {
                        new StatPackage("fire", 15 + MagicPower, "Fire Elemental uses Flame! (" + (15 + MagicPower) + " magic damage)"),
                        new StatPackage("burn", MagicPower, "You are burning! ("+MagicPower+" piercing burn damage)")
                    };
                        }
                    }
                    else
                    {
                        return new List<StatPackage>() { new StatPackage("none", 0, "Fire Elemental has no energy to attack anymore! End it fast...") };
                    }
            }
        }
    }
}
