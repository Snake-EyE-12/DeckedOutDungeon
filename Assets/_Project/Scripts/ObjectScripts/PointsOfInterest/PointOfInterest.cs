using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guymon.DesignPatterns;

public abstract class PointOfInterest : MonoBehaviour
{
    [SerializeField] private string triggerEventName;
    [SerializeField] private string discoverName;

    private void OnEnable() {
        EventHandler.AddListener(triggerEventName, OnTrigger);
        EventHandler.AddListener(discoverName, OnDiscover);
    }
    private void OnDisable() {
        EventHandler.RemoveListener(triggerEventName, OnTrigger);
        EventHandler.RemoveListener(discoverName, OnDiscover);

    }
    protected abstract void OnTrigger();
    protected abstract void OnDiscover();


}
