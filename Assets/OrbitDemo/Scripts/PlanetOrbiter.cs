using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbiter : MonoBehaviour
{
    [SerializeField]
    Material[] planetMaterials;
    [SerializeField]
    Material trailMaterial;

    Star star;

    void Awake() {
        SetRandomPlanetMaterial();
        SetTrailRenderer();
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
        Vector3 distanceVector = this.transform.position - star.transform.position; 
        float r = distanceVector.magnitude;

        float totalForce = - (star.Mass() * this.Mass()) / (Mathf.Max(0.5f, r*r));
        Vector3 gravityForce = (distanceVector).normalized * totalForce;
        
        GetComponent<Rigidbody>().AddForce(gravityForce);
    }

    void SetInitialOrbitingVelocity() {
        Vector3 distanceVector = this.transform.position - star.transform.position; 
        float r = distanceVector.magnitude;
        
        float v = Mathf.Sqrt(star.Mass() / Mathf.Max(r, 1f));
        Vector3 tangentialDirection = Vector3.Cross(distanceVector, transform.up).normalized;
        
        GetComponent<Rigidbody>().velocity = v * tangentialDirection;
    }

    void SetTrailRenderer() {
        TrailRenderer tr = GetComponent<TrailRenderer>();
        tr.time = 1.0f; //extend this to get longer trails
        tr.startWidth = 0.1f; //width of trail 
        tr.endWidth = 0;
        tr.startColor = new Color(1, 1, 0, 0.1f); //yellow hue
        tr.endColor = new Color(0, 0, 0, 0); //fade out to nothing
    }

    void SetRandomPlanetMaterial() {
        GetComponent<Renderer>().material = planetMaterials[Random.Range(0, planetMaterials.Length)];
    }

    public float Mass() {
        return 1f;
    }

}
