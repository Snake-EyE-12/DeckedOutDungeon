using System.Collections;
using System.Collections.Generic;
using Guymon.Utilities;
using UnityEngine;

public class ClankGenerator : PointOfInterest
{
    protected override void OnTrigger() {
        Guymon.Utilities.Console.Info("Clank Generated");
    }
}
