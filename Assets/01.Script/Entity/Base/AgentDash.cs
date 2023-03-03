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
        yield return new WaitForSeconds(0.3f);
    }
}
