using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Entity
{
    [SerializeField]
    private NormalEnemyDataSO _enemySO = null;
    public NormalEnemyDataSO EnemySO => _enemySO;

    private Player _target = null;
    public Player Target => _target;

    private StateMachine<NormalEnemy> _fsm = null;

    private void Start()
    {
        _target = GameObject.FindObjectOfType<Player>();
        _fsm = new StateMachine<NormalEnemy>(this, new EnemyIdleState());
        _fsm.AddStateList(new EnemyAttackState());
        _fsm.AddStateList(new EnemyMoveState());
    }

    private void Update()
    {
        _fsm.Execute();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_enemySO == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemySO.detectLength);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _enemySO.attackLength);
    }
#endif
}
