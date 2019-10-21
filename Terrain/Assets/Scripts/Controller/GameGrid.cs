using UnityEngine;

/*
Retrieved from: https://unity3d.college/2017/10/08/simple-unity3d-snap-grid-system/

This class acts as the base grid for the game world. It allows tiles and buildings 
to be placed/snap to a grid. Grid points are whole numbers in the x-z plane,
starting at (0, 0).
*/
public class GameGrid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    /*
    Gets the nearest whole number coordinates to a point anywhere in the gameworld.
    */
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = (int)Mathf.Round(position.x / size);
        int yCount = (int)Mathf.Round(position.y / size);
        int zCount = (int)Mathf.Round(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size
            );

        result += transform.position;

        return result;
    }

    /*
    Method to draw circles to show the grid squares' corners.
    This won't be shown when the game is playing, as is only used for development.
    */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < 40; x += size)
        {
            for (float z = 0; z < 40; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }

}