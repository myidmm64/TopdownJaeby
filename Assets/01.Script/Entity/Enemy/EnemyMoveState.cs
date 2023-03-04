using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : State<NormalEnemy>
{
    public override void Enter()
    {
        _stateMachineOwnerClass.EntityActionLock(false, ActionType.Move);
        _stateMachineOwnerClass.AgentAnimation.MoveAnimation(Vector2.one);
    }

    public override void Execute()
    {
        float distance = Vector2.Distance(_stateMachineOwnerClass.Target.transform.position, _stateMachineOwnerClass.transform.position);
        Vector2 target = _stateMachineOwnerClass.Target.transform.position - _stateMachineOwnerClass.transform.position;
        _stateMachineOwnerClass.GetEntityAction<AgentMove>(ActionType.Move).MoveAgent(target.normalized);
        _stateMachineOwnerClass.AgentRenderer.Flip(target.x);

        if (distance > _stateMachineOwnerClass.EnemySO.detectLength)
            _stateMachine.ChangeState<EnemyIdleState>();
        else if (distance <= _stateMachineOwnerClass.EnemySO.attackLength)
            _stateMachine.ChangeState<EnemyAttackState>();

    }

    public override void Exit()
    {
        _stateMachineOwnerClass.EntityActionExit(ActionType.Move);
        _stateMachineOwnerClass.EntityActionLock(true, ActionType.Move);
        _stateMachineOwnerClass.AgentAnimation.MoveAnimation(Vector2.zero);
    }
}
