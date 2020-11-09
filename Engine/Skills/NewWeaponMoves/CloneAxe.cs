using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.NewWeaponMoves
{
    [Serializable]
    class CloneAxe : Skill
    {
        // Clone Axe:  another attack - the basic Axe Cut
        public CloneAxe() : base("Clone Axe", 10, 1)
        {
            PublicName = "Clone Axe: make the basic Axe Cut";
            RequiredItem = "Axe";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response = new StatPackage("incised");
            response.HealthDmg = (int)(0.4 * player.Strength) + (int)(0.1 * player.Precision);
            response.CustomText = "You use Axe Cut! (" + ((int)(0.4 * player.Strength) + (int)(0.1 * player.Precision)) + " incised damage)";
            return new List<StatPackage>() { response};
        }
    }
}
