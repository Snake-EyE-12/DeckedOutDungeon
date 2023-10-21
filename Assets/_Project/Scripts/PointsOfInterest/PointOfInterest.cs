using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guymon.DesignPatterns;

public abstract class PointOfInterest : MonoBehaviour
{
    [SerializeField] private string triggerEventName;

    private void OnEnable() {
        EventHandler.AddListener(triggerEventName, OnTrigger);
    }
    private void OnDisable() {
        EventHandler.RemoveListener(triggerEventName, OnTrigger);
    }
    protected abstract void OnTrigger();


}
