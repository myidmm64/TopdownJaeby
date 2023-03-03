using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : AgentAnimation
{

    public void DashAnimation()
    {
        _animator.SetTrigger("Dash");
    }
}