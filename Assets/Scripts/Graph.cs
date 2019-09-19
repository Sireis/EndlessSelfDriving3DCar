using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph
{
    private RectTransform canvas;
    private Vector2 relativeOrigin;
    private float relativeHeight, relativeLength;

    private int xPosition, xOffset;
    private List<GameObject> pixels = new List<GameObject>();

    public Graph(RectTransform canvas, Vector2 relativeOrigin, float relativeHeight, float relativeLength)
    {
        this.canvas = canvas;
        this.relativeOrigin = relativeOrigin;
        this.relativeHeight = relativeHeight;
        this.relativeLength = relativeLength;
        this.color = Color.black;
    }

    public void AddValue(float yValue)
    {
        int drawPosition = (int)(xPosition * relativeLength) + xOffset;
        xPosition++;

        if (Mathf.Abs(drawPosition) >= (canvas.sizeDelta.x*relativeLength) - 10)
        {
            Object.Destroy(pixels[0]);
            pixels.RemoveAt(0);

            foreach (GameObject p in pixels)
            {
                p.GetComponent<RectTransform>().anchoredPosition -= Vector2.right;
            }

            drawPosition = (int)(canvas.sizeDelta.x * relativeLength) - 10;
        }

        GameObject pixel = new GameObject("Pixel");

        RectTransform rectTransform = pixel.AddComponent<RectTransform>();
        rectTransform.transform.SetParent(canvas);
        rectTransform.localScale = Vector3.one;
        rectTransform.anchorMin = relativeOrigin;
        rectTransform.anchorMax = relativeOrigin;
        rectTransform.pivot = relativeOrigin;
        rectTransform.anchoredPosition = new Vector2(drawPosition, 0.8f * yValue * canvas.sizeDelta.y * relativeHeight);
        rectTransform.sizeDelta = new Vector2(2, 2);

        Image image = pixel.AddComponent<Image>();
        image.color = this.color;

        pixels.Add(pixel);
    }

    public void AddYLabel(string description, float yValue)
    {
        GameObject label = new GameObject("Label");

        RectTransform rectTransform = label.AddComponent<RectTransform>();
        rectTransform.transform.SetParent(canvas);
        rectTransform.localScale = Vector3.one;
        rectTransform.anchorMin = relativeOrigin;
        rectTransform.anchorMax = relativeOrigin;
        rectTransform.pivot = relativeOrigin;
        rectTransform.anchoredPosition = new Vector2(7, 0.8f * yValue * canvas.sizeDelta.y * relativeHeight);
        rectTransform.sizeDelta = new Vector2(100, 11);

        Text text = label.AddComponent<Text>();
        text.text = description;
        text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        text.fontSize = 10;

        //xOffset = (int) text.flexibleWidth;
        xOffset = 40;
    }

    public Color color
    {
        get;
        set;
    }
}
