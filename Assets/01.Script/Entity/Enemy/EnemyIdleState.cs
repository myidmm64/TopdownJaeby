using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State<NormalEnemy>
{
    public override void Enter()
    {
        _stateMachineOwnerClass.AgentAnimation.AnimationForcePlay("Idle");
    }

    public override void Execute()
    {
        float distance = Vector2.Distance(_stateMachineOwnerClass.Target.transform.position, _stateMachineOwnerClass.transform.position);
        if (distance <= _stateMachineOwnerClass.EnemySO.detectLength)
            _stateMachine.ChangeState<EnemyMoveState>();
        else if (distance <= _stateMachineOwnerClass.EnemySO.attackLength)
            _stateMachine.ChangeState<EnemyAttackState>();
    }

    public override void Exit()
    {
    }
}
