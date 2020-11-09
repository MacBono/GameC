using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class EarthElemental : Monster
    {
        private int turn;
        private string info;
        public EarthElemental(int elemLevel)
        {
            Health = 30 + 10 * elemLevel;
            turn = 0;
            Strength = 10 + 2 * elemLevel;
            Armor = 50;
            Precision = 60;
            MagicPower = 10 + 2 * elemLevel;
            Stamina = 100;
            XPValue = 10 + 3 * elemLevel;
            Name = "monster1565";
            BattleGreetings = "I am the Rock!";
        }
        public override void React(List<StatPackage> packs) // receive the result of your opponent's action
        {
            foreach (StatPackage pack in packs)
            {
                if (pack.DamageType == "earth")
                {
                    info = "This unit is immune to it's element's type of damage.";
                }
                else if (pack.DamageType == "water" || pack.DamageType == "air" || pack.DamageType == "fire")
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
                        return new List<StatPackage>()
                    {
                        new StatPackage("earth", 10 + MagicPower + Strength, "Earth Elemental speeds up! He uses Rigth Hook! (" + (15 + MagicPower + Strength) + " magic damage)"),
                        new StatPackage("earth", 10 + MagicPower + Strength, "And finishes you with a Left Jab! (" + (15 + MagicPower + Strength) + " magic damage)")
                    };

                    }
                    else
                    {
                        Stamina -= 10;
                        return new List<StatPackage>()
                    {
                        new StatPackage("earth", 10 + MagicPower + Strength, "Earth Elemental uses Rock Punch! (" + (15 + MagicPower + Strength) + " magic damage)"),
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
