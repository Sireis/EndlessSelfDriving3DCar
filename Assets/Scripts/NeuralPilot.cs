using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralPilot : Pilot
{
    NeuralNet leftNet;
    NeuralNet rightNet;
    NeuralNet accelerateNet;

    public string pathToNetFolder;

    private const int leftRightFilterLength = 1;
    private const int upDownFilterLength = 1;

    private float[] leftRightFilter = new float[leftRightFilterLength];
    private float[] upDownFilter = new float[upDownFilterLength];


    protected override void Initialize()
    {
        leftNet = new NeuralNet(pathToNetFolder + "\\0.net");
        rightNet = new NeuralNet(pathToNetFolder + "\\1.net");
        accelerateNet = new NeuralNet(pathToNetFolder + "\\2.net");

        for (int i = 0; i < leftRightFilterLength; i++)
        {
            leftRightFilter[i] = 0;
        }

        for (int i = 0; i < upDownFilterLength; i++)
        {
            upDownFilter[i] = 0;
        }
    }

    protected override float DetermineLeftRight()
    {
        float l = leftNet.Evaluate(carInfo, 5);
        float r = rightNet.Evaluate(carInfo, 5);

        float left = (l > 0.5f && l > r) ? 1.0f : 0.0f;
        float right = (r > 0.5f && r > l) ? -1.0f : 0.0f;
        
        return AddToFilter(leftRightFilter, leftRightFilterLength, left + right);
    }

    protected override float DetermineAccelerateBrake()
    {
        float f = accelerateNet.Evaluate(carInfo, 6);

        float newVal = (f > 0.5f) ? 1.0f : -1.0f;
        return AddToFilter(upDownFilter, upDownFilterLength, newVal);
    }

    private float AddToFilter(float[] filterBuffer, int filterLength, float value)
    {
        for (int i = 0; i < filterLength - 1; i++)
        {
            filterBuffer[i] = filterBuffer[i + 1];
        }

        filterBuffer[filterLength - 1] = value;

        float result = 0.0f;

        for (int i = 0; i < filterLength; i++)
        {
            result += filterBuffer[i];
        }

        return result / filterLength;
    }
}
