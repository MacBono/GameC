using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Items.EpicItems
{
    [Serializable]
    class BurnProtectionAmulet : Item
    {
        // immunity to "burn" type damage and extra magic power
        public BurnProtectionAmulet() : base("item1561")
        {
            PublicName = "Burn-Protection Amulet *epic*";
            PublicTip = "Burn immunity. +10 to Magic Power";
            GoldValue = 80;
            MgcMod = 10;
        }
        public override StatPackage ModifyDefensive(StatPackage pack, List<string> otherItems)
        {
            if (pack.DamageType == "burn")
            {
                pack.HealthDmg = 0;
            }
            return pack;
        }
    }
}
