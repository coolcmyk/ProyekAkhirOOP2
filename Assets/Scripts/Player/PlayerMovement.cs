using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemetn : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    
    private Vector2 moveInput;
    private Vector3 mouseInput;
    public float mouseSensitivity = 1f;

    public Transform viewCam;
    void Start()
    {
        
    }
    void Update()
    {
        mouseInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveHorizontal = transform.up * -mouseInput.x;
        Vector3 moveVertical = transform.right * mouseInput.y;
        rb.velocity = (moveHorizontal + moveVertical) * moveSpeed;


        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);

        viewCam.localRotation = Quaternion.Euler(viewCam.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));
    }
}
