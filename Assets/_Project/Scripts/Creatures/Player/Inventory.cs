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
        data.items.Clear();

        //test stuff
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
        updateSelectedSlot();
    }
    private void updateSelectedSlot() {
        if(data.items.Count <= 0) return;
        slot += playerInput.GetScrollDirection() * scrollSpeed * Time.deltaTime;
        data.selectedSlot = (int)Mathf.Abs(slot) % data.items.Count;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        CollectableItem collectable;
        if(other.TryGetComponent<CollectableItem>(out collectable)) {
            AddItem(collectable.getItemStack());
            Destroy(other.gameObject);
        }
    }
}
