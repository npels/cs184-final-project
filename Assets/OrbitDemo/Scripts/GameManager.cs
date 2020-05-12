using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    #region Singleton
    public static GameManager Instance;
    void Awake() {
        Instance = this;
    }
    #endregion 

    [SerializeField]
    int planetCount = 100;
    [SerializeField]
    Vector2 radiusRange = new Vector2(10, 200);
    [SerializeField]
    Vector2 planetSizeRange = new Vector2(0.5f, 1f);
    [SerializeField]
    GameObject planetPrefab;
    [SerializeField]
    GameObject starPrefab;
    [SerializeField]
    GameObject gasPlanetPrefab;
    
    Star star;
    public Dictionary<string, GameObject> nameToSolarSystemObject; //name to solar system gameobject

    // Start is called before the first frame update
    void Start()
    {
        nameToSolarSystemObject = new Dictionary<string, GameObject>();

        star = Instantiate(starPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Star>();
        nameToSolarSystemObject["Star"] = star.gameObject;

        CreatePlanetsAroundStar(star);
        CreateGasPlanetAroundStar(star);

        UIManager.Instance.PopulateDropdown(nameToSolarSystemObject);
    }

    void CreatePlanetsAroundStar(Star star) {
        for (int i = 0; i < planetCount; i++) {
            GameObject planet = GameObject.Instantiate(planetPrefab);
            PlanetOrbiter planetOrbiter = planet.GetComponent<PlanetOrbiter>();
            planet.transform.position = GetRandomPositionAroundStar(star);
            // planet.transform.localScale *= Random.Range(planetSizeRange.x, planetSizeRange.y);
            planetOrbiter.SetStar(star);

            nameToSolarSystemObject["Planet " + (i+1)] = planet;
        }
    }

    void CreateGasPlanetAroundStar(Star star)
    {
        int gasNum = Random.Range(1, 5);
        for (int i = 0; i < gasNum; i++)
        {
            GameObject planet = GameObject.Instantiate(gasPlanetPrefab);
            PlanetOrbiter planetOrbiter = planet.GetComponent<PlanetOrbiter>();
            planet.transform.position = GetRandomPositionAroundStar(star);
            planet.transform.localScale *= Random.Range(0.04f, 0.08f);
            planetOrbiter.SetStar(star);

            nameToSolarSystemObject["Gas Planet " + (i + 1)] = planet;
        }
    }

    Vector3 GetRandomPositionAroundStar(Star star) {
        float minRadius = radiusRange[0];
        float maxRadius = radiusRange[1];

        //restrict to a box around this star
        float randX = Random.Range(-10, 10);
        float randY = Random.Range(-1, 1); 
        float randZ = Random.Range(-10, 10);
        Vector3 randomOffset = new Vector3(randX, randY, randZ);
        float randRadius = Random.Range(minRadius, maxRadius);

        return star.transform.position + (randRadius * randomOffset.normalized);
    }

}
