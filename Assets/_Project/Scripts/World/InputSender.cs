using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputSender
{
    public virtual Vector2 GetMoveDirection() {return Vector2.zero; }
    public virtual bool IsSprinting() { return false; }
    public virtual bool IsCrouching() { return false;}
    public virtual bool IsInteracting() { return false; }
    public virtual Vector2 GetLookDirection() { return Vector2.zero; }
    public virtual float GetScrollDirection() { return 0; }
}
