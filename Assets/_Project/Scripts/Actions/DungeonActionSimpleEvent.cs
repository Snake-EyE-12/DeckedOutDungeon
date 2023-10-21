using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;

[CreateAssetMenu(fileName = "DungeonActionSimpleEvent", menuName = "DeckedOutDungeon/SimpleEvent", order = 0)]
public class DungeonActionSimpleEvent : DungeonActionBase
{
    [SerializeField] protected List<string> eventNames;
    public override void Activate() {
        foreach(string eventName in eventNames) EventHandler.Invoke(eventName);
    }
}