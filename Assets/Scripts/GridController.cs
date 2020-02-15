using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    //The quad used to display the cells
    public GameObject cellQuadObj;

    //The size of one cell
    public float cellSize = 1f;
    //How many cells do we have in one row?
    public int gridSize = 20;

    //To make it easier to access the script from other scripts
    public static GridController current;



    void Start()
    {
        current = this;
    
        //Display the grid cells with quads
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                //The center position of the cell
                Vector3 centerPos = new Vector3(x + cellSize / 2f, 0f, z + cellSize / 2f);

                GameObject newCellQuad = Instantiate(cellQuadObj, centerPos, Quaternion.identity, transform);

                newCellQuad.SetActive(true);
            }
        }
    }



    //Is a world position within the grid?
    public bool IsWorldPosInGrid(Vector3 worldPos)
    {
        bool isWithin = false;

        int gridX = TranslateFromWorldToGrid(worldPos.x);
        int gridZ = TranslateFromWorldToGrid(worldPos.z);

        if (gridX >= 0 && gridZ >= 0 && gridX < gridSize && gridZ < gridSize)
        {
            isWithin = true;
        }

        return isWithin;
    }



    //Translate from world position to grid position
    private int TranslateFromWorldToGrid(float pos)
    {
        int gridPos = Mathf.FloorToInt(pos / cellSize);

        return gridPos;
    }
}
