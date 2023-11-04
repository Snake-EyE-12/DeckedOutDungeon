using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, InputSender
{
    [SerializeField] private KeyCode sprintKey = KeyCode.C;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private bool invertScrollWheel;
    [SerializeField][Min(0.001f)] private float scrollSensitivity;


    private Vector2 movementInput;
    private bool sprinting;
    private bool crouching;
    private bool interacting;
    private Vector2 mousePos;
    private void Update() {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(Input.GetKeyDown(sprintKey)) sprinting = true;
        if(Input.GetKeyUp(sprintKey)) sprinting = false;
        if(Input.GetKeyDown(crouchKey)) crouching = true;
        if(Input.GetKeyUp(crouchKey)) crouching = false;
        if(Input.GetKeyDown(interactKey)) interacting = true;
        if(Input.GetKeyUp(interactKey)) interacting = false;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public Vector2 GetMoveDirection() {
        return movementInput;
    }
    public bool IsSprinting() {
        return sprinting;
    }
    public bool IsCrouching() {
        return crouching;
    }
    public bool IsInteracting() {
        return interacting;
    }
    public Vector2 GetLookDirection() {
        return mousePos;
    }
    public float GetScrollDirection() {
        return Input.GetAxis("Mouse ScrollWheel") / scrollSensitivity * ((invertScrollWheel) ? -1 : 1);
    }
}
