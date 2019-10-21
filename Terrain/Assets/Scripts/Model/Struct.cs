using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Struct
{
    string name;
    int num;

    public string Name { get => name; set => name = value; }
    public int Num { get => num; set => num = value; }
    public Struct(string name, int num)
    {
        Name = name;
        Num = num;
    }
}
