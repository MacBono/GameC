using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.BasicWeaponMoves
{
    [Serializable]
    class SwordSlashDecorator : SkillDecorator
    {
        // decorator for Sword Slash 
        public SwordSlashDecorator(Skill skill) : base("Sword Slash", 10, 1, skill)
        {
            MinimumLevel = Math.Max(1, skill.MinimumLevel) + 1;
            PublicName = "COMBO - Basic sword slash [requires sword]: 0.1*Str + 0.1*Pr damage [stab] and then 0.1*Str + 0.1*Pr damage [incised] AND " + decoratedSkill.PublicName.Replace("COMBO: ", "");
            RequiredItem = "Sword";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response1 = new StatPackage("stab", (int)(0.1 * player.Strength) + (int)(0.1 * player.Precision));
            StatPackage response2 = new StatPackage("incised", (int)(0.1 * player.Strength) + (int)(0.1 * player.Precision), "You use Sword Slash! (" + ((int)(0.1 * player.Strength) + (int)(0.1 * player.Precision)) + " stab damage, " + ((int)(0.1 * player.Strength) + (int)(0.1 * player.Precision)) + " incised damage)");
            List<StatPackage> combo = decoratedSkill.BattleMove(player);
            combo.Add(response1);
            combo.Add(response2);
            return combo;
        }
    }
}
