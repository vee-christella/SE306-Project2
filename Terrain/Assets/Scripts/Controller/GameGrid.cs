using UnityEngine;

/*
Retrieved from: https://unity3d.college/2017/10/08/simple-unity3d-snap-grid-system/

Allows tiles to be placed/snap to a grid
*/
public class GameGrid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = (int)Mathf.Floor(position.x / size);
        int yCount = (int)Mathf.Floor(position.y / size);
        int zCount = (int)Mathf.Floor(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size
            );

        result += transform.position;

        return result;
    }

    /*
    Method to draw circles to show the grid squares
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