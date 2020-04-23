using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPlanet : MonoBehaviour
{
    public float starMass = 1000f;

    void Start() {
        float initV = Mathf.Sqrt(starMass / Mathf.Max(transform.position.magnitude, 1f));
        Vector3 tangentialDirection = Vector3.Cross(transform.position, transform.up).normalized;
        GetComponent<Rigidbody>().velocity = initV * tangentialDirection;
    }

    void FixedUpdate()
    {
        float r = Mathf.Max(Vector3.Magnitude(transform.position), 1f);
        float totalForce = - (starMass) / (r*r);
        Vector3 force = (transform.position).normalized * totalForce;
        GetComponent<Rigidbody>().AddForce(force);
    }
    

}