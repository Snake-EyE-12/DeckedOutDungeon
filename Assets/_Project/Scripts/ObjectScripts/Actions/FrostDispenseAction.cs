using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;

[CreateAssetMenu(fileName = "Action", menuName = "DungeonActions/FrostDispense", order = 0)]
public class FrostDispenseAction : DungeonActionBase
{
    public override void Activate() {
        DungeonManager.Instance().generateFrost();
    }
}