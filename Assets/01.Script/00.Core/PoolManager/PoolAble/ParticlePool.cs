using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : PoolAbleObject
{
    [SerializeField] private float poolTime;
    public override void Init_Pop()
    {
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(Wait());
    }

    public override void Init_Push()
    {

    }

    public void Set(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);//transform.position = info.point + info.normal * 0.15f;
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(poolTime);
        PoolManager.Push(PoolType, gameObject);
    }
}
