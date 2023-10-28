using System.Collections;
using System.Collections.Generic;
using Guymon.Utilities;
using UnityEngine;

public class FrostDispenser : PointOfInterest
{
    [SerializeField][Range(0, 1)] private float triggerChance;
    [SerializeField] private SpawnArea spawnArea;
    protected override void OnTrigger() {
        if(Random.Range(0.0f, 1.0f) <= triggerChance) {
            Guymon.Utilities.Console.Info("Frost Dispenser Triggered");
            spawnArea.Spawn();
        }
    }
}
