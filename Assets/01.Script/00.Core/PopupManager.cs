using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupManager : MonoSingleTon<PopupManager>
{
    public void Popup(string text, Vector2 pos, PopupDataSO data)
    {
        PopupPoolObject popupPoolObj = PoolManager.Pop(PoolType.PopText).GetComponent<PopupPoolObject>();
        switch (data.popupType)
        {
            case PopupType.None:
                break;
            case PopupType.Punch:
                popupPoolObj.PunchPopup(text, pos, data.punchSize, data.duration, data.fontSize, data.color);
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