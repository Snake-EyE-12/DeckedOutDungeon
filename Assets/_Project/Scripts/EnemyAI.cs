using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : InputSender
{
    private void Update() {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            
        }
    }
    private class EnemyStateBase
    {

    }
}
