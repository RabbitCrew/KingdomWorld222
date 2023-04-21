using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobStringArr : MonoBehaviour
{
    protected string[] jobArr = new string[13]
        { "Citizen", "Woodcutter", "Carpenter", "Hunter", "Farmer", "Pastoralist", "Warehouse Keeper", "Iron Miner", "Stone Miner", "Ham Npc", "Cheese Npc", "Cloth Npc", "Smith" };

    protected string[] buildingArr = new string[17]
        {"Tree", "Mine", "AnExchange", "CapenterHouse",
        "CheeseHouse", "FabricHouse", "Farm", "HamHouse",
        "House", "HunterHouse", "MineWorkerHouse", "Smithy",
        "Storage", "Universitas", "WoodCutterHouse", "Field",
        "Road" };
}
