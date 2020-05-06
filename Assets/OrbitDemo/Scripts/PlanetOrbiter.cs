using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbiter : MonoBehaviour
{
    [SerializeField]
    Material[] planetMaterials;
    protected float EPS = 0.01f;

    protected Star star;

    void Awake() {
        SetRandomPlanetMaterial();
        SetRandomTrailColor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (star == null) {
            return; 
        }

        ApplyGravity();
    }

    public void SetStar(Star star) {
        this.star = star;
        this.transform.SetParent(star.transform);

        SetInitialOrbitingVelocity();
    }

    void ApplyGravity() {
        Vector3 gravityDir = this.transform.position - star.transform.position; 
        float r = gravityDir.magnitude;

        float totalForce = - star.Mass / (Mathf.Max(r*r, EPS));
        Vector3 gravityForce = (gravityDir).normalized * totalForce;
        
        GetComponent<Rigidbody>().AddForce(gravityForce);
    }

    protected virtual void SetInitialOrbitingVelocity() {
        Vector3 starDir = this.transform.position - star.transform.position; 
        float r = starDir.magnitude;
        
        float v = Mathf.Sqrt(star.Mass / Mathf.Max(r, EPS));
        Vector3 tangentialDirection = Vector3.Cross(starDir, transform.up).normalized;
        
        GetComponent<Rigidbody>().velocity = (1.25f) * v * tangentialDirection;
    }

    void SetRandomTrailColor() {
        Color randomColor = new Color(
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f)
        );
        GetComponent<TrailRenderer>().startColor = randomColor;
        GetComponent<TrailRenderer>().endColor = randomColor;
    }

    void SetRandomPlanetMaterial() {
        GetComponent<Renderer>().material = planetMaterials[Random.Range(0, planetMaterials.Length)];
    }

}
