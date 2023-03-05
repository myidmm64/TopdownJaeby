using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleTon<TimeManager>
{
    private Coroutine _timeCoroutine = null;
    
    public void ResetCoroutine()
    {
        if (_timeCoroutine != null)
            StopCoroutine(_timeCoroutine);
    }

    public void TimeScaleChange(float startScale, float endScale, float realTime)
    {
        ResetCoroutine();
        _timeCoroutine = StartCoroutine(TimeScaleChangeCoroutine(startScale, realTime, endScale));
    }



    private IEnumerator TimeScaleChangeCoroutine(float startScale, float realTime, float endScale)
    {
        Time.timeScale = startScale;
        yield return new WaitForSecondsRealtime(realTime);
        Time.timeScale = endScale;
    }
}
