using System;
using System.Collections;
using UnityEngine;

public static class Util
{
    // int -> enum으로 변환
    public static T IntToEnum<T>(int i)
    {
        return (T)(object)i;
    }

    // string -> enum으로 변환
    public static T StringToEnum<T>(string s)
    {
        return (T)Enum.Parse(typeof(T), s);
    }

    // 몇 초 후에 액션 실행
    public static IEnumerator WaitTimeAfterPlayAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}