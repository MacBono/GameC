using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine
{
    // wrapper class for a bunch of statistics
    // meant to be used during battles and interactions

    // predefined damage types:
    // PHYSICAL DAMAGE - stab, incised (po polsku: rany kłute i cięte)
    // MAGIC DAMAGE - fire, air, water, earth
    // OTHERS - poison, *new* burn
    public class StatPackage
    {
        public int HealthDmg { get; set; }
        public int StrengthDmg { get; set; }
        public int ArmorDmg { get; set; }
        public int PrecisionDmg { get; set; }
        public int MagicPowerDmg { get; set; }
        public int Stun { get; set; }
        public string DamageType { get; set; }
        public string CustomText { get; set; }
        public StatPackage(string dmgType)
        {
            DamageType = dmgType;
        }
        public StatPackage(string dmgType, int hp)
        {
            DamageType = dmgType;
            HealthDmg = hp;
        }
        public StatPackage(string dmgType, int hp, string text)
        {
            DamageType = dmgType;
            HealthDmg = hp;
            CustomText = text;
        }
        public StatPackage(string dmgType, int hp, int armor, string text)
        {
            DamageType = dmgType;
            HealthDmg = hp;
            ArmorDmg = armor;
            CustomText = text;
        }
        public StatPackage(string dmgType, int hp, int strength, int armor, int precision, int magic, string text)
        {
            DamageType = dmgType;
            HealthDmg = hp;
            StrengthDmg = strength;
            ArmorDmg = armor;
            PrecisionDmg = precision;
            MagicPowerDmg = magic;
            CustomText = text;
        }
        
    }
}
