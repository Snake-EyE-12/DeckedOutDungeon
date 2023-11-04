using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testing {
    public class TEStateMachine : TStateManager2D<TEStateMachine.EnemyStates>
    {
        public enum EnemyStates
        {
            Wondering,
            Charging,
            Attacking
        }
        private void Awake() {
            States.Add(EnemyStates.Wondering, new EnemyWonderState<EnemyStates>(TEStateMachine.EnemyStates.Wondering));
            States.Add(EnemyStates.Charging, new EnemyChargingState<EnemyStates>(TEStateMachine.EnemyStates.Charging));
            CurrentState = States[EnemyStates.Wondering];
        }

    }
    public class EnemyWonderState<EnemyStates> : TBaseState2D<TEStateMachine.EnemyStates>
    {
        public EnemyWonderState(TEStateMachine.EnemyStates key) : base(key)
        {
        }

        public override void OnEnterState(TStateManager2D<TEStateMachine.EnemyStates> manager)
        {
            Debug.Log("Entered Wondering State");
        }

        public override void UpdateState(TStateManager2D<TEStateMachine.EnemyStates> manager)
        {
            if(Input.GetKeyDown(KeyCode.I)) manager.ChangeState(TEStateMachine.EnemyStates.Charging);
        }
    }
    public class EnemyChargingState<EnemyStates> : TBaseState2D<TEStateMachine.EnemyStates>
    {
        public EnemyChargingState(TEStateMachine.EnemyStates key) : base(key)
        {
        }

        public override void OnEnterState(TStateManager2D<TEStateMachine.EnemyStates> manager)
        {
            Debug.Log("Entered Charging State");
        }

        public override void UpdateState(TStateManager2D<TEStateMachine.EnemyStates> manager)
        {
            if(Input.GetKeyDown(KeyCode.I)) manager.ChangeState(TEStateMachine.EnemyStates.Wondering);
        }
    }
}