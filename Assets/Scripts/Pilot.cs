using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : MonoBehaviour
{
    private Car car;
    protected float[] carInfo = new float[6];

    // Start is called before the first frame update
    void Start()
    {
        car = gameObject.GetComponent<Car>();

        for (int i = 0; i < carInfo.Length - 1; i++)
        {
            carInfo[i] = car.GetDistance(i);
        }
        carInfo[carInfo.Length - 1] = car.GetVelocity();

        Initialize();
    }

    protected virtual void Initialize()
    {

    }

    // Update is called once per frame  
    void Update()
    {
        for (int i = 0; i < carInfo.Length - 1; i++)
        {
            carInfo[i] = car.GetDistance(i) / 66.6f;
        }
        carInfo[carInfo.Length - 1] = car.GetVelocity() / 30;

        car.Steer(DetermineLeftRight());
        car.Accelerate(DetermineAccelerateBrake());
        //if (DetermineBrake())
        //{
        //    car.Brake();
        //}
    }

    protected virtual float DetermineLeftRight()
    {
        float left_right = Input.GetAxisRaw("Horizontal");

        return left_right;
    }

    protected virtual float DetermineAccelerateBrake()
    {
        float up_down = Input.GetAxisRaw("Vertical");

        return (up_down > 0) ? 1.0f : -1.0f;
    }

}
