using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleGraph : MonoBehaviour
{
    public RectTransform canvas;
    public Transform camera;

    private Graph angleGraph;

    // Start is called before the first frame update
    void Start()
    {
        angleGraph = new Graph(canvas, new Vector2(0, 0.5f), 0.5f, 1.0f);
        angleGraph.color = Color.cyan;
        angleGraph.AddYLabel("+90°", +1.0f);
        angleGraph.AddYLabel("-----", 0.0f);
        angleGraph.AddYLabel("-90°", -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float value = Vector3.SignedAngle(Quaternion.Euler(0, 0, 0)*Vector3.forward, camera.rotation*Vector3.forward,Vector3.up);
        angleGraph.AddValue(value / 90.0f);
        //angleGraph.AddValue((camera.rotation.eulerAngles.y % 360.0f - 180.0f) / 180.0f);
    }
}
