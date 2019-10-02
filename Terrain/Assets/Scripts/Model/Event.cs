using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event
{

    public enum EventType
    {
        Random, Transition
    };

    private int greenPointDelta;
    private int happinessDelta;
    private int moneyDelta;
    private EventType type;

    public int GreenPointDelta { get => greenPointDelta; set => greenPointDelta = value; }
    public int HappinessDelta { get => happinessDelta; set => happinessDelta = value; }
    public int MoneyDelta { get => moneyDelta; set => moneyDelta = value; }
    public EventType Type { get => type; set => type = value; }

    public abstract void TitleDelta();


}
