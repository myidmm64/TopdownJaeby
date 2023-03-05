using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/NormalEnemyData")]
public class NormalEnemyDataSO : ScriptableObject
{
    public float detectLength = 5f;
    public float attackLength = 2f;
    public GameObject bullet = null;

}
