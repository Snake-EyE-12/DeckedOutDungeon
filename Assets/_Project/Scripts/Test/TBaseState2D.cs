using UnityEngine;
using System;

namespace testing
{
    public abstract class TBaseState2D<T> where T : Enum
    {
        public TBaseState2D(T key) {
            StateKey = key;
        }
        public T StateKey {get; private set;}
        public virtual void OnEnterState(TStateManager2D<T> manager) {}
        public virtual void OnExitState(TStateManager2D<T> manager) {}
        public virtual void UpdateState(TStateManager2D<T> manager) {}
        public virtual void OnTriggerEnter2D(Collider2D other, TStateManager2D<T> manager) {}
        public virtual void OnTriggerStay2D(Collider2D other, TStateManager2D<T> manager) {}
        public virtual void OnTriggerExit2D(Collider2D other, TStateManager2D<T> manager) {}
    }
}