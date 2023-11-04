using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class TopDownMovementController : MonoBehaviour
{

    [SerializeField] private float minMovementSpeed;
    [SerializeField] private float maxMovementSpeed;

    private Rigidbody2D rb2d;
    private InputSender playerInput;
    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<InputSender>();
    }




    private void Update() {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(playerInput.GetLookDirection().y - transform.position.y, playerInput.GetLookDirection().x - transform.position.x) - 90);
        rb2d.AddForce(playerInput.GetMoveDirection().normalized * maxMovementSpeed * Time.deltaTime * (playerInput.IsSprinting() ? 2 : 1));
    }
}
