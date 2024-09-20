using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindForce : MonoBehaviour
{
    public Cloth clothComponent;
    public Vector3 windDirection = new Vector3(1f, 0f, 0f);
    public float windForce = 5f;

    void Update()
    {
        // Apply wind force to the cloth
        clothComponent.externalAcceleration = windDirection * windForce;
    }
}
