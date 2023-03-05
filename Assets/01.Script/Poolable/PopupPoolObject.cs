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

    public void PopupText(Vector2 startPos, Vector2 lastPos, Color color, float duration, int fontSize) // ���� ������, ������ ������, ����, ��Ʈ������, ������
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

    public void PunchPopup(PopupDataSO data, string text, Vector2 pos, Action Callback = null)
    {
        _text.SetText(text);
        _text.fontSize = data.fontSize;
        _text.color = data.color;
        transform.position = pos;
        transform.localScale = Vector2.one * data.punchSize;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOScale(1f, data.duration));
        _seq.AppendCallback(() => { Callback?.Invoke(); });
        _seq.Append(_text.DOFade(0f, data.fadeDuration));
        _seq.AppendCallback(() => { PoolManager.Push(poolType, gameObject); });
    }

    public void DropPopup(PopupDataSO data, string text, Vector2 pos, Vector2 endPos, Action Callback = null)
    {
        _text.SetText(text);
        _text.fontSize = data.fontSize;
        _text.color = data.color;
        transform.position = pos;
        transform.localScale = Vector2.one * data.punchSize;

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOJump(endPos, 2f, 1, data.duration));
        _seq.AppendCallback(() => { Callback?.Invoke(); });
        _seq.Append(_text.DOFade(0f, data.fadeDuration));
        _seq.AppendCallback(() => { PoolManager.Push(poolType, gameObject); });
    }
}