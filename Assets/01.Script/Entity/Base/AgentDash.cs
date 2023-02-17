using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDash : MonoBehaviour
{
    private AgentMove _agentMove = null;
    private AgentInput _agentInput = null;

    private bool _dashing = false;

    private Rigidbody2D _rigid = null;
    [SerializeField]
    private MovementDataSO _moveDataSO = null;

    PlayerAnimation _playerAnimation = null;

    private void Start()
    {
        _agentMove = GetComponent<AgentMove>();
        _agentInput = GetComponent<AgentInput>();
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void Dash()
    {
        if (_dashing)
            return;
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        _dashing = true;
        _agentMove.MoveLock = true;
        _rigid.velocity = _agentInput.InputVecNorm * _moveDataSO.dashPower;
        _playerAnimation.DashAnimation();
        yield return new WaitForSeconds(0.3f);
        _agentMove.MoveLock = false;
        _dashing = false;
    }
}
