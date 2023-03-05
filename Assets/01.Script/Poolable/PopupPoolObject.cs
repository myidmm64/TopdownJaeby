using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class PopupPoolObject : PoolAbleObject
{
    [SerializeField]
    private TextMeshPro _text = null;
    private MeshRenderer _meshRenderer = null;

    private Sequence _seq = null;

    public override void Init_Pop()
    {
        if (_meshRenderer == null)
        {
            _meshRenderer = _text.GetComponent<MeshRenderer>();
        }
    }

    public override void Init_Push()
    {
        StopAllCoroutines();

        if (_seq != null)
        {
            _seq.Kill();
        }

        transform.position = Vector2.zero;
        transform.localScale = Vector2.one;
        _text.color = Color.white;
    }

    public void PopupText(Vector2 startPos, Vector2 lastPos, Color color, float duration, int fontSize) // 시작 포지션, 마지막 포지션, 색깔, 폰트사이즈, 사이즈
    {
        transform.position = startPos;
        _text.color = color;
        _text.fontSize = fontSize;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOMove(lastPos, duration));
        _seq.Join(_text.DOFade(0, duration));
        _seq.AppendCallback(() =>
        {
            PoolManager.Push(PoolType, gameObject);
        });
    }

    public void PunchPopup(string text, Vector2 pos, float size, float duration, float fontSize, Color color, Action Callback = null)
    {
        _text.SetText(text);
        _text.fontSize = fontSize;
        _text.color = color;
        transform.position = pos;
        transform.localScale = Vector2.one * size;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOScale(1f, duration));
        _seq.AppendCallback(() =>
        {
            Callback?.Invoke();
            PoolManager.Push(poolType, gameObject);
        });
    }

    public void DropPopup(string text, Vector2 pos, Vector2 endPos, float duration, float fontSize, Color color, Action Callback = null)
    {
        _text.SetText(text);
        _text.fontSize = fontSize;
        _text.color = color;
        transform.position = pos;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOJump(endPos, 2f, 1, duration));
        _seq.AppendCallback(() =>
        {
            Callback?.Invoke();
            PoolManager.Push(poolType, gameObject);
        });
    }
}