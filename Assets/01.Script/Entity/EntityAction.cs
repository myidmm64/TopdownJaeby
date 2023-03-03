using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAction : MonoBehaviour
{
    [SerializeField]
    private ActionType _actionType = ActionType.None; // ���� �׼��ΰ���
    public ActionType ActionType => _actionType;

    protected Entity _entity = null;
    protected Player _player => _entity as Player;

    protected bool _locked = false; // ��� ������ �׼��̴�?
    public bool Locked { get => _locked; set => _locked = value; }

    protected bool _excuting = false; // �׼� ������?
    public bool Excuting => _excuting;

    protected virtual void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    public abstract void ActionExit(); // �׼� ���� ����
}
