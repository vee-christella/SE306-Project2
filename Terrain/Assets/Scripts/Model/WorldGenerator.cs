using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator
{

    public static void generateWorld(Game game){
        int rows = game.Rows;
        int cols = game.Columns;


        float randomRow1 = Random.Range(0f, 9999f);
        float randomCol1 = Random.Range(0f, 9999f);
        float randomRow2 = Random.Range(0f, 9999f);
        float randomCol2 = Random.Range(0f, 9999f);
        float rowCoord;
        float colCoord;
        float max=0;
        float min=1;
        float[,] perlinArray1 = new float[rows,cols];
        float[,] perlinArray2 = new float[rows,cols];
        float scale = rows/5f;

        for(int i = 0; i<rows; i++){
            for(int j = 0; j<cols; j++){
                rowCoord = (float)i/rows*scale + randomRow1;
                colCoord = (float)j/cols*scale + randomCol1;
                perlinArray1[i,j] = Mathf.PerlinNoise(rowCoord, colCoord);
                if(perlinArray1[i,j] > max){
                    max = perlinArray1[i,j];
                }
                if(perlinArray1[i,j]<min){
                    min = perlinArray1[i,j];
                }
                rowCoord = (float)i/rows*scale + randomRow2;
                colCoord = (float)j/cols*scale + randomCol2;
                perlinArray2[i,j] = Mathf.PerlinNoise(rowCoord, colCoord);
                if(perlinArray2[i,j] > max){
                    max = perlinArray2[i,j];
                }
                if(perlinArray2[i,j]<min){
                    min = perlinArray2[i,j];
                }
            }
        }
        float divider = (max-min)/4;
        for(int i = 0; i<rows; i++){
            for(int j = 0; j<cols; j++){
                if(perlinArray1[i,j]>=min && perlinArray1[i,j]<min+divider*2 && perlinArray2[i,j]<min+divider*2){
                    
                    game.getTileAt(i,j).setType(Tile.TileType.Mountain);

                }else if(perlinArray1[i,j]>=min+divider*2 && perlinArray2[i,j]<min+divider*2){
                    
                    game.getTileAt(i,j).setType(Tile.TileType.Water);

                }else if(perlinArray1[i,j]>=min && perlinArray1[i,j]<min+divider*2 && perlinArray2[i,j]>=min+divider*2){
                    
                    game.getTileAt(i,j).setType(Tile.TileType.Desert);

                }else if(perlinArray1[i,j]>=min+divider*2 && perlinArray2[i,j]>=min+divider*2){
                    
                    game.getTileAt(i,j).setType(Tile.TileType.Plain);

                }
            }
        }
    }

    public static void addBuildingsToWorld(Game game)
    {
        int rows = game.Rows;
        int cols = game.Columns;
        var town = new { name = "Town Hall", num = 1 };
        var animal = new { name = "Animal Farm", num = 4 };
        var bee = new { name = "Bee Farm", num = 4 };
        var coal = new { name = "Coal Mine", num = 5 };
        var factory = new { name = "Factory", num = 5 };
        var forrest = new { name = "Forest", num = 20 };
        var green = new { name = "Greenhouse", num = 4 };
        var hydro = new { name = "Hydro Plant", num = 2 };
        var movie = new { name = "Movie Theatre", num = 3 };
        var national = new { name = "National Park", num = 3 };
        var nuclear = new { name = "Nuclear Plant", num = 4 };
        var oil = new { name = "Oil Refinery", num = 4 };
        var pollutant = new { name = "Pollutant", num = 10 };
        var race = new { name = "Race Track", num = 3 };
        var recycling = new { name = "Recyling Plant", num = 3 };
        var solar = new { name = "Solar Farm", num = 2 };
        var vegetable = new { name = "Vegetable Farm", num = 3 };
        var wind = new { name = "Wind Turbine", num = 2 };
        var zoo = new { name = "Zoo", num = 4 };
        var desert = new[]{ national, pollutant, nuclear, oil, race, solar, factory, recycling }.ToList();
        var plain = new[] { forrest, pollutant, movie, national, nuclear, race, wind, zoo, animal, bee, factory, green, recycling, vegetable }.ToList();
        var mountain = new[] { coal, pollutant}.ToList();
        var water = new[] { hydro, pollutant }.ToList();
        var coords = new List<dynamic>();
        //BuildingController.Instance.addBuildingToTile("Town Hall", tile);
        
        //create variable for every coordinate
        for (int i = 1; i < rows-1; i++)
        {
            for (int j = 1; j < cols-1; j++)
            {
                coords.Add(new { x = i, y = j });
            }
        }
        //randomise list of coordinates
        int n = coords.Count;
        while (n > 1)
        {
            int rand = Random.Range(0, n);
            var temp = coords[rand];
            coords[rand] = coords[n];
            coords[n] = temp;
            n--;
        }
        //loop through list of coordinates in random order so buildings created are spread out
        foreach(var coord in coords)
        {
            bool replaceBuilding = false;
            int x = coord.x;
            int y = coord.y;
            Tile tile = game.getTileAt(x, y);
            var building = new { name = "temp", num = 0 };
            //pick building to create based on tile
            if (tile.Type == Tile.TileType.Desert)
            {
                building = desert[Random.Range(0, desert.Count)];
                
            }else if (tile.Type == Tile.TileType.Plain)
            {
                building = plain[Random.Range(0, plain.Count)];

            }
            else if (tile.Type == Tile.TileType.Water)
            {
                building = water[Random.Range(0, water.Count)];

            }
            else if (tile.Type == Tile.TileType.Mountain)
            {
                building = mountain[Random.Range(0, mountain.Count)];

            }
            //if building chosen, create on tile
            if (building.num > 0)
            {
                if (BuildingController.Instance.addBuildingToTile(building.name, tile))
                {
                    replaceBuilding = true;
                }
            }
            //If building successfully created, modify building List
            if (replaceBuilding)
            {
                var newBuilding = new { name = building.name, num = building.num - 1 };
                if (desert.Remove(building))
                {
                    if (newBuilding.num > 0)
                    {
                        desert.Add(newBuilding);
                    }
                }
                if (plain.Remove(building))
                {
                    if (newBuilding.num > 0)
                    {
                        plain.Add(newBuilding);
                    }
                }
                if (water.Remove(building))
                {
                    if (newBuilding.num > 0)
                    {
                        water.Add(newBuilding);
                    }
                }
                if (mountain.Remove(building))
                {
                    if (newBuilding.num > 0)
                    {
                        mountain.Add(newBuilding);
                    }
                }
            }
        }
        
    }


}
