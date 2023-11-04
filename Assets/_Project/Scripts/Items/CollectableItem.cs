using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private InventoryItem item;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer.sprite = item.sprite;
    }
    public InventoryItem getItemStack() {
        return item;
    }
}
