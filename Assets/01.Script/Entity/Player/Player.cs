using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private bool _dirSetLock = false;
    public bool DirSetLock { get => _dirSetLock; set => _dirSetLock = value; }

    public Vector2 GetDir()
    {
        float x = _agentAnimation.animator.GetFloat("Horizontal");
        float y = _agentAnimation.animator.GetFloat("Vertical");
        return new Vector2(x, y).normalized;
    }

    public void DirSet(Vector2 input)
    {
        if (_dirSetLock)
            return;
        _agentAnimation.DirSet(input.normalized);
        _agentRenderer.Flip(input.x);
        /*Vector2 distance = input - (Vector2)transform.position;
        _agentAnimation.DirSet(distance.normalized);
        _agentRenderer.Flip(Vector2.Dot(_agentRenderer.transform.right, distance));*/
    }
}
