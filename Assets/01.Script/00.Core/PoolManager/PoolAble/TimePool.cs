using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePool : PoolAbleObject
{
    [SerializeField] private float timePool;
    public override void Init_Pop()
    {
        StartCoroutine(TimePush());
    }

    public override void Init_Push()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    IEnumerator TimePush()
    {
        yield return new WaitForSeconds(timePool);
        PoolManager.Push(PoolType, gameObject);
    }
}
