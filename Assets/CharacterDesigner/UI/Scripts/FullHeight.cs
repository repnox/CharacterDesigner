using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHeight : MonoBehaviour
{
    public RectTransform rectTransform;

    void Update()
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, Screen.height);
    }
}
