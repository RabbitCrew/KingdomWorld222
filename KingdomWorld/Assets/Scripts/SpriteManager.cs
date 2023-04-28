using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;
public class SpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite[] citizenSprArr;
    [SerializeField] private Sprite[] grassTileArr;
    [SerializeField] private Sprite[] grassShadowTileArr;
    [SerializeField] private Sprite[] stoneTileArr;
    [SerializeField] private Sprite[] stoneShadowTileArr;
    [SerializeField] private Sprite[] bumpyGroundTileArr;
    [SerializeField] private Sprite[] bumpyGroundShadowTileArr;
    [SerializeField] private Sprite[] groundTileArr;
    [SerializeField] private Sprite[] groundShadowTileArr;
    [SerializeField] private Sprite[] riverTileArr;
    [SerializeField] private Sprite[] riverShadowTileArr;
    [SerializeField] private Sprite[] seaTileArr;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Sprite GetTileSprite(int tileType, int tileIndex)
	{
        switch ((TileNum)tileType)
		{
            case TileNum.OCEAN: return seaTileArr[0]; break;
            case TileNum.RIVER: return riverTileArr[tileIndex]; break;
            case TileNum.FLATTILE: return groundTileArr[tileIndex]; break;
            case TileNum.BUMPYTILE: return bumpyGroundTileArr[tileIndex]; break;
            case TileNum.STONE: return stoneTileArr[tileIndex]; break;
            case TileNum.GRASS: return grassTileArr[tileIndex]; break;
		}

        return null;
	}

    public Sprite GetTileShadowSprite(int tileType, int tileIndex)
	{
        switch ((TileNum)tileType)
        {
            case TileNum.OCEAN: return seaTileArr[tileIndex]; break;
            case TileNum.RIVER: return riverShadowTileArr[tileIndex]; break;
            case TileNum.FLATTILE: return groundShadowTileArr[tileIndex]; break;
            case TileNum.BUMPYTILE: return bumpyGroundShadowTileArr[tileIndex]; break;
            case TileNum.STONE: return stoneShadowTileArr[tileIndex]; break;
            case TileNum.GRASS: return grassShadowTileArr[tileIndex]; break;
        }

        return null;
	}

    public Sprite GetCitizenSprArr(int index)
    {
        if (index < citizenSprArr.Length && index > -1)
        {
            return citizenSprArr[index]; 
        }
        else
        {
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
