using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField]
    int planetCount = 100;
    [SerializeField]
    Vector2 radiusRange = new Vector2(10, 200);
    [SerializeField]
    GameObject planetPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreatePlanets();
    }

    void CreatePlanets() {
        for (int i = 0; i < planetCount; i++) {
            GameObject planet = GameObject.Instantiate(planetPrefab);
            PlanetOrbiter planetOrbiter = planet.GetComponent<PlanetOrbiter>();
            planet.transform.position = GetRandomPlanetPosition();
            planet.transform.localScale *= Random.Range(0.5f, 0.8f);
            planetOrbiter.SetStar(this);
        }
    }

    Vector3 GetRandomPlanetPosition() {
        float minRadius = radiusRange[0];
        float maxRadius = radiusRange[1];

        //restrict to a box around this star
        float randX = Random.Range(-10, 10);
        float randY = Random.Range(-1, 1); 
        float randZ = Random.Range(-10, 10);
        Vector3 randomOffset = new Vector3(randX, randY, randZ);
        float randRadius = Random.Range(minRadius, maxRadius);

        return this.transform.position + randRadius * randomOffset.normalized;
    }

    public float Mass() {
        return 1000f; 
    }

}
