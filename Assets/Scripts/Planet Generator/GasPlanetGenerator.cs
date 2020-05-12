using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlanetGenerator : MonoBehaviour
{
    public GasPlanet planet;
    public GasRings rings;
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
        Color colorBase = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Color ringColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        settings.planetMaterial.SetColor("_BaseColor", colorBase);
        rings.UpdateRingColor(ringColor);
        //rings.ringMat.SetColor("_BaseColor", ringColor);
        Color color1 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Color color2 = color1;
        Gradient gradient = new Gradient();
        GradientColorKey[] gradientColorKeys = new GradientColorKey[2];
        gradientColorKeys[0] = new GradientColorKey(color1, 0f);
        gradientColorKeys[1] = new GradientColorKey(color2, 1f);
        settings.planetColor = gradient;
        planet.colorSettings = settings;
    }
}
