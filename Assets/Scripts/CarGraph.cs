using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGraph : MonoBehaviour
{
    public RectTransform canvas;
    public Car car;

    private Graph graphSteering, graphAcceleration;


    //private int xPosition;

    //private List<GameObject> pixels = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        graphSteering = new Graph(canvas, new Vector2(0, 0.75f), 0.25f, 1.0f);
        graphSteering.color = Color.green;
        graphSteering.AddYLabel(" Left", +1.0f);
        graphSteering.AddYLabel("-----", +0.0f);
        graphSteering.AddYLabel("Right", -1.0f);
        graphAcceleration = new Graph(canvas, new Vector2(0, 0.25f), 0.25f, 1.0f);
        graphAcceleration.color = Color.magenta;
        graphAcceleration.AddYLabel("  Gas", +1.0f);
        graphAcceleration.AddYLabel("-----", +0.0f);
        graphAcceleration.AddYLabel("Break", -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        graphSteering.AddValue(-car.steering);
        graphAcceleration.AddValue(car.acceleration);
    }

    //private void AddValue(float yValue)
    //{
    //    int drawPosition = xPosition;
    //    xPosition++;
    //    if(Mathf.Abs(xPosition) >= canvas.sizeDelta.x)
    //    {
    //        Destroy(pixels[0]);
    //        pixels.RemoveAt(0);

    //        foreach (GameObject p in pixels)
    //        {
    //            p.GetComponent<RectTransform>().anchoredPosition -= Vector2.right;
    //        }

    //        drawPosition = (int) canvas.sizeDelta.x;
    //    }

    //    GameObject pixel = new GameObject("Pixel");

    //    RectTransform rectTransform = pixel.AddComponent<RectTransform>();
    //    rectTransform.transform.SetParent(canvas); 
    //    rectTransform.localScale = Vector3.one;
    //    rectTransform.anchorMin = new Vector2(0, 0.5f);
    //    rectTransform.anchorMax = new Vector2(0, 0.5f);
    //    rectTransform.pivot = new Vector2(0, 0.5f);
    //    rectTransform.anchoredPosition = new Vector2(drawPosition, 0.8f * yValue * canvas.sizeDelta.y / 2 );
    //    rectTransform.sizeDelta = new Vector2(2, 2); 

    //    Image image = pixel.AddComponent<Image>();
    //    image.color = Color.black;

    //    pixels.Add(pixel);

    //}
}
