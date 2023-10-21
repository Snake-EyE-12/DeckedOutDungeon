using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentOfInteraction : MonoBehaviour, InputReceiver
{
    private Interactable interactingWith;

    public bool ChangeInteraction(Interactable interactable) {
        if(interactingWith != interactable) {
            interactingWith = interactable;
            return true;
        }
        return false;
    }
    public void EnterInteractable(Interactable interactable) {
        ChangeInteraction(interactable);
    }
    public void ExitInteractable(Interactable interactable) {
        if(interactingWith == interactable) {
            ChangeInteraction(null);
        }
    }
    public void OnStartInteract() {
        interactingWith.OnStartInteraction();
    }
}
