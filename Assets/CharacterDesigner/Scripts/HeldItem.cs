using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : MonoBehaviour
{

    public Transform mountPoint;

    private void LateUpdate()
    {
        if (mountPoint)
        {
            transform.position = mountPoint.position;
            transform.rotation = mountPoint.rotation;
        }
    }
}
