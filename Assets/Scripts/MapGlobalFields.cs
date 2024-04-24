using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGlobalFields : MonoBehaviour
{
    public static List<GameObject> allEnemies = new();

    public static int[,] lockedCell = new int[5, 10]
    {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
        {1, 1, 1, 1, 0, 0, 0, 0, 1, 0},
        {0, 0, 0, 1, 1, 1, 1, 1, 1, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };

}
