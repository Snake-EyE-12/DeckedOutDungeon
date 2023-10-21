using System.Collections;
using System.Collections.Generic;
using Guymon.Utilities;
using UnityEngine;

public class FrostDispenser : PointOfInterest
{
    [SerializeField][Range(0, 1)] private float triggerChance;
    protected override void OnTrigger() {
        Guymon.Utilities.Logger.Info("Frost Dispenser Triggered");
    }
}
