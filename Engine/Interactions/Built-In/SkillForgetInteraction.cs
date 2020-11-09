using System;
using System.Collections.Generic;
using Game.Engine.Skills;

namespace Game.Engine.Interactions
{
    // a special interaction used for fogetting skills
    // if you want a clear example of how to write your own interesting interaction, this is probably NOT the right place 
    // see Gymir and Hymir files instead

    [Serializable]
    class SkillForgetInteraction : ListBoxInteraction
    {
        public SkillForgetInteraction(GameSession parentSession) : base(parentSession) 
        { 
            Name = "interaction0002";
            Enterable = false;
        }

        protected override void RunContent()
        {
            parentSession.SendText("\nDrinking from this fountain can help you forget about one of your skills.");
            List<Skill> tmp = parentSession.currentPlayer.ListOfSkills;
            List<string> choices = new List<string>();
            foreach (Skill sk in tmp)
            {
                    choices.Add(sk.ToString());
            }
            choices.Add("Thank you, I have changed my mind");
            int a = GetListBoxChoice(choices);
            if (a == 0) parentSession.SendText("\nCan't forget this skill."); // it disables to forget the basic skills(Punch or Hadouken), which are currently learned in the first place
            else if (a < choices.Count-1) parentSession.currentPlayer.ListOfSkills.RemoveAt(a);
        }
    }
}
