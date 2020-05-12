using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRandomizer : MonoBehaviour
{
    public GasRings rings;
    public float minWidth;
    public float maxWidth;
    public float minDistance;
    public float maxDistance;
    public float minHue;
    public float maxHue;
    public float minSaturation;
    public float maxSaturation;
    public float minValue;
    public float maxValue;

    private void Awake() {
        float width = Random.Range(minWidth, maxWidth);
        float distance = Random.Range(minDistance, maxDistance);

        rings.innerRadius = distance;
        rings.outerRadius = distance + width;

        rings.ringColor = UnityEngine.Random.ColorHSV(minHue, maxHue, minSaturation, maxSaturation, minValue, maxValue);
    }
}
