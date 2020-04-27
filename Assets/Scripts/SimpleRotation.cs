using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour {

    public float degreesPerSecond;

    void FixedUpdate()
    {
        if (degreesPerSecond != 0)
            transform.Rotate(new Vector3(0, (float)(degreesPerSecond / 50.0), 0));
    }
}
