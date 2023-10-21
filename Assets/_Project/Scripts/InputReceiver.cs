
using UnityEngine;

public interface InputReceiver
{
    public virtual void OnDirectionChange(Vector2 direction) {}
    public virtual void OnStartSprint() {}
    public virtual void OnEndSprint() {}
    public virtual void OnStartInteract() {}
    public virtual void OnEndInteract() {}
    public virtual void OnTargetChange(Vector2 target) {}
}
