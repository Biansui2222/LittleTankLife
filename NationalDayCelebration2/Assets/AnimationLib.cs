using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLib  {
    /// <summary>
    /// 0.5秒一呼吸，呼吸6次
    /// </summary>
    /// <param name="setSthState"></param>
    /// <param name="AniEnd">记得在此函数删除Breath的生成的循环函数</param>
    /// <param name="state">起始时状态为true</param>
    /// <returns>生成的循环更新函数</returns>
    public static System.Action Breath(System.Action<bool> setSthState, System.Action AniEnd, bool state=true)
    {
        Debug.Assert(setSthState != null);
        Debug.Assert(AniEnd != null);
        float nextActionTime =0;
        float times = 6;
        setSthState(state);
        return () => {
            if (times <= 0)
            {
                AniEnd();
                return;
            }
            if (nextActionTime == 0)
            {
                //Debug.Log("Breath - init");
                nextActionTime = Time.time + 0.5f;
            }
            if (nextActionTime < Time.time)
            {
                //Debug.Log("Breath - time--");
                times--;
                setSthState(!state);
                state = !state;
                nextActionTime = Time.time + 0.5f;
            }
        };
    }

}
