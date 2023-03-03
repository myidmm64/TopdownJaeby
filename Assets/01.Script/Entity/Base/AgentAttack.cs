using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentAttack : EntityAction
{
    [SerializeField]
    protected AttackSO _baseAttackSO = null;

    public abstract void TryAttack();
}
