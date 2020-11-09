using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterFactories
{
    [Serializable]
    class ElementalFactory : MonsterFactory
    {
        // this factory produces elementals

        private int encounterNumber = 0; // how many times has this factory been used already?
        public override Monster Create(int playerLevel)
        {
            if (encounterNumber == 0) // if this is the first time, return an Iron Elemental
            {
                Random rng = new Random();
                encounterNumber = rng.Next(1, 5);
                return new IronElemental(playerLevel);
            }
            else  // every next time, return a random Elemental out of 4 types: Fire, Air, Earth or Water
            {
                if (encounterNumber == 1)
                {
                    Random rng = new Random();
                    encounterNumber = rng.Next(1, 5);
                    return new AirElemental(playerLevel);
                }
                else if (encounterNumber == 2)
                {
                    Random rng = new Random();
                    encounterNumber = rng.Next(1, 5);
                    return new FireElemental(playerLevel);
                }
                else if (encounterNumber == 3)
                {
                    Random rng = new Random();
                    encounterNumber = rng.Next(1, 5);
                    return new WaterElemental(playerLevel);
                }
                else
                {
                    Random rng = new Random();
                    encounterNumber = rng.Next(1, 5);
                    return new EarthElemental(playerLevel);
                }
            }
        }
        public override System.Windows.Controls.Image Hint()
        {
            if (encounterNumber == 0) return new IronElemental(0).GetImage();
            else if (encounterNumber == 1) return new AirElemental(0).GetImage();
            else if (encounterNumber == 2) return new FireElemental(0).GetImage();
            else if (encounterNumber == 3) return new WaterElemental(0).GetImage();
            else if (encounterNumber == 4) return new EarthElemental(0).GetImage();
            else return null;
        }
    }
}
