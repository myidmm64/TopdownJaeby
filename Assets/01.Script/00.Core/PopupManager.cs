using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PopupManager : MonoSingleTon<PopupManager>
{
    public void Popup(PopupDataSO data, string text, Vector2 pos, Action Callback = null)
    {
        PopupPoolObject popupPoolObj = PoolManager.Pop(PoolType.PopText).GetComponent<PopupPoolObject>();
        switch (data.popupType)
        {
            case PopupType.None:
                break;
            case PopupType.Punch:
                popupPoolObj.PunchPopup(data, text, pos, Callback);
                break;
            case PopupType.Drop:
                break;
            default:
                break;
        }
    }
}

[System.Serializable]
public enum PopupType
{
    None,
    Punch,
    Drop
}