using System;
using System.Collections.Generic;
using Game.Engine.Skills.BasicWeaponMoves;
using Game.Engine.CharacterClasses;
using Game.Engine.Skills.NewWeaponMoves;

namespace Game.Engine.Skills.SkillFactories
{
    [Serializable]
    class SpearSpellFactory : SkillFactory
    {
        // this factory produces skills for "Spear" type weapons
        public Skill CreateSkill(Player player)
        {
            List<Skill> playerSkills = player.ListOfSkills;
            Skill known = CheckContent(playerSkills); // check what spells from the "Spear" item specialized Spells category are known by the player already
            if (known == null) // no "Spear" spells known - we will return one of them
            {
                SpearStab s1 = new SpearStab();
                LightSpear s2 = new LightSpear();
                // only include elligible spells
                List<Skill> tmp = new List<Skill>();
                if (s1.MinimumLevel <= player.Level) tmp.Add(s1); // check level requirements
                if (s2.MinimumLevel <= player.Level) tmp.Add(s2);
                if (tmp.Count == 0) return null;
                return tmp[Index.RNG(0, tmp.Count)]; // use Index.RNG for safe random numbers
            }
            else if (known.decoratedSkill == null) // a "Spear" spell has been already learned, use decorator to create a combo
            {
                SpearStabDecorator s1 = new SpearStabDecorator(known);
                LightSpearDecorator s2 = new LightSpearDecorator(known);
                List<Skill> tmp = new List<Skill>();
                if (s1.MinimumLevel <= player.Level) tmp.Add(s1); // check level requirements
                if (s2.MinimumLevel <= player.Level) tmp.Add(s2);
                if (tmp.Count == 0) return null;
                return tmp[Index.RNG(0, tmp.Count)];
            }
            else // although a combo of "Spear" spells has been already learned - this factory offers double combos 
            {
                SpearStabDecorator s1 = new SpearStabDecorator(known);
                LightSpearDecorator s2 = new LightSpearDecorator(known);
                List<Skill> tmp = new List<Skill>();
                if (s1.MinimumLevel <= player.Level) tmp.Add(s1); // check level requirements
                if (s2.MinimumLevel <= player.Level) tmp.Add(s2);
                if (tmp.Count == 0) return null;
                return tmp[Index.RNG(0, tmp.Count)];
            } 
        }
        private Skill CheckContent(List<Skill> skills) // wrapper method for checking
        {
            foreach (Skill skill in skills)
            {
                if (skill is LightSpear || skill is SpearStab || skill is SpearStabDecorator || skill is LightSpearDecorator) return skill;
            }
            return null;
        }

    }
}
