using UnityEngine;

public static class StaticMethod
{
    public static string TimeAsString(float time)
    {
        var stringTime = Mathf.Floor(time / 60 ).ToString("00")  + " : " + 
                         Mathf.FloorToInt(time % 60).ToString("00");
        return stringTime;
    }
}
