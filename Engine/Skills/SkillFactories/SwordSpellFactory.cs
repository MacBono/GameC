using System;
using System.Collections.Generic;
using Game.Engine.Skills;
using Game.Engine.CharacterClasses;
using Game.Engine.Skills.NewWeaponMoves;
using Game.Engine.Skills.BasicWeaponMoves;


namespace Game.Engine.Skills.SkillFactories
{
    [Serializable]
    class SwordSpellFactory : SkillFactory
    {
        // this factory produces skills for "Sword" type weapons
        public Skill CreateSkill(Player player)
        {
            List<Skill> playerSkills = player.ListOfSkills;
            Skill known = CheckContent(playerSkills); // check what spells from the "Sword" item specialized Spells category are known by the player already
            if (known == null) // no "Sword" spells known - we will return one of them
            {
                SwordSlash s1 = new SwordSlash();
                SharpenSword s2 = new SharpenSword();
                // only include elligible spells
                List<Skill> tmp = new List<Skill>();
                if (s1.MinimumLevel <= player.Level) tmp.Add(s1); // check level requirements
                if (s2.MinimumLevel <= player.Level) tmp.Add(s2);
                if (tmp.Count == 0) return null;
                return tmp[Index.RNG(0, tmp.Count)]; // use Index.RNG for safe random numbers
            }
            else if (known.decoratedSkill == null) // a "Sword" spell has been already learned, use decorator to create a combo
            {
                SwordSlashDecorator s1 = new SwordSlashDecorator(known);
                SharpenSwordDecorator s2 = new SharpenSwordDecorator(known);
                List<Skill> tmp = new List<Skill>();
                if (s1.MinimumLevel <= player.Level) tmp.Add(s1); // check level requirements
                if (s2.MinimumLevel <= player.Level) tmp.Add(s2);
                if (tmp.Count == 0) return null;
                return tmp[Index.RNG(0, tmp.Count)];
            }
            else // although a combo of "Sword" spells has been learned - this factory offers double combos 
            {
                SwordSlashDecorator s1 = new SwordSlashDecorator(known);
                SharpenSwordDecorator s2 = new SharpenSwordDecorator(known);
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
                if (skill is SwordSlash || skill is SharpenSword || skill is SwordSlashDecorator || skill is SharpenSwordDecorator) return skill;
            }
            return null;
        }

    }
}
