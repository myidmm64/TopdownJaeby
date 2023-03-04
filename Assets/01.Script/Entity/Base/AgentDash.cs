using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDash : EntityAction
{
    private bool _delayLock = false;
    private Coroutine _dashCoroutine = null;

    public void TryDash()
    {
        if (_locked || _delayLock || _entity.AgentInput.MoveInputVecNorm.sqrMagnitude == 0f)
            return;
        if (_dashCoroutine != null)
            StopCoroutine(_dashCoroutine);
        _dashCoroutine = StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        _delayLock = true;
        _entity.EntityActionExit(ActionType.Move);
        _entity.EntityActionLock(true, ActionType.Move);
        Vector2 dashAmount = _entity.AgentInput.MoveInputVecNorm * _entity.movementDataSO.dashPower;
        _entity.GetEntityAction<AgentMove>(ActionType.Move).VelocitySetExtra(dashAmount.x, dashAmount.y);
        _entity.AgentAnimation.AnimationForcePlay("Dash");
        yield return StartCoroutine(_entity.AgentAnimation.WaitCoroutine("Dash", 0));
        _entity.GetEntityAction<AgentMove>(ActionType.Move).VelocitySetExtra(0f, 0f);
        _entity.EntityActionLock(false, ActionType.Move);
        yield return new WaitForSeconds(_entity.movementDataSO.dashChargetime);
        _delayLock = false;
    }

    public override void ActionExit()
    {
        _entity.GetEntityAction<AgentMove>(ActionType.Move).VelocitySetExtra(0f, 0f);
        if (_dashCoroutine != null)
        {
            StopCoroutine(_dashCoroutine);
            _delayLock = false;
        }
    }
}
