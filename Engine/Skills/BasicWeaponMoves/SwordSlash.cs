using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.BasicWeaponMoves
{
    [Serializable]
    class SwordSlash : Skill
    {
        // simple slash with sword
        public SwordSlash() : base("Sword Slash", 20, 1)
        {
            PublicName = "Basic sword slash [requires sword]: 0.1*Str + 0.1*Pr damage [stab] and then 0.1*Str + 0.1*Pr damage [incised]";
            RequiredItem = "Sword";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response1 = new StatPackage("stab", (int)(0.1 * player.Strength) + (int)(0.1 * player.Precision));
            StatPackage response2 = new StatPackage("incised", (int)(0.1 * player.Strength) + (int)(0.1 * player.Precision), "You use Sword Slash! (" + ((int)(0.1 * player.Strength) + (int)(0.1 * player.Precision)) + " stab damage, " + ((int)(0.1 * player.Strength) + (int)(0.1 * player.Precision)) + " incised damage)");
            return new List<StatPackage>() { response1, response2 };
        }
    }
}
