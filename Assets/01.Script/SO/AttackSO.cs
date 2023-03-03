using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SO/Attack")]
public class AttackSO : ScriptableObject
{
    public AttackData baseAttackData = null;
}

[System.Serializable]
public class AttackData
{
    public List<int> damages = new List<int>();
    public List<float> delays = new List<float>();
}