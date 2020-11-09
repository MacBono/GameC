using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class IronElemental : Monster
    {
        public IronElemental(int elemLevel)
        {
            Health = 30 + 10 * elemLevel;
            Strength = 10 + 2 * elemLevel;
            Armor = 50;
            Precision = 60;
            MagicPower = 20;
            Stamina = 100;
            XPValue = 10 + 3 * elemLevel;
            Name = "monster1566";
            BattleGreetings = "I am the Guardain of Elementals!";
        }
        public override List<StatPackage> BattleMove()
        {
            if (stun == 1)
                return new List<StatPackage>() 
                { 
                    new StatPackage("stab", 0, "Immune to stun."),
                    new StatPackage("stab", 10 + Strength,20, "Iron Elemental punches you!(" + (10 + Strength) + " fist damage).\nYour Armor reduced by 20.")
                };
            else
            {
                if (Stamina > 0)
                {
                    return new List<StatPackage>()
                    {
                        new StatPackage("stab", 10 + Strength,20, "Iron Elemental punches you!(" + (10 + Strength) + " fist damage).\nYour Armor reduced by 20.")
                    };
                }
                else
                {
                    return new List<StatPackage>() { new StatPackage("none", 0, "Iron Elemental has no energy to attack anymore! End it fast...") };
                }
            }
        }
    }
}
