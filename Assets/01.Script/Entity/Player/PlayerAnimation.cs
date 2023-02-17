using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(Vector2 input)
    {
        _animator.SetBool("Move", input.sqrMagnitude > 0f);
        if(input.sqrMagnitude > 0f)
        {
            _animator.SetFloat("Horizontal", input.x);
            _animator.SetFloat("Vertical", input.y);
        }
        if (input.x < 0f)
            _spriteRenderer.flipX = true;
        else if (input.x > 0f)
            _spriteRenderer.flipX = false;
    }

    public void DashAnimation()
    {
        _animator.SetTrigger("Dash");
    }
}