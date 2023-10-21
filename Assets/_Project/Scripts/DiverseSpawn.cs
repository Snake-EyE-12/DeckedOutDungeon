using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guymon.Utilities;


public class DiverseSpawn : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 normal;
    [Range(0.0f,360.0f)] public float angle;
    public float radius;


    //[SerializeField][Range(0.0f,360.0f)] private float angle;
    //[SerializeField] private float arc;
    //[SerializeField] private float force;
    private void Start() {
        //spawn();
    }
    private void spawn() {
        //DrawWireArc(transform.position, direction, roll, angle, radius);
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction.normalized * radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + normal.normalized);
        Gizmos.color = Color.red;
        GizmosExtensions.DrawWireArc(transform.position, direction, normal, angle, radius);
    }
    
}
