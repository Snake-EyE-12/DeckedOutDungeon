using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputSender
{
    public virtual Vector2? GetMoveDirection() {return null; }
    public virtual bool IsSprinting() { return false; }
    public virtual bool IsCrouching() { return false;}
    public virtual bool IsInteracting() { return false; }
    public virtual Vector2? GetLookDirection() { return null; }
    public virtual float GetScrollDirection() { return 0; }
}
