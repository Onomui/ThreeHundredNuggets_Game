using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGlobalFields : MonoBehaviour
{
    public static List<GameObject> allEnemies = new();

    public static int[,] lockedCellExample = new int[5, 10]
    {
        {1, 1, 1, 1, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 1, 0, 1, 1, 1, 1, 0},
        {0, 0, 0, 1, 1, 1, 0, 0, 1, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };
    public static int[,] lockedCell;

    private void Awake()
    {
        allEnemies.Clear();
        lockedCell = DeepCopy<int>(lockedCellExample);
    }

    private static T[,] DeepCopy<T>(T[,] original)
    {
        int rows = original.GetLength(0);
        int cols = original.GetLength(1);

        T[,] copy = new T[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                copy[i, j] = original[i, j];
            }
        }

        return copy;
    }

}
