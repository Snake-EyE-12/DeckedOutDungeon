using System.Collections;
using System.Collections.Generic;
using Guymon.Utilities;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private Rigidbody2D prefab;
    [SerializeField][Range(0, 360)] private float angle;
    [SerializeField][Range(0, 360)] private float arc;
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;


    [ContextMenu(nameof(Spawn))]
    public void Spawn() {
        Rigidbody2D rb = Instantiate(prefab.gameObject, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.AddForce((Quaternion.AngleAxis(0.5f * Random.Range(-arc, arc), Vector3.forward) * getCenterDirection(angle)).normalized * Random.Range(minForce, maxForce) * 50 * rb.mass);
    }
    private Vector2 getCenterDirection(float degrees) {
        return new Vector2(Mathf.Sin(Mathf.Deg2Rad * degrees), Mathf.Cos(Mathf.Deg2Rad * degrees));
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        GizmosExtensions.DrawWireArc(transform.position, getCenterDirection(angle), Vector3.forward, arc, maxForce);
    }
}
