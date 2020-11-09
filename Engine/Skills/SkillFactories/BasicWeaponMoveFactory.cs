using System;
using System.Collections.Generic;
using Game.Engine.Skills.BasicWeaponMoves;
using Game.Engine.CharacterClasses;
using Game.Engine.Skills.NewWeaponMoves;

namespace Game.Engine.Skills.SkillFactories
{
    [Serializable]
    class BasicWeaponMoveFactory : SkillFactory
    {
        // this factory produces skills from BasicWeaponMoves directory
        // note: since every skill in BasicWeaponMoves is meant for a different weapon, we don't use any combos or decorators here
        public Skill CreateSkill(Player player)
        {
            List<Skill> playerSkills = player.ListOfSkills;
            List<Skill> tmp = new List<Skill>();
            SpearStab s1 = new SpearStab();
            SwordSlash s2 = new SwordSlash();
            AxeCut s3 = new AxeCut();
            LightSpear s4 = new LightSpear();
            SharpenSword s5 = new SharpenSword();
            CloneAxe s6 = new CloneAxe();
            if (s1.MinimumLevel <= player.Level) tmp.Add(s1); // check level requirements
            if (s2.MinimumLevel <= player.Level) tmp.Add(s2);
            if (s3.MinimumLevel <= player.Level) tmp.Add(s3);
            if (s4.MinimumLevel <= player.Level) tmp.Add(s4);
            if (s5.MinimumLevel <= player.Level) tmp.Add(s5);
            if (s6.MinimumLevel <= player.Level) tmp.Add(s6);
            foreach (Skill skill in playerSkills) // don't offer skills which the player knows already
            {
                if (skill is SpearStab) tmp.Remove(s1);
                if (skill is SwordSlash) tmp.Remove(s2);
                if (skill is AxeCut) tmp.Remove(s3);
                if (skill is LightSpear) tmp.Remove(s4);
                if (skill is SharpenSword) tmp.Remove(s5);
                if (skill is CloneAxe) tmp.Remove(s6);
            }
            if (tmp.Count == 0) return null;
            return tmp[Index.RNG(0, tmp.Count)];
        }

    }
}
