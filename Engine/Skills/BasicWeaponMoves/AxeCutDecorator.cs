using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.BasicWeaponMoves
{
    [Serializable]
    class AxeCutDecorator : SkillDecorator
    {
        // decorator for Axe Cut
        public AxeCutDecorator(Skill skill) : base("Axe Cut", 10, 1, skill)
        {
            MinimumLevel = Math.Max(1, skill.MinimumLevel) + 1;
            PublicName = "COMBO - Basic axe cut [requires axe]: 0.4*Str + 0.1*Pr damage [incised] AND " + decoratedSkill.PublicName.Replace("COMBO: ", "");
            RequiredItem = "Axe";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response = new StatPackage("incised");
            response.HealthDmg = (int)(0.4 * player.Strength) + (int)(0.1 * player.Precision);
            response.CustomText = "You use Axe Cut! (" + ((int)(0.4 * player.Strength) + (int)(0.1 * player.Precision)) + " incised damage)";
            List<StatPackage> combo = decoratedSkill.BattleMove(player);
            combo.Add(response);
            return combo;
        }
    }
}
