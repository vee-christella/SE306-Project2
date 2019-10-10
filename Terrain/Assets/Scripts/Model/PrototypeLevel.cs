using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeLevel
{
    private static int[,] arr = new int[10, 10] {
        { 6, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 6, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 6, 4, 4, 4, 2, 2, 4, 6, 6, 6 },
        { 6, 6, 4, 4, 2, 2, 6, 6, 4, 4 },
        { 0, 6, 4, 4, 4, 2, 6, 6, 4, 4 },
        { 0, 6, 4, 4, 4, 2, 2, 2, 2, 2 },
        { 0, 6, 6, 4, 4, 4, 2, 2, 2, 2 },
        { 0, 0, 6, 4, 4, 4, 4, 2, 2, 2 },
        { 0, 0, 6, 4, 4, 4, 4, 2, 4, 4 },
        { 0, 0, 6, 0, 4, 4, 4, 4, 4, 4 }
    };

    public static int[,] Arr { get => arr; set => arr = value; }
}
