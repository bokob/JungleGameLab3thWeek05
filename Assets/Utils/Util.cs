using System;

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
}