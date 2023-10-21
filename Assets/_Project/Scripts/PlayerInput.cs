using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputReceiver[] inputReceivers;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    private void Awake() {
        inputReceivers = GetComponents<InputReceiver>();
    }
    private void Update() {
        foreach(InputReceiver ir in inputReceivers) {
            ir.OnDirectionChange(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
            if(Input.GetKeyDown(sprintKey)) ir.OnStartSprint();
            if(Input.GetKeyUp(sprintKey)) ir.OnEndSprint();
            if(Input.GetKeyDown(interactKey)) ir.OnStartInteract();
            if(Input.GetKeyUp(interactKey)) ir.OnEndInteract();
        }
    }
}
