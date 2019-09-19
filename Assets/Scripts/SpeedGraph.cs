using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGraph : MonoBehaviour
{
    public RectTransform canvas;
    public Car car;

    private Graph speedGraph;

    // Start is called before the first frame update
    void Start()
    {
        speedGraph = new Graph(canvas, new Vector2(0, 0), 1.0f, 1.0f);
        speedGraph.color = Color.yellow;
        speedGraph.AddYLabel("100", 1.0f);
        speedGraph.AddYLabel(" 50", 0.5f);
        speedGraph.AddYLabel("  0", 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        speedGraph.AddValue(car.GetVelocity() / 100);
    }
}
