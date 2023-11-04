using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "InventoryData", menuName = "Data/InventoryData", order = 0)]
public class InventoryData : ScriptableObject {
    public List<InventoryItem> items = new List<InventoryItem>();
    public int selectedSlot;

}