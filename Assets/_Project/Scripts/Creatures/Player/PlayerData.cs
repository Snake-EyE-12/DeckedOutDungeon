using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour, Damageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxStamina;
    [SerializeField][Min(0)][Tooltip("seconds")] private float staminaDrain;
    [SerializeField][Min(0)] private float sprintingStaminaDrainMultiplier;
    [SerializeField] private PlayerInfoBlock playerDungeonInfo;
    private InputSender playerInput;
    private void Awake() {
        playerInput = GetComponent<InputSender>();
    }
    
    private void Start() {
        playerDungeonInfo.health = maxHealth;
        playerDungeonInfo.stamina = maxStamina;
        playerDungeonInfo.clankBlock = 0;
        playerDungeonInfo.hazardBlock = 0;
    }

    public void OnTakeDamage(int amount) {
        //dealing damage
        // start damage cooldown

        // health - damage
        // update UI hearts
    }
    private void Update() {
        // manage stamina
        playerDungeonInfo.stamina -= (playerInput.IsCrouching() ? (0) : staminaDrain * Time.deltaTime * (playerInput.IsSprinting() ? sprintingStaminaDrainMultiplier : 1));
    }

}
