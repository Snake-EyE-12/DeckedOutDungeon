using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objectToFollow;
    [SerializeField] private float z;

    private void Update() {
        transform.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y, z);
    }
}
