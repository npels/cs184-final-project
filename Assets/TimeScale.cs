﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    public float timeScale = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
        // Time.fixedDeltaTime = timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
