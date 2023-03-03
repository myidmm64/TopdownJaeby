using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<Vector2> OnMovementKeyPress = null;
    [SerializeField]
    private UnityEvent<Vector2> OnPointerPositionChanged = null;
    [SerializeField]
    private UnityEvent OnDashKeyPress = null;
    [SerializeField]
    private UnityEvent OnAttackKeyPress = null;

    private Vector2 _inputVec = Vector2.zero;
    public Vector2 InputVec => _inputVec;
    public Vector2 InputVecNorm => _inputVec.normalized;

    private Entity _entity = null;

    private void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    private void Update()
    {
        GetPointerInput();
        GetMovementInput();
        GetDashInput();
        GetAttackInput();
    }

    private void GetAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
            OnAttackKeyPress?.Invoke();
    }

    private void GetDashInput()
    {
        if (Input.GetMouseButtonDown(1))
            OnDashKeyPress?.Invoke();
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        //매우 안좋음
        Vector2 mouseInWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        OnPointerPositionChanged.Invoke(mouseInWorldPos);
    }

    private void GetMovementInput()
    {
        if (_entity.EntityActionLockCheck(ActionType.Move))
            return;
        _inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMovementKeyPress?.Invoke(_inputVec);
    }
}
