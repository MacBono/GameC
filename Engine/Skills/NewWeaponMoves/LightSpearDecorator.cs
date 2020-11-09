using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.NewWeaponMoves
{
    [Serializable]
    class LightSpearDecorator : SkillDecorator
    {
        // decorator for Light Spear
        public LightSpearDecorator(Skill skill) : base("Spear Stab", 10, 1, skill)
        {
            MinimumLevel = Math.Max(1, skill.MinimumLevel) + 1;
            PublicName = "COMBO - Light your Spear: deal 15 burn damage AND " + decoratedSkill.PublicName.Replace("COMBO: ", "");
            RequiredItem = "Spear";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response = new StatPackage("burn");
            response.HealthDmg = 15;
            response.CustomText = "You light your spear! (" + 15 + " burn damage)";
            List<StatPackage> combo = decoratedSkill.BattleMove(player);
            combo.Add(response);
            return combo;
        }
    }
}
