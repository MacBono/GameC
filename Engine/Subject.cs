using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine
{
    // monsters and players are subjects
    // nothing important happens here, just a bunch of properties are defined
    [Serializable]
    public abstract class Subject : DisplayItem
    {
        protected int health, strength, armor, precision, magicPower, stamina;
        public virtual int Health
        {
            get
            {
                if (health < 0) {
                    health = 0;
                    return health;
                }
                else
                    return health;
            }
            set
            {
                health = value;
            }
        }
        public virtual int Strength
        {
            get
            {
                if (strength < 0) return 0;
                else
                    return strength;
            }
            set
            {
                strength = value;
            }
        }
        public virtual int Armor
        {
            get
            {
                if (armor< 0) return 0;
                else
                    return armor;
            }
            set
            {
                armor = value;
            }
        }
        public virtual int Precision
        {
            get
            {
                if (precision< 0) return 0;
                else
                    return precision;
            }
            set
            {
                precision = value;
            }
        }
        public virtual int MagicPower
        {
            get
            {
                if (magicPower< 0) return 0;
                else
                    return magicPower;
            }
            set
            {
                magicPower = value;
            }
        }
        public virtual int Stamina
        {
            get
            {
                if (stamina < 0) return 0;
                else
                    return stamina;
            }
            set
            {
                stamina = value;
            }
        }
    }
}
