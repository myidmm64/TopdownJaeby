using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PoolManager
{
    private static PoolDataSO poolData = null;//풀링 데이터
    private static Dictionary<PoolType, LocalPoolManager> localPoolDic = new();//PoolType으로 검색하기 위한 딕셔너리
    private static GameObject root = null;

    private static void Init()
    {
        foreach (LocalPoolManager manager in localPoolDic.Values)
        {
            manager.Destroy();
        }
        localPoolDic.Clear();

        poolData = ResourceManager.Load<PoolDataSO>("Core/Data/PoolData");

        GameObject newRoot = new();
        newRoot.name = "@POOL_ROOT";
        root = newRoot;

        for (int i = 0; i < poolData.poolDatas.Length; i++)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(root.transform);
            LocalPoolManager localPool = obj.AddComponent<LocalPoolManager>();
            PoolDataSO.PoolData data = poolData.poolDatas[i];
            localPool.Init(data.InitCount, data.PoolAbleObject, data.PoolType);
            localPoolDic.Add(data.PoolType, localPool);
            localPool.name = $"POOL_LOCAL[{data.PoolType}]";
        }
    }

    /// <summary>
    /// Type에 맞는 오브젝트 꺼내오기
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static GameObject Pop(PoolType type)
    {
        if (root == null) Init();
        return localPoolDic[type].Pop().gameObject;
    }
    /// <summary>
    /// Type에 맞게 오브젝트 넣기
    /// </summary>
    /// <param name="type"></param>
    /// <param name="obj"></param>
    public static void Push(PoolType type, GameObject obj)
    {
        if (root == null) Init();
        localPoolDic[type].Push(obj.GetComponent<PoolAbleObject>());
    }

}