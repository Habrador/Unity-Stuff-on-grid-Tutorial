using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move an object with the mouse and try to lock the object to the grid
public class MoveObject : MonoBehaviour 
{
    //The squares that approximate the shape of the object
    public GameObject[] squaresArray;

    //How many square in which directions does this object consist of
    public int squaresX;
    public int squaresZ;
	
	
	
	void Update() 
	{
        LockObjectToGrid();

        RotateObject();
    }



    //Rotate the object
    private void RotateObject()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0f, -90f, 0f));

            SwapSquares();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(new Vector3(0f, 90f, 0f));

            SwapSquares();
        }
    }



    //Swap how many squares we have in each direction
    private void SwapSquares()
    {
        int temp = squaresX;

        squaresX = squaresZ;

        squaresZ = temp;
    }



    //Lock the object to the grid
    private void LockObjectToGrid()
    {
        //Find the coordinate of the mouse cursor in world space
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Vector3 worldPos = hit.point;

            //Snap the center of the object to the grid

            //Need to add 0.5 if the shape is uneven, like in a 3x3
            float xComp = 0f;
            float zComp = 0f;

            if (squaresX % 2 != 0)
            {
                xComp = 0.5f;
            }
            if (squaresZ % 2 != 0)
            {
                zComp = 0.5f;
            }

            float cellSize = GridController.current.cellSize;

            float snapX = cellSize * Mathf.Round(worldPos.x / cellSize) + xComp;
            float snapZ = cellSize * Mathf.Round(worldPos.z / cellSize) + zComp;

            //Snap the object to the grid
            transform.position = new Vector3(snapX, worldPos.y, snapZ);


            //But dont snap if all the squares that approximate this object are not within the grid
            if (!AreAllSquaresWithinTheGrid())
            {
                transform.position = worldPos;
            }
        }
    }



    //Check if all squares that approximate the object are within the grid
    private bool AreAllSquaresWithinTheGrid()
    {
        bool isWithinGrid = true;

        for (int i = 0; i < squaresArray.Length; i++)
        {
            Vector3 squareWorldPos = squaresArray[i].transform.position;

            //If this square is not within the grid
            if (!GridController.current.IsWorldPosInGrid(squareWorldPos))
            {
                isWithinGrid = false;
                
                //Dont need to check the other squares
                break;
            }
        }

        return isWithinGrid;
    }
}
