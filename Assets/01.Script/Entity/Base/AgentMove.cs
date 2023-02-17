using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMove : MonoBehaviour
{
    [SerializeField]
    private MovementDataSO _movementSO = null;
    private Rigidbody2D _rigid = null;

    protected float _currentVelocity = 3;
    protected Vector2 _movementDirection;

    public UnityEvent<float> OnvelocityChange; //속도 바뀔 때 실행될 이벤트

    private bool _moveLock = false;
    public bool MoveLock { get => _moveLock; set => _moveLock = value; }

    private void Awake()
    {
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
            _currentVelocity += _movementSO.accaleration * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= _movementSO.deceleration * Time.deltaTime;
        }
        return Mathf.Clamp(_currentVelocity, 0, _movementSO.maxSpeed);
    }

    private void FixedUpdate()
    {
        if (_moveLock)
            return;
        OnvelocityChange?.Invoke(_currentVelocity);
        _rigid.velocity = _movementDirection * _currentVelocity;
    }

    public void StopImmediatelly()
    {
        _currentVelocity = 0;
        _rigid.velocity = Vector2.zero;
    }
}
