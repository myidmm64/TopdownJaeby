using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimation : MonoBehaviour
{
    protected Animator _animator = null;
    public Animator animator => _animator;
    protected SpriteRenderer _spriteRenderer = null;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void DirSet(Vector2 input)
    {
        if (input.sqrMagnitude > 0f)
        {
            _animator.SetFloat("Horizontal", input.x);
            _animator.SetFloat("Vertical", input.y);
        }
    }

    public void MoveAnimation(Vector2 input)
    {
        _animator.SetBool("Walk", input.sqrMagnitude > 0f);
    }

    public IEnumerator WaitCoroutine(string name, int layerIndex)
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(name) == false);
    }

    public void AnimationPlayAndWait(string name, int layerIndex, Action Callback)
    {
        AnimationCoroutine(name, layerIndex, Callback);
    }

    private IEnumerator AnimationCoroutine(string name, int layerIndex, Action Callback)
    {
        AnimationForcePlay(name);
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(name) == false);
        Callback?.Invoke();
    }

    public void AnimationForcePlay(string name)
    {
        _animator.Play(name);
        _animator.Update(0);
    }
}
