using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlanetGenerator : MonoBehaviour
{
    public GasPlanet planet;
    public Material material;

    void Awake()
    {
        RegenerateShape();
        RegenerateColor();
        //planet.GeneratePlanet();
    }

    public void RegenerateShape()
    {
        GasShapeSettings settings = ScriptableObject.CreateInstance("GasShapeSettings") as GasShapeSettings;
        float radius = 10;

        settings.planetRadius = radius;
        planet.shapeSettings = settings;
    }
    
    public void RegenerateColor()
    {
        GasColorSettings settings = ScriptableObject.CreateInstance("GasColorSettings") as GasColorSettings;
        settings.planetMaterial = new Material(material);
        settings.planetMaterial.SetColor("_BaseColor", Color.blue);
        Color color1 = Color.blue;
        Color color2 = Color.blue;
        Gradient gradient = new Gradient();
        GradientColorKey[] gradientColorKeys = new GradientColorKey[2];
        gradientColorKeys[0] = new GradientColorKey(color1, 0f);
        gradientColorKeys[1] = new GradientColorKey(color2, 1f);
        settings.planetColor = gradient;
        planet.colorSettings = settings;
    }
}
