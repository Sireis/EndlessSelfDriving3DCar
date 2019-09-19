using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    List<Barrier> barriers = new List<Barrier>();

    public Material material;

    [Range(0.01f,10)]
    public float scale;
    [Range(0,100)]
    public float amplitude;

    public int seed;

    private float distance = 8.0f, width = 20.0f;
    private Vector3 point, lastPoint, veryLastPoint;
    private double timeTracker = 0.0;
    private double tracker = 0.0;
    private float xOffset, yOffset;

    
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(seed);
        xOffset = Random.Range(-10000, 10000);
        yOffset = Random.Range(-10000, 10000);

        veryLastPoint = new Vector3();
        lastPoint = veryLastPoint + distance * Vector3.forward;
        point = lastPoint + distance * Vector3.forward;
        Vector3 temp = point;
        AddBarrier(temp + distance * 1 * Vector3.forward);
        AddBarrier(temp + distance * 2 * Vector3.forward);
        AddBarrier(temp + distance * 3 * Vector3.forward);
        AddBarrier(temp + distance * 4 * Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        timeTracker += Time.deltaTime;
        tracker += 0.016;
        if (Time.deltaTime > 20)
        {
            print("Time is odd: " + Time.deltaTime);
        }

        //if (timeTracker % 0.25f <= 1.1 * Time.smoothDeltaTime)
        //{
        float offset = NoiseFunction((float)tracker);
        Vector3 newPoint = point + new Vector3(offset, 0, distance);
        //Vector3 newPoint = new Vector3(offset, 0, point.z + distance);
        AddBarrier(newPoint);
        //}
    }

    private void AddBarrier(Vector3 newPoint)
    {
        Vector3 leftStart = lastPoint + Quaternion.Euler(0, -90, 0) * (width * (point - veryLastPoint).normalized);
        Vector3 rightStart = lastPoint + Quaternion.Euler(0, +90, 0) * (width * (point - veryLastPoint).normalized);
        Vector3 leftEnd = point + Quaternion.Euler(0, -90, 0) * (width * (newPoint - lastPoint).normalized);
        Vector3 rightEnd = point + Quaternion.Euler(0, +90, 0) * (width * (newPoint - lastPoint).normalized);

        float angleStart = Vector3.SignedAngle(lastPoint - point, veryLastPoint - point, Vector3.up);
        float angleEnd = -Vector3.SignedAngle(point - lastPoint, newPoint - lastPoint, Vector3.up);

        Barrier barrier;
        barrier = gameObject.AddComponent<Barrier>();
        barrier.Initialize(leftStart, leftEnd, angleStart, angleEnd, material);
        barriers.Add(barrier);
        barrier = gameObject.AddComponent<Barrier>();
        barrier.Initialize(rightStart, rightEnd, angleStart, angleEnd, material);
        barriers.Add(barrier);

        veryLastPoint = lastPoint;
        lastPoint = point;
        point = newPoint;

        if(barriers.Count > 10000)
        {
            foreach (Transform item in barriers[0].transform)
            {
                Object.Destroy(item.gameObject);
            }
            Object.Destroy(barriers[0]);
            
            foreach (Transform item in barriers[1].transform)
            {
                Object.Destroy(item.gameObject);
            }
            Object.Destroy(barriers[1]);

            barriers.RemoveAt(0);
            barriers.RemoveAt(0);
        }
    }

    private float NoiseFunction(float x)
    {
        return amplitude * Mathf.PerlinNoise(yOffset, (x + xOffset) / scale) - amplitude / 2;
    }

    private void OnDrawGizmos()
    {
    }
}

