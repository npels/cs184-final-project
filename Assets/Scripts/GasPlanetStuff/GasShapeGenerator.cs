using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasShapeGenerator 
{
    GasShapeSettings settings;

    public GasShapeGenerator(GasShapeSettings settings)
    {
        this.settings = settings;
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        return pointOnUnitSphere * settings.planetRadius;
    }
}
