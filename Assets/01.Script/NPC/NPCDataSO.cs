using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/NPCData")]
public class NPCDataSO : ScriptableObject
{
    public Sprite faceSprite = null;
    public string npcName = "";
    public string subName = "";
    public Color subColor = Color.white;
}
