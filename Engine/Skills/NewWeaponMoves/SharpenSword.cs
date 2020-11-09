using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.NewWeaponMoves
{
    [Serializable]
    class SharpenSword : Skill
    {
        // sharpen sword: -15 armor debuff fot the enemy
        public SharpenSword() : base("Sharpen Sword", 10, 1)
        {
            PublicName = "Sharpen Sword: reduce enemy armor by 15 [incised]";
            RequiredItem = "Sword";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response = new StatPackage("incised");
            response.ArmorDmg = 15;
            response.CustomText = "You Sharpen your Sword! (enemy armor decreased by 15)";
            return new List<StatPackage>() { response };
        }
    }
}
