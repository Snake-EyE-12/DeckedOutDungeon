using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;

[CreateAssetMenu(fileName = "DungeonActionSimpleEvent", menuName = "DungeonActions/SimpleEvent", order = 0)]
public class DungeonActionSimpleEvent : DungeonActionBase
{
    public override void Activate() {
        foreach(string eventName in eventNames) EventHandler.Invoke(eventName);
    }
}