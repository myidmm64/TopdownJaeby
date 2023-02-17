using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Movement/Data")]
public class MovementDataSO : ScriptableObject
{
    [Header("�̵�")]
    public float speed = 3f;
    public float maxSpeed = 4f;

    public float accaleration = 1f;
    public float deceleration = 1f;

    [Header("���")]
    public float dashChargetime = 0.3f;
    public float dashPower = 5f;
}
