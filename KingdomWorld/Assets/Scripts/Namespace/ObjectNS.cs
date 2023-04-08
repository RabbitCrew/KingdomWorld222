using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectNS
{
    public struct ObjectInfo
    {
        public int sizeX { get; }
        public int sizeY { get; }
        public int objNum { get; }
        public int[] possibleTileArr { get; }
        public ObjectInfo(int sizeX, int sizeY, int objNum, int[] possibleTileArr)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.objNum = objNum;
            this.possibleTileArr = possibleTileArr;
        }
    }

    public enum ObjectTypeNum
    {
        WATING = -1,TREE, MINE, ANEXCHANGE, CARPENTERHOUSE,
        CHEESEHOUSE, FABRICHOUSE, FARM, HAMHOUSE,
        HOUSE, HUNTERHOUSE, MINEWORKERHOUSE, SMITHY,
        STORAGE, UNIVERSITAS, WOODCUTTERHOUSE, FIELD,
        ROAD
    };

    public enum TileNum
    {
        BUMPYTILE, FLATTILE, GLASS, RIVER, OCEAN, STONE
    };

    public enum JobNum
    {
        CITIZEN, WOODCUTTER, CARPENTER, HUNTER, FARMER, PASTORALIST, WAREHOUSEKEEPER
    };
}
