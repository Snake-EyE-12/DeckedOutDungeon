using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DungeonAction", menuName = "DungeonActions/AbstractDungeonActionBase", order = 0)]
public class DungeonActionBase : ScriptableObject
{
    public virtual void Activate() {}
    public virtual void Deactivate() {}
}