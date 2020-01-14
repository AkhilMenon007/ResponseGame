﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<Ball>();
        if (ball != null) 
        {
            ball.Miss();
        }
    }
}
