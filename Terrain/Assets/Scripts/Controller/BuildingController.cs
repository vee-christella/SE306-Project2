﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public static BuildingController Instance { get; protected set; }

    public Sprite[] buildingSprites = new Sprite[12];
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool addBuildingToTile(string buildingType, Tile tile)
    {   
        Building building = GameController.Instance.Game.addBuildingToTile(buildingType, tile);
        if(building!= null)
        {
            Debug.Log("Building added asdfqwer");
            GameObject buildingGO = new GameObject();
            buildingGO.name = "Building(" + tile.X + ", " + tile.Y + ")";
            buildingGO.transform.position = new Vector3(tile.X, tile.Y, tile.Z);
            SpriteRenderer buildingSR = buildingGO.AddComponent<SpriteRenderer>();
            buildingSR.sortingLayerName="Building";
            buildingSR.sprite = buildingSprites[building.Id];
            return true;
        }
        else
        {
            return false;
        }
    }
}
