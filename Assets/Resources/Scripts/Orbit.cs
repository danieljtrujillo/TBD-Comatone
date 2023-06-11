using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public GameObject target; // the GameObject to orbit
    public float speed = 20.0f; // the speed of the orbit

    private void Update()
    {
        if (target != null)
        {
            // Rotate around the target
            transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}
