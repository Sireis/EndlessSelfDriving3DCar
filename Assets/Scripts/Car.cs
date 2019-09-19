using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Flags]
    private enum Flags
    {
        Accelerate = 1,
        Brake = 2,
        Left = 4,
        Right = 8,
        Stop = 16
    }

    private float AccelerateRatio = 0.0f;
    private float SteerRatio = 0.0f;

    private int Flag = 0;

    private Quaternion initialRotation;

    private float pos_a = 80.0f, neg_a = 800.0f;
    private float v = 0.0f, v_max = 500.0f;
    private float p = 0.0f, dp = 130.0f;

    private Vector3 velocityState = new Vector3();

    private Vector3[] sensorDirections = new Vector3[]
    {
        Vector3.left,
        new Vector3(-1,0,1),
        Vector3.forward,
        new Vector3(1,0,1),
        Vector3.right
    };
    
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = 0;

        if ((Flag & (int) Flags.Stop) != (int) Flags.Stop)
        {
            if (AccelerateRatio > 0.0f)
            {
                v += (float)(Time.deltaTime * pos_a * AccelerateRatio);
                v = (v > v_max) ? v_max : v;
            }
            else
            {
                v += (float)(Time.deltaTime * neg_a * AccelerateRatio);
                v = (v < 0) ? 0.0f : v;
            }

            acceleration = AccelerateRatio;

            if(v != 0.0f)
            {
                p -= Time.deltaTime * dp * SteerRatio;
            }

            steering = SteerRatio;
            
            velocityState = Quaternion.Euler(0, p, 0) * (v * Vector3.forward);
            transform.position += Time.deltaTime * velocityState;
            transform.rotation = Quaternion.Euler(0, p, 0);
            Flag = 0;
        }
    }

    public void Steer(float ratio)
    {
        if(ratio > 1.0f)
        {
            SteerRatio = 1.0f;
        }
        else if(ratio < -1.0f)
        {
            SteerRatio = -1.0f;
        }
        else
        {
            SteerRatio = ratio;
        }
    }

    public void Accelerate(float ratio)
    {
        if (ratio > 1.0f)
        {
            AccelerateRatio = 1.0f;
        }
        else if (ratio < -1.0f)
        {
            AccelerateRatio = -1.0f;
        }
        else
        {
            AccelerateRatio = ratio;
        }
    }

    public float GetVelocity()
    {
        return v;
    }

    public float GetDistance(int sensor)
    {
        RaycastHit raycastHit;
        Ray ray = new Ray(transform.position, initialRotation * transform.rotation * sensorDirections[sensor]);

        if(Physics.Raycast(ray, out raycastHit, 300.0f))
        {
            return raycastHit.distance;
        }
        else
        {
            return float.MaxValue;
        }
    }

    public float acceleration
    {
        get;
        private set;
    }

    public float steering
    {
        get;
        private set;
    }
}