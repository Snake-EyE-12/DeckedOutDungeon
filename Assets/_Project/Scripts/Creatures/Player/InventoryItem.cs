using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int count = 0;
    public Sprite sprite;
    public InventoryItem(string name, int count, Sprite sp) {
        this.count = count;
        itemName = name;
        sprite = sp;
    }
    public bool Equals(InventoryItem item) {
        return item.itemName == itemName;
    }
}
