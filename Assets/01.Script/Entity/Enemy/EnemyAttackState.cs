using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State<NormalEnemy>
{
    public override void Enter()
    {
        _stateMachine.ChangeState<EnemyMoveState>();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
