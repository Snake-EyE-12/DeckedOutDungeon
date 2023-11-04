using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    private int canInteractCount = 0;
    private int interactingCount = 0;
    [SerializeField] private List<DungeonActionBase> dungeonActions = new List<DungeonActionBase>();

    public void OnStartInteraction() {
        interactingCount++;
        DungeonActionBuffer.Instance().Queue(dungeonActions);
    }
    public void OnEndInteraction() {
        interactingCount--;
    }












    private void OnTriggerEnter2D(Collider2D other) {
        AgentOfInteraction aoi;
        if(other.TryGetComponent<AgentOfInteraction>(out aoi)) {
            aoi.EnterInteractable(this);
            canInteractCount++;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        AgentOfInteraction aoi;
        if(other.TryGetComponent<AgentOfInteraction>(out aoi)) {
            aoi.ExitInteractable(this);
            canInteractCount--;
        }
    }
}
