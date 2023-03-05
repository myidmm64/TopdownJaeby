using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/HitData")]
public class HitDataSO : ScriptableObject
{
    [Header("카메라")]
    public float emplitude = 10f;
    public float intensity = 3.5f;
    public float cameraDuration = 0.15f;
    [Header("타임")]
    public float startScale = 0.2f;
    public float hitTimeDuration = 0.02f;
    public float DieTimeDuration = 0.05f;
    [Header("피격")]
    public PoolType hitEffect = PoolType.None;
}
