using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.NewWeaponMoves
{
    [Serializable]
    class SharpenSwordDecorator : SkillDecorator
    {
        // decorator for Sharpen Sword
        public SharpenSwordDecorator(Skill skill) : base("Sharpen Sword", 10, 1, skill)
        {
            MinimumLevel = Math.Max(1, skill.MinimumLevel) + 1;
            PublicName = "COMBO - Sharpen Sword: reduce enemy armor by 15 [incised] AND " + decoratedSkill.PublicName.Replace("COMBO: ", "");
            RequiredItem = "Sword";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response = new StatPackage("incised");
            response.ArmorDmg = 15;
            response.CustomText = "You Sharpen your Sword! (enemy armor decreased by 15)";
            List<StatPackage> combo = decoratedSkill.BattleMove(player);
            combo.Add(response);
            return combo;
        }
    }
}
