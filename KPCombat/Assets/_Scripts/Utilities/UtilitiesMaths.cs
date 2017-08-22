using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Utilities
{
    public static System.Random R;

    public static void InitializeMath()
    {
        R = new System.Random();
    }

    public static float NextFloat(float minValue = 0, float maxValue = 1)
    {
        double range = maxValue - (double)minValue;
        double sample = R.NextDouble();
        double scaled = (sample * range) + minValue;

        return (float)scaled;
    }

    public static int NextInt(int minValue, int maxValue)
    {
        return R.Next(minValue, maxValue);
    }

    public static Vector3 NextVector2(Vector2 min, Vector2 max)
    {
        Vector2 val = Vector2.zero;

        val.x = NextFloat(min.x, max.x);
        val.y = NextFloat(min.y, max.y);

        return val;
    }

    public static Vector3 NextVector3(Vector3 min, Vector3 max)
    {
        Vector3 val = Vector3.zero;

        val.x = NextFloat(min.x, max.x);
        val.y = NextFloat(min.y, max.y);
        val.z = NextFloat(min.z, max.z);

        return val;
    }

    public static int Combination(int n, int r)
    {  
        int Comb = 0;  
        Comb = (int)(Factorial(n) / (Factorial(r) * Factorial(n - r)));  
        return Comb;  
    }

    public static long Factorial(int num)
    {  
        long fact = 1;  
        for (int i = 2; i <= num; i++)
        {  
            fact = fact * i;  
        }  
        return fact;  
    }

    public static float Normalize(float val, float oldMax, float oldMin, float newMax, float newMin)
    {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newValue = (((val - oldMin) * newRange) / oldRange) + newMin;

        return newValue;
    }

    public static TimeSpan ConvertSecondsToTimeSpan(double value)
    {
        return TimeSpan.FromSeconds(value);
    }
}
