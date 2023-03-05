using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AgentHP : MonoBehaviour
{
    [SerializeField]
    private HitDataSO _hitDataSO = null;

    [SerializeField]
    private Slider _hpSlider = null;
    [SerializeField]
    private TextMeshProUGUI _hpText = null;
    [SerializeField]
    private float _hpDownTime = 0.15f;
    [SerializeField]
    private int _maxHP = 0;
    private int _curHP = 0;
    private int HP
    {
        get => _curHP;
        set
        {
            _curHP = value;
            _curHP = Mathf.Clamp(_curHP, 0, _maxHP);
            if (_hpText != null)
                _hpText.SetText($"{_curHP} / {_maxHP}");
            if (_hpCoroutine != null)
                StopCoroutine(_hpCoroutine);
            _hpCoroutine = StartCoroutine(HPDownCoroutine(_hpSlider.value, _curHP));
        }
    }
    private Coroutine _hpCoroutine = null;

    private void Start()
    {
        _hpSlider.maxValue = _maxHP;
        _hpSlider.minValue = 0;
        HP = _maxHP;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            HP -= 10;
        }
    }

    private IEnumerator HPDownCoroutine(float start, float end)
    {
        // 20, 10
        bool down = start > end;
        _hpSlider.value = start;
        float time = 0f;
        while(time <= 1f)
        {
            time += Time.deltaTime * (1f / _hpDownTime);
            float delta = (start - end) * time;
            _hpSlider.value = start - delta;
            yield return null;
        }
    }

    public void Hit(int amount)
    {
        HP -= amount;
        PoolManager.Pop(_hitDataSO.hitEffect).transform.position = transform.position;
        CameraManager.Instance.CameraShake(_hitDataSO.emplitude, _hitDataSO.intensity, _hitDataSO.cameraDuration);
        TimeManager.Instance.TimeScaleChange(_hitDataSO.startScale, 1f, (HP == 0) ? _hitDataSO.DieTimeDuration : _hitDataSO.hitTimeDuration);
        if (HP == 0)
            Die();
    }

    private void Die()
    {

    }
}
