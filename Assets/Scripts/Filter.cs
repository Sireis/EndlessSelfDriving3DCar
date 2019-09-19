using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Due to lack of something like "where T : ISummable" this cannot be
// put into a tempalte class
public class Vec3Filter
{
    private Vector3[] filterBuffer;
    private int index = 0;

    public Vec3Filter(int length)
    {
        filterBuffer = new Vector3[length];
    }
    public Vector3 GetFilteredValue(Vector3 newValue)
    {
        filterBuffer[index] = newValue;
        index++;
        if(index >= filterBuffer.Length)
        {
            index = 0;
        }

        Vector3 temp = new Vector3();
        for (int i = 0; i < filterBuffer.Length; i++)
        {
           temp += filterBuffer[i];
        }

        return temp / filterBuffer.Length;
    }
}

public class FloatFilter
{
    private float[] filterBuffer;
    private int index = 0;

    public FloatFilter(int length)
    {
        filterBuffer = new float[length];
    }
    public float GetFilteredValue(float newValue)
    {
        filterBuffer[index] = newValue;
        index++;
        if (index >= filterBuffer.Length)
        {
            index = 0;
        }

        float temp = new float();
        for (int i = 0; i < filterBuffer.Length; i++)
        {
            temp += filterBuffer[i];
        }

        return temp / filterBuffer.Length;
    }
}
