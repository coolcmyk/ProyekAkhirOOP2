using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private spriteRenderer theSR;
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        theSR.flipx = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerMovement.instance.transform.position, -Vector3.forward);
    }
}
