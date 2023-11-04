using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image[] hearts = new Image[5];
    [SerializeField] private Image staminaBar;
    [SerializeField] private TMP_Text clankCounter;
    [SerializeField] private TMP_Text hazardCounter;
    [SerializeField] private TMP_Text trapTimer;
    [SerializeField] private PlayerInfoBlock playerDungeonInfo;
    [SerializeField] private InventoryItemDisplay[] itemDisplay = new InventoryItemDisplay[3];
    [SerializeField] private InventoryData inventoryData;





    
    
    private void Update() {
        updateHearts();
        updateWheel();
        staminaBar.fillAmount = ((float)playerDungeonInfo.stamina / 100);
        clankCounter.text = $"{playerDungeonInfo.clankBlock}";
        hazardCounter.text = $"{playerDungeonInfo.hazardBlock}";
        trapTimer.text = $"{DungeonManager.Instance().getTrapDisplayTime()}";
    }
    private void updateHearts() {
        for(int i = 0 ; i < hearts.Length; i++) {
            if(80 - (20 * i) > playerDungeonInfo.health) hearts[4 - i].color = Color.black;
            else hearts[4 - i].color = Color.red;
        }
    }
    private void updateWheel() {
        if(inventoryData.items.Count <= 0) return;
        for(int i = 0; i < 3; i++) {
            int listPos = ((inventoryData.selectedSlot + (i - 1)) % inventoryData.items.Count);
            if(listPos < 0) listPos = inventoryData.items.Count + listPos;
            itemDisplay[i].image.sprite = inventoryData.items[listPos].sprite;
            itemDisplay[i].count.text = $"{inventoryData.items[listPos].count}";
        }
    }
}
