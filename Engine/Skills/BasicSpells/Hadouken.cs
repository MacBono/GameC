using System;
using System.Collections.Generic;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Skills.BasicSkills
{
    [Serializable]
    class Hadouken : Skill
    {
        // It is a basic spell for Mage. It doesn't require any weapon and cannot be forgot
        // Hadouken: 150%[Pr] chance to land a ball of energy that deals 0.5*[Mp] damage
        // if your precision stat is higher than 100, you will always land Hadouken
        public Hadouken() : base("Hadouken", 10, 1)
        {
            PublicName = "Hadouken: a chance equal to your Precision stat to land 0.15*MP damage [air]";
            RequiredItem = "none";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response = new StatPackage("fire");
            Random rnd = new Random();
            if (rnd.Next(0, 100) < (int)1.5*player.Precision)
            {
                response.HealthDmg = (int)(7 * player.Level);
                response.CustomText = "You use Hadouken! (" + (int)(7 * player.Level) + " air damage)";
            }
            else
            {
                response.HealthDmg = 0;
                response.CustomText = "You try to use Hadouken but it misses!";
            }
            return new List<StatPackage>() { response };
        }
    }
}
