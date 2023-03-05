using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : PoolAbleObject
{
    [SerializeField]
    private Animator _animator = null;
    [SerializeField]
    private string _nameStr = "Effect";

    private Coroutine _co = null;

    public override void Init_Pop()
    {
        _co = StartCoroutine(EffectAnimationCoroutine());
    }

    private IEnumerator EffectAnimationCoroutine()
    {
        _animator.Play(_nameStr);
        _animator.Update(0);
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName(_nameStr) == false);
        PoolManager.Push(poolType, gameObject);
    }

    public override void Init_Push()
    {
        if (_co != null)
            StopCoroutine(_co);
    }
}
