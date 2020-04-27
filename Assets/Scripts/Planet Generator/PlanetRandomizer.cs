using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRandomizer : MonoBehaviour {
    public List<ShapeSettings> shapes;
    public List<ColorSettings> colors;
    public Planet planet;

    void Awake() {
        planet.shapeSettings = shapes[Random.Range(0, shapes.Count)];
        planet.colorSettings = colors[Random.Range(0, colors.Count)];

        planet.changeSeed(Random.Range(int.MinValue / 10, int.MaxValue / 10));

        // planet.GeneratePlanet();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
