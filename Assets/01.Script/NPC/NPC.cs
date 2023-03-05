using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private NPCDataSO _npcDataSO = null;
    [SerializeField]
    private DialogDataSO _dialogDataSO = null;

    [SerializeField]
    private TextMeshProUGUI _nameText = null;
    [SerializeField]
    private TextMeshProUGUI _subNameText = null;

    [SerializeField]
    private GameObject _doInteractObj = null;

    private bool _dialoging = false;

    #region 프로퍼티
    public NPCDataSO npcDataSO => _npcDataSO;
    public DialogDataSO dialogDataSO => _dialogDataSO;
    #endregion

    private void Start()
    {
        _nameText.SetText(_npcDataSO.npcName);
        _subNameText.SetText(_npcDataSO.subName);
        _subNameText.color = _npcDataSO.subColor;
        _doInteractObj.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TryDialog();
    }

    public void TryDialog()
    {
        if (_doInteractObj.activeSelf == false || _dialoging)
            return;
        _dialoging = true;
        DialogManager.Instance.DialogStart(_dialogDataSO, () => { _dialogDataSO = _dialogDataSO.nextData; _dialoging = false; });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _doInteractObj.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _doInteractObj.SetActive(false);
        }
    }
}
