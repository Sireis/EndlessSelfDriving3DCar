using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNet
{
    private int inputCount;
    private int numberOfLayers;

    private float[] lastWeights;
    private float lastBias;

    private Matrix[] weights;
    private Matrix[] biases;
    private Matrix u;

    public NeuralNet(int inputCount, int numberOfLayers)
    {
        this.inputCount = inputCount;
        this.numberOfLayers = numberOfLayers;
    }

    public NeuralNet(string pathToFile)
    {
        LoadFromFile(pathToFile);
    }

    public float Evaluate(float[] input, int size)
    {
        Matrix m = new Matrix(size, 1);

        for (int i = 0; i < m.dimensionY; i++)
        {
            m[i, 0] = input[i];
        }

        return Evaluate(m);
    }

    public float Evaluate(Matrix Mx1)
    {
        Matrix temp = new Matrix(inputCount, 1);

        for (int i = 0; i < inputCount; i++)
        {
            temp[i,0] = Mx1[i,0];
        }

        for (int i = 0; i < numberOfLayers; i++)
        {
            temp = weights[i] * temp - biases[i];
            temp = LogisticFunction(temp);
        }

        float f = 0.0f;
        for (int i = 0; i < inputCount; i++)
        {
            f += lastWeights[i] * temp[i, 0];
        }

        f -= lastBias;
        f = 1 / (1 + Mathf.Exp(-f));

        return f;
    }

    private Matrix LogisticFunction(Matrix temp)
    {
        Matrix m = new Matrix(temp.dimensionY, temp.dimensionX);

        for (int i = 0; i < m.dimensionY; i++)
        {
            for (int j = 0; j < m.dimensionX; j++)
            {
                m[i, j] = 1 / (1 + Mathf.Exp(-temp[i, j]));
            }
        }

        return m;
    }

    public void LoadFromFile(string pathToFile)
    {
        if(!File.Exists(pathToFile))
        {
            return;
        }

        byte[] buffer = File.ReadAllBytes(pathToFile);

        int n_int = sizeof(int);
        int n_float = sizeof(float);

        int ptr = 0;

        inputCount = System.BitConverter.ToInt32(buffer, ptr);
        ptr += n_int;
        numberOfLayers = System.BitConverter.ToInt32(buffer, ptr);
        ptr += n_int;

        weights = new Matrix[numberOfLayers];
        
        for (int i = 0; i < numberOfLayers; ++i)
        {
            weights[i] = new Matrix(inputCount, inputCount);

            for (int j = 0; j < inputCount; ++j)
            {
                for (int k = 0; k < inputCount; ++k)
                {                    
                    weights[i][j,k] = System.BitConverter.ToSingle(buffer, ptr);
                    ptr += n_float;
                }
            }
        }

        biases = new Matrix[numberOfLayers];

        for (int i = 0; i < numberOfLayers; ++i)
        {
            biases[i] = new Matrix(inputCount, 1);

            for (int j = 0; j < inputCount; ++j)
            {
                biases[i][j, 0] = System.BitConverter.ToSingle(buffer, ptr);
                ptr += n_float;
            }
        }

        lastWeights = new float[inputCount];

        for (int i = 0; i < inputCount; i++)
        {
            lastWeights[i] = System.BitConverter.ToSingle(buffer, ptr);
            ptr += n_float;
        }

        lastBias = System.BitConverter.ToSingle(buffer, ptr);
    }
}
