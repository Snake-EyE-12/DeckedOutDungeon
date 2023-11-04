using System.Collections.Generic;
using UnityEngine;
using System;
namespace testing
{
    public abstract class TStateManager2D<T> : MonoBehaviour where T : Enum
    {
        protected Dictionary<T, TBaseState2D<T>> States = new Dictionary<T, TBaseState2D<T>>();
        protected TBaseState2D<T> CurrentState;
        void Start() {
            CurrentState.OnEnterState(this);
        }
        void Update() {
            
            CurrentState.UpdateState(this);
            
        }
        public void ChangeState(T stateKey) {
            CurrentState.OnExitState(this);
            CurrentState = States[stateKey];
            CurrentState.OnEnterState(this);
        }
        void OnTriggerEnter2D(Collider2D other) {
            CurrentState.OnTriggerEnter2D(other, this);
        }
        void OnTriggerStay2D(Collider2D other) {
            CurrentState.OnTriggerStay2D(other, this);
        }
        void OnTriggerExit2D(Collider2D other) {
            CurrentState.OnTriggerExit2D(other, this);
        }
    }
}