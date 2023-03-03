using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AgentAttack
{
    private Coroutine _attackCoroutine;
    private Coroutine _delayCoroutine;

    private bool _delayLock = false;

    [SerializeField]
    private int _maxAttackIndex = 2;
    private int _attackIndex = -1;

    public override void ActionExit()
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
    }

    public override void TryAttack()
    {
        if (_delayLock || _locked)
            return;
        _attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        _delayLock = true;
        _attackIndex = (_attackIndex + 1) % _maxAttackIndex;
        _entity.EntityActionExit(ActionType.Move);
        _entity.EntityActionLock(true, ActionType.Move);
        string aniName = $"BaseAttack{_attackIndex}";
        _entity.AgentAnimation.AnimationForcePlay(aniName);
        yield return StartCoroutine(_entity.AgentAnimation.WaitCoroutine(aniName, 0));
        _entity.EntityActionLock(false, ActionType.Move);
        yield return new WaitForSeconds(_baseAttackSO.baseAttackData.delays[_attackIndex]);
        _delayLock = false;
    }
}
