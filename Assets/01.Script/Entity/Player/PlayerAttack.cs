using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AgentAttack
{
    private Coroutine _attackCoroutine;
    private Coroutine _resetCoroutine;

    private bool _delayLock = false;

    [SerializeField]
    private float _indexResetTime = 0.2f;
    [SerializeField]
    private int _maxAttackIndex = 2;
    private int _attackIndex = -1;

    public override void ActionExit()
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
        if (_resetCoroutine != null)
            StopCoroutine(_resetCoroutine);
        _entity.EntityActionLock(false, ActionType.Move, ActionType.Dash);
        _delayLock = false;
        _attackIndex = -1;
    }

    public override void TryAttack()
    {
        if (_delayLock || _locked)
            return;
        if (_resetCoroutine != null)
            StopCoroutine(_resetCoroutine);
        _attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator IndexResetCoroutine()
    {
        yield return new WaitForSeconds(_indexResetTime);
        _attackIndex = -1;
    }

    private IEnumerator AttackCoroutine()
    {
        _delayLock = true;
        _attackIndex = (_attackIndex + 1) % _maxAttackIndex;
        AttackColliderCreate();
        _entity.EntityActionExit(ActionType.Move, ActionType.Dash);
        _entity.EntityActionLock(true, ActionType.Move, ActionType.Dash);
        string aniName = $"BaseAttack{_attackIndex}";
        _entity.AgentAnimation.AnimationForcePlay(aniName);
        yield return StartCoroutine(_entity.AgentAnimation.WaitCoroutine(aniName, 0));
        _entity.EntityActionLock(false, ActionType.Move, ActionType.Dash);
        yield return new WaitForSeconds(_baseAttackSO.baseAttackData.delays[_attackIndex]);
        _delayLock = false;

        _resetCoroutine = StartCoroutine(IndexResetCoroutine());
    }

    public void AttackColliderCreate()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll((Vector2)transform.position + _player.GetDir(), 1f);
        for (int i = 0; i < targets.Length; i++)
            targets[i].GetComponent<AgentHP>().Hit(_baseAttackSO.baseAttackData.damages[_attackIndex]);
    }
}