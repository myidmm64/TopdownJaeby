using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = null;
    public bool Fliped => _spriteRenderer.flipX;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Flip(Vector2 input)
    {
        if (input.x < 0f)
            _spriteRenderer.flipX = true;
        else if (input.x > 0f)
            _spriteRenderer.flipX = false;
    }
}
