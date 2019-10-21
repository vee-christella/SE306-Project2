using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator
{

    public static void generateWorld(Game game)
    {
        int rows = game.Rows;
        int cols = game.Columns;


        float rowCoord;
        float colCoord;
        float max = 0;
        float min = 1;
        float[,] perlinArray1 = new float[rows, cols];
        float[,] perlinArray2 = new float[rows, cols];
        List<float> perlinList1 = new List<float>();
        List<float> perlinList2 = new List<float>();
        float scale;
        scale = 2 + rows / 10f;
        bool mapMaking = true;
        int counter = 0;
        while (mapMaking)
        {

            float randomRow1 = Random.Range(0f, 9999f);
            float randomCol1 = Random.Range(0f, 9999f);
            float randomRow2 = Random.Range(0f, 9999f);
            float randomCol2 = Random.Range(0f, 9999f);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rowCoord = (float)i / rows * scale + randomRow1;
                    colCoord = (float)j / cols * scale + randomCol1;
                    perlinArray1[i, j] = Mathf.PerlinNoise(rowCoord, colCoord);
                    perlinList1.Add(perlinArray1[i, j]);
                    if (perlinArray1[i, j] > max)
                    {
                        max = perlinArray1[i, j];
                    }
                    if (perlinArray1[i, j] < min)
                    {
                        min = perlinArray1[i, j];
                    }
                    rowCoord = (float)i / rows * scale + randomRow2;
                    colCoord = (float)j / cols * scale + randomCol2;
                    perlinArray2[i, j] = Mathf.PerlinNoise(rowCoord, colCoord);
                    perlinList2.Add(perlinArray2[i, j]);
                    if (perlinArray2[i, j] > max)
                    {
                        max = perlinArray2[i, j];
                    }
                    if (perlinArray2[i, j] < min)
                    {
                        min = perlinArray2[i, j];
                    }
                }
            }
            perlinList1.Sort();
            perlinList2.Sort();
            float median1;
            float median2;
            if (perlinList1.Count % 2 == 0)
            {
                median1 = perlinList1[perlinList1.Count * 4 / 10];
                median2 = (perlinList2[perlinList2.Count / 2] + perlinList2[perlinList2.Count / 2 - 1]) / 2;
            }
            else
            {
                median1 = perlinList1[perlinList1.Count * 4 / 10];
                median2 = perlinList2[(perlinList2.Count - 1) / 2];
            }
            int mountainCount = 0;
            int waterCount = 0;
            int plainCount = 0;
            int desertCount = 0;
            float divider = (max - min) / 4;
            bool townHallBuilt = false;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (perlinArray1[i, j] < median1 && perlinArray2[i, j] < median2)
                    {

                        game.getTileAt(i, j).setType(Tile.TileType.Mountain);
                        mountainCount++;

                    }
                    else if (perlinArray1[i, j] < median1 && perlinArray2[i, j] >= median2)
                    {

                        game.getTileAt(i, j).setType(Tile.TileType.Water);
                        waterCount++;

                    }
                    else if (perlinArray1[i, j] >= median1 && perlinArray2[i, j] >= median2)
                    {

                        game.getTileAt(i, j).setType(Tile.TileType.Desert);
                        desertCount++;

                    }
                    else if (perlinArray1[i, j] >= median1 && perlinArray2[i, j] < median2)
                    {

                        game.getTileAt(i, j).setType(Tile.TileType.Plain);
                        plainCount++;
                        if(!townHallBuilt && i > rows * 4 / 10 && i <= rows * 6 / 10 && j > cols * 4 / 10 && j <= cols * 6 / 10 )
                        {
                            game.addBuildingToTile("Town Hall", game.getTileAt(i, j));
                            Debug.Log("TownHall Built");
                            townHallBuilt = true;
                        }

                    }
                }
            }
            if (!townHallBuilt)
            {
                game.getTileAt(rows / 2, cols / 2).setType(Tile.TileType.Plain);
                game.addBuildingToTile("Town Hall", game.getTileAt(rows / 2, cols / 2));
                townHallBuilt = true;
            }
            int num = (rows * cols) / 6;

            perlinList1.Clear();
            perlinList2.Clear();
            Debug.Log("Loop world generation: " + counter);
            counter++;
            if (mountainCount >= num && waterCount >= num && plainCount >= num && desertCount >= num)
            {
                mapMaking = false;
                Debug.Log("Mountains: " + mountainCount + " water: " + waterCount + " plains: " + plainCount + " desert: " + desertCount);
            }
        }
    }

    public static void addBuildingsToWorld(Game game)
    {
        int rows = game.Rows;
        int cols = game.Columns;
        Struct animal = new Struct("Animal Farm", 4);
        Struct bee = new Struct("Bee Farm", 4);
        Struct coal = new Struct("Coal Mine", 5);
        Struct factory = new Struct("Factory", 5);
        Struct forest = new Struct("Forest", 20);
        Struct green = new Struct("Greenhouse", 4);
        Struct hydro = new Struct("Hydro Plant", 2);
        Struct movie = new Struct("Movie Theatre", 3);
        Struct national = new Struct("National Park", 3);
        Struct nuclear = new Struct("Nuclear Plant", 4);
        Struct oil = new Struct("Oil Refinery", 4);
        Struct pollutant = new Struct("Pollutant", 10);
        Struct race = new Struct("Race Track", 3);
        Struct recycling = new Struct("Recycling Plant", 3);
        Struct solar = new Struct("Solar Farm", 2);
        Struct vegetable = new Struct("Vegetable Farm", 3);
        Struct wind = new Struct("Wind Turbine", 2);
        Struct zoo = new Struct("Zoo", 4);
        List<Struct> desert = new List<Struct>() { national, pollutant, nuclear, oil, race, solar, factory, recycling };
        List<Struct> plain = new List<Struct>() { forest, pollutant, movie, national, nuclear, race, wind, zoo, animal, bee, factory, green, recycling, vegetable };
        List<Struct> mountain = new List<Struct>() { coal, pollutant };
        List<Struct> water = new List<Struct>() { hydro, pollutant };
        PlayerPrefs.SetInt("ActivateAchievement", 1);
        var tempCoord = new { x = 0, y = 0 };
        var coords = new[] { tempCoord }.ToList();
        //BuildingController.Instance.addBuildingToTile("Town Hall", tile);
        coords.Remove(tempCoord);

        //create variable for every coordinate
        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 1; j < cols - 1; j++)
            {
                coords.Add(new { x = i, y = j });
            }
        }
        //randomise list of coordinates
        int n = coords.Count - 1;
        while (n > 0)
        {
            int rand = Random.Range(0, n);
            var temp = coords[rand];
            coords[rand] = coords[n];
            coords[n] = temp;
            n--;
        }
        int buildingCounter = 0;
        //loop through list of coordinates in random order so buildings created are spread out
        foreach (var coord in coords)
        {
            game.Money = 500;
            bool replaceBuilding = false;
            int x = coord.x;
            int y = coord.y;
            Tile tile = game.getTileAt(x, y);
            Struct building = new Struct("", 0);
            //pick building to create based on tile
            if (tile.Type == Tile.TileType.Desert && desert.Any())
            {
                building = desert[Random.Range(0, desert.Count)];
            }
            else if (tile.Type == Tile.TileType.Plain && plain.Any())
            {
                building = plain[Random.Range(0, plain.Count)];

            }
            else if (tile.Type == Tile.TileType.Water && water.Any())
            {
                building = water[Random.Range(0, water.Count)];

            }
            else if (tile.Type == Tile.TileType.Mountain && mountain.Any())
            {
                building = mountain[Random.Range(0, mountain.Count)];
            }
            //if building chosen, create on tile
            if (building.Num > 0)
            {
                if (game.addBuildingToTile(building.Name, tile) != null)
                {
                    replaceBuilding = true;
                }
            }
            //If building successfully created, modify building List
            if (replaceBuilding)
            {
                buildingCounter++;
                Debug.Log("Building Generation Counter: " + buildingCounter);
                building.Num--;
                if (building.Num <= 0)
                {
                    desert.Remove(building);
                    plain.Remove(building);
                    water.Remove(building);
                    mountain.Remove(building);
                }
            }
        }
        PlayerPrefs.SetInt("ActivateAchievement", 0);
        Debug.Log("Buildings Generated: " + buildingCounter);
    }
}
