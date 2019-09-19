using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Matrix
{
    private float[,] m;

    public int dimensionY
    {
        get
        {
            return m.GetLength(0);
        }
    }

    public int dimensionX
    {
        get
        {
            return m.GetLength(1);
        }
    }

    public float this[int y, int x]
    {
        get
        {
            if (y > m.GetLength(0) || x > m.GetLength(1))
            {
                throw new System.Exception();
            }
            else
            {
                try
                {
                    float test = m[y, x];
                }
                catch (System.Exception)
                {

                    throw;
                }
                return m[y, x];
            }
        }

        set
        {
            m[y,x] = value;
        }
    }

    public Matrix(int dimensionY, int dimensionX)
    {
        m = new float[dimensionY, dimensionX];
    }

    public static Matrix operator *(Matrix m1, Matrix m2)
    {
        if(m1.dimensionX != m2.dimensionY)
        {
            return null;
        }

        Matrix m = new Matrix(m1.dimensionY, m2.dimensionX);

        for (int i = 0; i < m.dimensionY; ++i)
        {
            for (int j = 0; j < m.dimensionX; j++)
            {
                float elementLocalSum = 0.0f;

                for (int k = 0; k < m1.dimensionX; ++k)
                {
                        elementLocalSum += m1[i,k] * m2[k,j];                 
                }

                m[i, j] = elementLocalSum;
            }
        }

        return m;
    }

    public static Matrix operator -(Matrix m1, Matrix m2)
    {
        if (m1.dimensionX != m2.dimensionX || m2.dimensionY != m2.dimensionY)
        {
            return null;
        }

        Matrix m = new Matrix(m1.dimensionY, m2.dimensionX);

        for (int i = 0; i < m.dimensionY; ++i)
        {
            for (int j = 0; j < m.dimensionX; j++)
            {
                m[i, j] = m1[i, j] - m2[i, j];
            }
        }

        return m;
    }
}
