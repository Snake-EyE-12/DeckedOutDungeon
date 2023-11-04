using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guymon.DesignPatterns;

public class EnemyStateMachine : StateMessenger2D<EnemyStateMachine.EnemyStates>
{
    public enum EnemyStates
    {
        Wondering,
        Charging,
        Attacking
    }
    private void Awake() {
        States.Add(EnemyStates.Wondering, new EnemyWonderState<EnemyStates>(EnemyStateMachine.EnemyStates.Wondering));
        States.Add(EnemyStates.Charging, new EnemyChargingState<EnemyStates>(EnemyStateMachine.EnemyStates.Charging));
        CurrentState = States[EnemyStates.Wondering];
    }

}

public class EnemyWonderState<EnemyStates> : BaseState2D<EnemyStateMachine.EnemyStates>
{
    public EnemyWonderState(EnemyStateMachine.EnemyStates key) : base(key)
    {
    }

    public override void OnEnterState(StateMessenger2D<EnemyStateMachine.EnemyStates> manager)
    {
        Debug.Log("Entered Wondering State");
    }

    public override void UpdateState(StateMessenger2D<EnemyStateMachine.EnemyStates> manager)
    {
        if(Input.GetKeyDown(KeyCode.I)) manager.ChangeState(EnemyStateMachine.EnemyStates.Charging);
    }
}
public class EnemyChargingState<EnemyStates> : BaseState2D<EnemyStateMachine.EnemyStates>
{
    public EnemyChargingState(EnemyStateMachine.EnemyStates key) : base(key)
    {
    }

    public override void OnEnterState(StateMessenger2D<EnemyStateMachine.EnemyStates> manager)
    {
        Debug.Log("Entered Charging State");
    }

    public override void UpdateState(StateMessenger2D<EnemyStateMachine.EnemyStates> manager)
    {
        if(Input.GetKeyDown(KeyCode.I)) manager.ChangeState(EnemyStateMachine.EnemyStates.Wondering);
    }
}