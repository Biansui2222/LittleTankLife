using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T RandomIn<T>(params T[] args)
    {
        return args[Random.Range(0, args.Length)];
    }

    public static T RandomSelect<T>(this UnityEngine.Random r, params T[] args)
    {
        return args[Random.Range(0, args.Length)];
    }
}
