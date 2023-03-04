using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMove : EntityAction
{
    private Rigidbody2D _rigid = null;

    protected float _currentVelocity = 3;
    protected Vector2 _movementDirection;

    private Vector3 _moveAmount = Vector3.zero;
    private Vector3 _extraMoveAmount = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void MoveAgent(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            //가고자 하는 방향과 반대면 가속 중지
            if (Vector2.Dot(movementInput, _movementDirection) < 0)
            {
                _currentVelocity = 0;
            }
            _movementDirection = movementInput.normalized;
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }

    private float CalculateSpeed(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += _entity.movementDataSO.accaleration * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= _entity.movementDataSO.deceleration * Time.deltaTime;
        }
        return Mathf.Clamp(_currentVelocity, 0, _entity.movementDataSO.maxSpeed);
    }

    private void FixedUpdate()
    {
        _moveAmount = (_locked == false) ? _movementDirection * _currentVelocity : Vector2.zero;
        _rigid.velocity = _moveAmount + _extraMoveAmount;
    }

    public void VelocitySetMove(float? x = null, float? y = null)
    {
        if (x == null)
            x = _moveAmount.x;
        if (y == null)
            y = _moveAmount.y;
        _moveAmount = new Vector3(x.Value, y.Value, 0f);
    }

    public void VelocitySetExtra(float? x = null, float? y = null)
    {
        if (x == null)
            x = _extraMoveAmount.x;
        if (y == null)
            y = _extraMoveAmount.y;
        _extraMoveAmount = new Vector3(x.Value, y.Value, 0f);
    }

    public void VeloCityResetImm(bool x = false, bool y = false)
    {
        if (x)
        {
            _moveAmount.x = _extraMoveAmount.x = 0f;
        }
        if (y)
        {
            _moveAmount.y = _extraMoveAmount.y = 0f;
        }
    }

    public override void ActionExit()
    {
        VeloCityResetImm(true,true);
        _rigid.velocity = Vector2.zero;
    }
}
