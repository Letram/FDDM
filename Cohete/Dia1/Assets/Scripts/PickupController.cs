using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public int value;
    private int positionIndex;

    public void setPositionIndex(int index)
    {
        positionIndex = index;
    }

    public int getPositionIndex()
    {
        return positionIndex;
    }
}
