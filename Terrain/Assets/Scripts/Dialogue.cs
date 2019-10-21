using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Use as an object to pass in dialogue manager whenever we
// want to start a new dialogue
public class Dialogue
{
    public string npcName;

    [TextArea(3, 10)]
    public string[] sentences;


}
