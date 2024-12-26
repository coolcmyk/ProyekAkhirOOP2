using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float lifetime = 5f; // Default value for lifetime

    // Start is called before the first frame update
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
