using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PopupData")]
public class PopupDataSO : ScriptableObject
{
    public PopupType popupType = PopupType.None;
    public float fontSize = 3.5f;
    public Color color = Color.white;
    public float duration = 0.3f;

    [Header("특수 아이들")]
    public float punchSize = 2f;
}
