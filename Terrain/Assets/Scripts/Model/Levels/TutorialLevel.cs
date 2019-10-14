using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel
{
    private static int[,] arr = new int[5, 5] {
        { 4, 2, 2, 4, 1 },
        { 4, 2, 2, 4, 1 },
        { 4, 4, 4, 4, 1 },
        { 4, 4, 6, 6, 6 },
        { 6, 6, 6, 1, 1 }
    };

    public static int[,] Arr { get => arr; set => arr = value; }
}
