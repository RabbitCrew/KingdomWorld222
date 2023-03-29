using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectNS
{
    public struct ObjectSize
    {
        public int sizeX { get; }
        public int sizeY { get; }
        public int objNum { get; }
        public ObjectSize(int sizeX, int sizeY, int objNum)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.objNum = objNum;
        }
    }

    public enum ObjectNum
    {
        TREE, MINE, ANEXCHANGE, CARPENTERHOUSE,
        CHEESEHOUSE, FABRICHOUSE, FARM, HAMHOUSE,
        HOUSE, HUNTERHOUSE, MINEWORKERHOUSE, SMITHY,
        STORAGE, UNIVERSITAS, WOODCUTTERHOUSE
    };

    public enum TileNum
    {
        BUMPYTILE, FLATTILE, GLASS, RIVER, OCEAN, STONE
    };


}
