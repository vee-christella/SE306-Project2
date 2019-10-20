using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel
{
    private static int[,] arr = new int[5, 5] {
        { 2, 1, 1, 2, 0 },
        { 2, 1, 1, 2, 0 },
        { 2, 2, 2, 2, 0 },
        { 2, 2, 3, 3, 3 },
        { 3, 3, 3, 0, 0 }
    };

    public static int[,] Arr { get => arr; set => arr = value; }
}
