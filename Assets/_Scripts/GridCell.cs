using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    int gridXPos, gridYPos;
    bool isOpen;

    public void SetGridCoords(int xCoord, int yCoord)
    {
        gridXPos = xCoord;
        gridYPos = yCoord;
    }
}
