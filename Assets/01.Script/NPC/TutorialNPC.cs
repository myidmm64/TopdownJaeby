using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPC : NPC
{
    [SerializeField]
    private PopupDataSO _popupData = null;
    [SerializeField]
    private List<string> _dialogs = new List<string>();
    private int _index = 0;

    protected override void Start()
    {
        base.Start();
    }

    public void DoDialog()
    {
        if (_index >= _dialogs.Count)
            return;

        PopupManager.Instance.Popup(_popupData, _dialogs[_index], (Vector2)transform.position + Vector2.up * 0.8f, DoDialog);
        _index++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            DoDialog();
    }
}
