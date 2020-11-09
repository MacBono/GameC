using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.NewWeaponMoves
{
    [Serializable]
    class LightSpear : Skill
    {
        // Light Spear: additional attack dealing burn damage
        public LightSpear() : base("Light your Spear", 10, 1)
        {
            PublicName = "Light your Spear: deal 15 burn damage";
            RequiredItem = "Spear";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response = new StatPackage("burn");
            response.HealthDmg = 15;
            response.CustomText = "You light your spear! (" +15 + " burn damage)";
            return new List<StatPackage>() { response };
        }
    }
}
