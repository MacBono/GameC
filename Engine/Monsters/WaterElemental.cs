using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class WaterElemental : Monster
    {
        private int turn;
        private int lvl;
        private string info;
        public WaterElemental(int elemLevel)
        {
            Health = 30 + 10 * elemLevel;
            turn = 0;
            lvl = elemLevel;
            Strength = 0;
            Armor = 40;
            Precision = 60;
            MagicPower = 10 + 2 * elemLevel;
            Stamina = 100;
            XPValue = 10 + 3 * elemLevel;
            Name = "monster1564";
            BattleGreetings = "I am the Queen of Seas!!!";
        }
        public override void React(List<StatPackage> packs) // receive the result of your opponent's action
        {
            foreach (StatPackage pack in packs)
            {
                if (pack.DamageType == "water")
                {
                    info = "This unit is immune to it's element's type of damage.";
                }
                else if (pack.DamageType == "fire" || pack.DamageType == "air" || pack.DamageType == "earth")
                {
                    if (pack.HealthDmg < MagicPower / 2)
                        Health -= 0;
                    else
                        Health -= Math.Abs(pack.HealthDmg - MagicPower / 2);
                }
                else if (pack.DamageType == "incised" || pack.DamageType == "stab")
                {
                    if (pack.HealthDmg < Armor / 10)
                        Health -= 0;
                    else
                        Health -= Math.Abs(pack.HealthDmg - Armor / 10);
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
                        Health += 10 * lvl;
                        return new List<StatPackage>()
                    {
                        new StatPackage("stab", 0, "The Water Elemental creates a fountain. She heals for " + 10*lvl)
                    };

                    }
                    else
                    {
                        Stamina -= 10;
                        return new List<StatPackage>()
                    {
                        new StatPackage("water", 15 + MagicPower, "Water Elemental uses Water Burst! (" + (15 + MagicPower) + " magic damage)"),
                    };
                    }
                }
                else
                {
                    return new List<StatPackage>() { new StatPackage("none", 0, "Water Elemental has no energy to attack anymore! End it fast...") };
                }
            }
        }
    }
}
