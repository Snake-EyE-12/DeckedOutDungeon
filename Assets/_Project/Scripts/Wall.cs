using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject frameObject;
    [SerializeField] private GameObject solidWallObject;
    
    public void Initialize(bool isFrame) {
        if(isFrame) solidWallObject.SetActive(false);
        else frameObject.SetActive(false);

        // connectedWalls = wallCount;
        // totalWallLength = wallLength;
        // position = placement;
        // frame = Vector2.Distance(position[0], position[1]) >= minimumLengthForFrame && connectedWalls > 4 && Random.Range(0.0f, 1.0f) <= becomeFrameChance;
        // if(frame) {
        //     GetComponent<SpriteRenderer>().color = Color.yellow;
        //     GetComponent<Collider2D>().enabled = false;
        // }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger Happened");
    }
}
