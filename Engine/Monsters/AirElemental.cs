using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class AirElemental: Monster
    {
        private int turn;
        private string info;
        public AirElemental(int elemLevel)
        {
            Health = 30 + 10 * elemLevel;
            turn = 0;
            Strength = 10 + 3 * elemLevel;
            Armor = 50;
            Precision = 60;
            MagicPower = 10 + 2 * elemLevel;
            Stamina = 100;
            XPValue = 10 + 3 * elemLevel;
            Name = "monster1562";
            BattleGreetings = "All the winds are under my control!!!";
        }
        public override void React(List<StatPackage> packs) // receive the result of your opponent's action
        {
            foreach (StatPackage pack in packs)
            {
                if (pack.DamageType == "air")
                {
                    info = "This unit is immune to it's element's type of damage.";
                }
                else if (pack.DamageType == "water" || pack.DamageType == "fire" || pack.DamageType == "earth")
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
                        Armor += 20;
                        return new List<StatPackage>()
                    {
                        new StatPackage("stab", 0, "The Air Elemental speeds the winds. He increases his Armor (by 40)!"),
                    };

                    }
                    else
                    {
                        Stamina -= 10;
                        return new List<StatPackage>()
                    {
                        new StatPackage("air", 10 + Strength + MagicPower, (Precision + Strength) / 10, "Air Elemental uses Breeze! (" + (10 + Strength + MagicPower) + " magic damage).\nYour Armor reduced by "+(Precision + Strength) / 10)
                    };
                    }
                }
                else
                {
                    return new List<StatPackage>() { new StatPackage("none", 0, "Air Elemental has no energy to attack anymore! End it fast...") };
                }
            }
        }
    }
}
