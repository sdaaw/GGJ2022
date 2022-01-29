using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public PlateType plateType;

    public enum PlateType
    {
        Empty,
        Normal,
        JumpObstacle,
        SlideObstacle,
        Red,
        Blue
    }
}
