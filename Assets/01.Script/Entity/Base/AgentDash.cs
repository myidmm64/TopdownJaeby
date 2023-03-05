using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentDash : EntityAction
{
    private bool _delayLock = false;
    private Coroutine _dashCoroutine = null;
    private Coroutine _dashSliderCoroutine = null;
    [SerializeField]
    private Slider _dashSlider = null;

    private void Start()
    {
        if (_dashSlider != null)
            _dashSlider.maxValue = _entity.movementDataSO.dashChargetime;
    }

    public void TryDash()
    {
        if (_locked || _delayLock || _entity.AgentInput.MoveInputVecNorm.sqrMagnitude == 0f)
            return;
        if (_dashCoroutine != null)
            StopCoroutine(_dashCoroutine);
        if (_dashSliderCoroutine != null && _dashSlider != null)
        {
            StopCoroutine(_dashSliderCoroutine);
            _dashSlider.value = _dashSlider.maxValue;
        }
        _dashCoroutine = StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        _delayLock = true;
        _entity.EntityActionExit(ActionType.Move);
        _entity.EntityActionLock(true, ActionType.Move);
        Vector2 dashAmount = _entity.AgentInput.MoveInputVecNorm * _entity.movementDataSO.dashPower;
        _entity.GetEntityAction<AgentMove>(ActionType.Move).VelocitySetExtra(dashAmount.x, dashAmount.y);
        _entity.AgentAnimation.AnimationForcePlay("Dash");
        yield return StartCoroutine(_entity.AgentAnimation.WaitCoroutine("Dash", 0));
        _entity.GetEntityAction<AgentMove>(ActionType.Move).VelocitySetExtra(0f, 0f);
        _entity.EntityActionLock(false, ActionType.Move);
        if (_dashSlider != null)
            _dashSliderCoroutine = StartCoroutine(SliderCoroutine());
        yield return new WaitForSeconds(_entity.movementDataSO.dashChargetime);
        _delayLock = false;
    }

    private IEnumerator SliderCoroutine()
    {
        _dashSlider.maxValue = _entity.movementDataSO.dashChargetime;
        float delta = 0f;
        while (delta <= _dashSlider.maxValue)
        {
            _dashSlider.value = delta;
            delta += Time.deltaTime;
            yield return null;
        }
        _dashSlider.value = _dashSlider.maxValue;
    }

    public override void ActionExit()
    {
        _entity.GetEntityAction<AgentMove>(ActionType.Move).VelocitySetExtra(0f, 0f);
        if (_dashCoroutine != null)
        {
            StopCoroutine(_dashCoroutine);
            _delayLock = false;
        }
        if (_dashSliderCoroutine != null && _dashSlider != null)
        {
            StopCoroutine(_dashSliderCoroutine);
            _dashSlider.value = _dashSlider.maxValue;
        }
    }
}
