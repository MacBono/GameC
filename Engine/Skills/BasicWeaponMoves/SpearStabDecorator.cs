using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.BasicWeaponMoves
{
    [Serializable]
    class SpearStabDecorator : SkillDecorator
    {
        // decorator for Spear Stab
        public SpearStabDecorator(Skill skill) : base("Spear Stab", 10, 1, skill)
        {
            MinimumLevel = Math.Max(1, skill.MinimumLevel) + 1;
            PublicName = "COMBO - Basic spear stab [requires spear]: 0.2*Str + 0.3*Pr damage [stab] AND " + decoratedSkill.PublicName.Replace("COMBO: ", "");
            RequiredItem = "Spear";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response = new StatPackage("stab");
            response.HealthDmg = (int)(0.2 * player.Strength) + (int)(0.3 * player.Precision);
            response.CustomText = "You use Spear Stab! (" + ((int)(0.2 * player.Strength) + (int)(0.3 * player.Precision)) + " stab damage)";
            List<StatPackage> combo = decoratedSkill.BattleMove(player);
            combo.Add(response);
            return combo;
        }
    }
}
