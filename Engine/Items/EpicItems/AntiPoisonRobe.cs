using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Items.EpicItems
{
    [Serializable]
    class AntiPoisonRobe : Item
    {
        // immunity to "poison" type of damage
        public AntiPoisonRobe() : base("item1560")
        {
            PublicName = "AntiPoison Robe *epic*";
            PublicTip = "Poison immunity";
            GoldValue = 100;
        }
        public override StatPackage ModifyDefensive(StatPackage pack, List<string> otherItems)
        {
            if (pack.DamageType == "poison" )
            {
                pack.HealthDmg = 0;
            }
            return pack;
        }
    }
}
