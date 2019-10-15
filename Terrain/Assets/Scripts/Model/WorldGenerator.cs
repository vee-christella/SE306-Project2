using System.Collections;
using System.Collections.Generic;
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


}
