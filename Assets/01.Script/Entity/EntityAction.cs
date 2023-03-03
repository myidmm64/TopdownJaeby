using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAction : MonoBehaviour
{
    [SerializeField]
    private ActionType _actionType = ActionType.None; // 무슨 액션인가요
    public ActionType ActionType => _actionType;

    protected Entity _entity = null;
    protected Player _player => _entity as Player;

    protected bool _locked = false; // 사용 가능한 액션이니?
    public bool Locked { get => _locked; set => _locked = value; }

    protected bool _excuting = false; // 액션 실행중?
    public bool Excuting => _excuting;

    protected virtual void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    public abstract void ActionExit(); // 액션 강제 종료
}
