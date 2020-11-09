using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.BasicWeaponMoves
{
    [Serializable]
    class Punch : Skill
    {
        // It is a basic spell for Warrior. It doesn't require any weapon and cannot be forgot
        // Punch: deals 5*PlayerLevel damage
        public Punch() : base("Punch", 5, 1)
        {
            PublicName = "Simple punch: 5*PlayerLevel damage";
            RequiredItem = "none";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response = new StatPackage("stab");
            response.HealthDmg = (5 * player.Level);
            response.CustomText = "You use Punch! (" + (5 * player.Level) + " damage)";
            return new List<StatPackage>() { response };
        }
    }
}
