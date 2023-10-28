using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputSender : MonoBehaviour
{
    private InputReceiver[] inputReceivers;
    private void Awake() {
        inputReceivers = GetComponents<InputReceiver>();
    }
}
