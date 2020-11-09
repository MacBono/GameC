using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Game.Engine.Monsters
{
    [Serializable]
    public abstract class Monster : Subject
    {
        // abstract class representing a monster
        protected int stun;
        public int XPValue { get; protected set; }
        public string BattleGreetings { get; protected set; } // what the monster says when it attacks the player for the first time
        public abstract List<StatPackage> BattleMove(); // perform an action in the battle
        public virtual void React(List<StatPackage> packs) // receive the result of your opponent's action
        {
            foreach (StatPackage pack in packs)
            {
                if (pack.DamageType == "fire" || pack.DamageType == "water" || pack.DamageType == "air" || pack.DamageType == "earth")
                {
                    if (pack.HealthDmg < MagicPower / 2)
                        Health -= 0;
                    else
                        Health -= pack.HealthDmg - MagicPower / 2;
                }
                else if (pack.DamageType == "incised" || pack.DamageType == "stab")
                {
                    if (pack.HealthDmg < Armor / 10)
                        Health -= 0;
                    else
                        Health -= pack.HealthDmg - Armor / 10;
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
    }
}
