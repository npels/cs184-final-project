using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbiterVerlet : PlanetOrbiter
{
    Vector3 velocity;
    Vector3 acc; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (star == null) {
            return;
        }

        //verletUpdate(Time.deltaTime);
        huenUpdate(Time.deltaTime);
    }

    protected override void SetInitialOrbitingVelocity() {
        Vector3 starDir = this.transform.position - star.transform.position; 
        float r = starDir.magnitude;
        
        float v = Mathf.Sqrt(star.Mass / Mathf.Max(r, EPS));
        Vector3 tangentialDirection = Vector3.Cross(starDir, transform.up).normalized;
        
        this.velocity = (1.25f) * v * tangentialDirection;
    }

    void verletUpdate(float dt)
    {
        Vector3 newPos = this.transform.position + (this.velocity * dt) + (this.acc * dt * dt * 0.5f);
        Vector3 newAcc = applyGravity(); 
        Vector3 newVel = this.velocity + (this.acc + newAcc) * (dt * 0.5f);
        this.transform.position = newPos;
        this.velocity = newVel;
        this.acc = newAcc;
    }

    void huenUpdate(float dt) {
        Vector3 newAcc = applyGravity(); 
        Vector3 vel_E = velocity + (dt * 0.5f * (acc + newAcc));
        Vector3 newPos = this.transform.position + (dt * 0.5f * (velocity + vel_E));
        Vector3 newVel = velocity + (dt * 0.5f * (acc + newAcc));

        this.transform.position = newPos;
        velocity = newVel;
        acc = newAcc;
    }

    Vector3 applyGravity() {
        Vector3 gravityDir = this.transform.position - star.transform.position; 
        float r = gravityDir.magnitude;

        float totalForce = - star.Mass / (Mathf.Max(r*r, EPS));
        Vector3 gravityForce = (gravityDir).normalized * totalForce;
        return gravityForce;
    }


}
