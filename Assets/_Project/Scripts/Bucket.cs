using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private List<DungeonActionBase> dungeonActions = new List<DungeonActionBase>();
    [SerializeField][Min(0)][Tooltip("seconds")] private float playSpeed;
    private float elapsedTime;
    //private int frostCount = 0;
    //private int coinCount = 0;


    private void Update() {
        if(elapsedTime < playSpeed) {
            elapsedTime += Time.deltaTime;
            return;
        }
        if(dungeonActions.Count > 0) {
            elapsedTime = 0;
            playAction();
        }
    }



    private void playAction() {
        dungeonActions[0].Activate();
        dungeonActions.RemoveAt(0);
    }
    private void shuffleActions() {
        for(int i = 0; i < dungeonActions.Count; i++) {
            int randomValue = Random.Range(0, dungeonActions.Count);
            DungeonActionBase temp = dungeonActions[i];
            dungeonActions[i] = dungeonActions[randomValue];
            dungeonActions[randomValue] = temp;
        }
    }
}
