using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/DialogData")]
public class DialogDataSO : ScriptableObject
{
    public List<DialogData> dialogDatas = new List<DialogData>();
    public DialogDataSO nextData = null;
}

[System.Serializable]
public class DialogData
{
    public NPCDataSO characterData = null;

    public float nextCharDelay = 0.02f;
    public List<string> dialogs = new List<string>();
    public string endActionKey = "";
}