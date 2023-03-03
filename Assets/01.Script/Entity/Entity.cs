using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Entity : MonoBehaviour
{
    private List<EntityAction> _entityActions = new List<EntityAction>();

    protected AgentInput _agentInput = null;
    protected AgentRenderer _agentRenderer = null;
    protected AgentAnimation _agentAnimation = null;

    #region 프로퍼티
    public AgentInput AgentInput => _agentInput;
    public AgentRenderer AgentRenderer => _agentRenderer;
    public AgentAnimation AgentAnimation => _agentAnimation;
    #endregion

    protected virtual void Awake()
    {
        List<EntityAction> tempActions = new List<EntityAction>(GetComponents<EntityAction>());
        _entityActions = (from action in tempActions orderby action.ActionType ascending select action).ToList();
        _agentInput = GetComponent<AgentInput>();
        _agentRenderer = transform.Find("AgentRenderer").GetComponent<AgentRenderer>();
        _agentAnimation = _agentAnimation.GetComponent<AgentAnimation>();
    }
    /// <summary>
    /// 인자로 넘긴 액션들을 강제 종료
    /// </summary>
    /// <param name="types"></param>
    public void EntityActionExit(params ActionType[] types)
    {
        for (int i = 0; i < types.Length; i++)
            GetEntityAction(types[i]).ActionExit();
    }

    /// <summary>
    /// 인자로 넘긴 액션들의 잠금을 value로 설정함
    /// </summary>
    /// <param name="value"></param>
    /// <param name="types"></param>
    public void EntityActionLock(bool value, params ActionType[] types)
    {
        for (int i = 0; i < types.Length; i++)
            GetEntityAction(types[i]).Locked = value;
    }

    /// <summary>
    /// 인자로 넘긴 액션이 잠겨있다면 true 반환
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public bool EntityActionLockCheck(params ActionType[] types)
    {
        for (int i = 0; i < types.Length; i++)
            if (GetEntityAction(types[i]).Locked)
                return true;
        return false;
    }

    /// <summary>
    /// 인자로 넘긴 액션을 하고있는 중이라면 true 반환
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public bool EntityActionCheck(params ActionType[] types)
    {
        for (int i = 0; i < types.Length; i++)
            if (GetEntityAction(types[i]).Excuting)
                return true;
        return false;
    }

    public EntityAction GetEntityAction(ActionType type)
    {
        if (type == ActionType.None)
        {
            Debug.LogError("type이 None인데?");
            return null;
        }
        return _entityActions[((int)type) - 1];
    }
}
