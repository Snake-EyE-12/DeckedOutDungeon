using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;
using UnityEngine;

public class DungeonActionBuffer : Singleton<DungeonActionBuffer>
{
    private Queue<DungeonActionBase> actionQueue = new Queue<DungeonActionBase>();
    [SerializeField][Min(0)][Tooltip("seconds")] private float actionPlaySpeed;
    private float elapsedTime = 0;


    public void Queue(List<DungeonActionBase> actions) {
        foreach(DungeonActionBase da in actions) {
            actionQueue.Enqueue(da);
        }
    }
    public void Queue(DungeonActionBase action) {
        actionQueue.Enqueue(action);
    }
    private void Update() {
        if(elapsedTime < actionPlaySpeed) {
            elapsedTime += Time.deltaTime;
            return;
        }
        if(actionQueue.Count > 0) {
            elapsedTime = 0;
            actionQueue.Dequeue().Activate();
        }
    }
}
