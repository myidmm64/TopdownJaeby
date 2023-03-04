using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentRenderer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = null;
    private UnityEvent<bool> OnFliped = null;
    private bool _fliped = false;
    public bool Fliped => _spriteRenderer.flipX;

    private Vector3 localSc;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Flip(float dot)
    {
        if ((dot < 0f && _fliped == false) || (dot > 0f && _fliped))
        {
            _fliped = !_fliped;
            localSc = transform.localScale;
            localSc.x *= -1f;
            transform.localScale = localSc;
        }
        OnFliped?.Invoke(_fliped);
    }
}
