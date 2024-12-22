using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private SpriteRenderer theSR;
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        theSR.flipX = true;
    }
    void Update()
    {
        transform.LookAt(PlayerController.instance.transform.position, -Vector3.forward);
    }
}
