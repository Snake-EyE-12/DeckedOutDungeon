using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private InputReceiver[] inputReceivers;
    private void Awake() {
        inputReceivers = GetComponents<InputReceiver>();
    }
    private void Update() {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //if(other)
    }
}
