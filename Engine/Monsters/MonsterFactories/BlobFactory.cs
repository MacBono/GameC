using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterFactories
{
    [Serializable]
    class BlobFactory : MonsterFactory
    {
        // this factory produces Red Blobs (and later Acid Blobs)

        private int encounterNumber = 0; // how many times has this factory been used already?
        public override Monster Create(int playerLevel)
        {
            if (encounterNumber == 0) // if this is the first time, return a Red Blob
            {
                encounterNumber++;
                return new RedBlob(playerLevel);
            }
            else if (encounterNumber == 1) // if this is the second time, return an evolution (Acid Blob)
            {
                encounterNumber++;
                return new AcidBlob(playerLevel);
            }
            else return null; // no more Blobs to create
        }
        public override System.Windows.Controls.Image Hint()
        {
            if (encounterNumber == 0) return new RedBlob(0).GetImage();
            else if (encounterNumber == 1) return new AcidBlob(0).GetImage();
            else return null;
        }
    }
}
