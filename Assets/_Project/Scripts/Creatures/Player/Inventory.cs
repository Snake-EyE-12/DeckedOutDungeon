using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryData data;
    [SerializeField] private Sprite[] spritesForTesting;
    [SerializeField][Min(0)] private float scrollSpeed;
    private float slot = 0;

    private InputSender playerInput;
    private void Awake() {
        playerInput = GetComponent<InputSender>();

        //test stuff
        data.items.Clear();
        AddItem(new InventoryItem("Ruby", 1, spritesForTesting[0]));
        AddItem(new InventoryItem("Stone", 1, spritesForTesting[1]));
        AddItem(new InventoryItem("Stone", 2, spritesForTesting[1]));
        AddItem(new InventoryItem("Red", 1, spritesForTesting[2]));
        AddItem(new InventoryItem("Blue", 1, spritesForTesting[3]));
        //end
    }
    public void AddItem(InventoryItem item) {
        foreach(InventoryItem i in data.items) {
            if(item.Equals(i)) {
                i.count += item.count;
                return;
            }
        }
        data.items.Add(item);
    }
    private void Update() {
        slot += playerInput.GetScrollDirection() * scrollSpeed * Time.deltaTime;
        data.selectedSlot = (int)Mathf.Abs(slot) % data.items.Count;
    }
}
