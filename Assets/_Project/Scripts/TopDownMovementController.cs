using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class TopDownMovementController : MonoBehaviour, InputReceiver
{
    private Vector2 moveDirection;
    private int sprinting = 0;
    private Rigidbody2D rb2d;

    [SerializeField] private float minMovementSpeed;
    [SerializeField] private float maxMovementSpeed;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }



    public void OnDirectionChange(Vector2 direction) {
        moveDirection = direction;
    }
    public void OnStartSprint() {
        sprinting++;
    }
    public void OnEndSprint() {
        sprinting--;
    }
    public void OnTargetChange(Vector2 target) {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) - 90);
    }
    



    private void Update() {
        rb2d.AddForce(moveDirection.normalized * maxMovementSpeed * Time.deltaTime * (sprinting + 1));
    }
}
