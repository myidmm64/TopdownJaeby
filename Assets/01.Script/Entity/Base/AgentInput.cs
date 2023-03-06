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
    [SerializeField]
    private UnityEvent OnInteractKeyPress = null;

    private Vector2 _moveInputVec = Vector2.zero;
    public Vector2 MoveInputVec => _moveInputVec;
    public Vector2 MoveInputVecNorm => _moveInputVec.normalized;

    private Vector2 _pointerInputVec = Vector2.zero;
    public Vector2 PointerInputVec => _pointerInputVec;
    public Vector2 PointerInputVecNorm => _pointerInputVec.normalized;
    public Vector2 PointerDistance => _pointerInputVec - (Vector2)transform.position;

    private Entity _entity = null;
    private Camera _cam = null;

    private void Awake()
    {
        _entity = GetComponent<Entity>();
        _cam = Camera.main;
    }

    private void Update()
    {
        GetPointerInput();
        GetMovementInput();
        GetDashInput();
        GetInteractInput();
        GetAttackInput();
    }

    private void GetInteractInput()
    {
        if (Input.GetMouseButtonDown(0))
            OnInteractKeyPress?.Invoke();
    }

    private void GetAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
            OnAttackKeyPress?.Invoke();
    }

    private void GetDashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnDashKeyPress?.Invoke();
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        _pointerInputVec = _cam.ScreenToWorldPoint(mousePos);
        OnPointerPositionChanged.Invoke(_pointerInputVec);
    }

    private void GetMovementInput()
    {
        if (_entity.EntityActionLockCheck(ActionType.Move))
            return;
        _moveInputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMovementKeyPress?.Invoke(_moveInputVec);
    }
}
