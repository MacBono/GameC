using System;
using System.Collections.Generic;
using Game.Engine.Skills.BasicWeaponMoves;
using Game.Engine.CharacterClasses;
using Game.Engine.Skills.NewWeaponMoves;

namespace Game.Engine.Skills.SkillFactories
{
    [Serializable]
    class AxeSpellFactory : SkillFactory
    {
        // this factory produces skills for "Axe" type weapons
        public Skill CreateSkill(Player player)
        {
            List<Skill> playerSkills = player.ListOfSkills;
            Skill known = CheckContent(playerSkills); // check what spells from the "Axe" item specialized Spells category are known by the player already
            if (known == null) // no "Axe" spells known - we will return one of them
            {
                AxeCut s1 = new AxeCut();
                CloneAxe s2 = new CloneAxe();
                // only include elligible spells
                List<Skill> tmp = new List<Skill>();
                if (s1.MinimumLevel <= player.Level) tmp.Add(s1); // check level requirements
                if (s2.MinimumLevel <= player.Level) tmp.Add(s2);
                if (tmp.Count == 0) return null;
                return tmp[Index.RNG(0, tmp.Count)]; // use Index.RNG for safe random numbers
            }
            else if (known.decoratedSkill == null) // a "Axe" spell has been already learned, use decorator to create a combo
            {
                AxeCutDecorator s1 = new AxeCutDecorator(known);
                CloneAxeDecorator s2 = new CloneAxeDecorator(known);
                List<Skill> tmp = new List<Skill>();
                if (s1.MinimumLevel <= player.Level) tmp.Add(s1); // check level requirements
                if (s2.MinimumLevel <= player.Level) tmp.Add(s2);
                if (tmp.Count == 0) return null;
                return tmp[Index.RNG(0, tmp.Count)];
            }
            else return null; // a combo of "Axe" spells has been already learned - this factory doesn't offer double combos so we stop here
        }
        private Skill CheckContent(List<Skill> skills) // wrapper method for checking
        {
            foreach (Skill skill in skills)
            {
                if (skill is AxeCut || skill is CloneAxe || skill is AxeCutDecorator || skill is CloneAxeDecorator) return skill;
            }
            return null;
        }

    }
}
