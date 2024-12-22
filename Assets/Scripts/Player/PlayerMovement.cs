// using UnityEngine;

// public class PlayerMovement : MonoBehaviour  // Fixed typo in class name
// {
//     public Rigidbody2D rb;
//     public float moveSpeed = 5f;
//     public float mouseSensitivity = 1f;
//     public Camera viewCam;
//     // public Camera playerCamera;  
    
//     private Vector2 moveInput;
//     private Vector3 mouseInput; 

//     void Start()
//     {
//         // If playerCamera isn't assigned in inspector, get it from viewCam
//         // if (playerCamera == null)
//         //     playerCamera = viewCam.GetComponent<Camera>();
//     }

//     void Update()
//     {
//         // Movement
//         moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//         Vector3 moveHorizontal = transform.up * -moveInput.x;
//         Vector3 moveVertical = transform.right * moveInput.y;
//         rb.velocity = (moveHorizontal + moveVertical) * moveSpeed;

//         // Camera rotation
//         mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        
//         // Update player rotation (around Z axis for 2D)
//         transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);

//         // Update camera rotation (around Y axis)
//         viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));

//         // Shooting
//         if(Input.GetMouseButton(0))
//         {
//             Ray ray = viewCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
//             RaycastHit hit;
//             if(Physics.Raycast(ray, out hit))
//             {
//                 Debug.Log(hit.transform.name);
//             }
//             else
//             {
//                 Debug.Log("Missed");
//             }
//         }
//     }
// }



using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;
    public Camera viewCam;
    public float drag = 5f; // Added drag value
    public float angularDrag = 10f; // Added angular drag value
    public GameObject bulletImpact; // Declare bullet impact object
    
    private Vector2 moveInput;
    private Vector2 mouseInput;
    private int currentHealth;
    public int maxHealth = 100;
    public GameObject deathScreen;
    private bool hasDied;
    void Awake() // Fix method name to use proper capitalization
    {
        instance = this;
    }

    void Start()
    {
        // Set up physics properties to prevent unwanted spinning
        rb.freezeRotation = true; // Prevents physics-based rotation
        rb.drag = drag; // Adds linear drag (friction)
        rb.angularDrag = angularDrag; // Adds rotational drag
        rb.gravityScale = 0f; // Ensures gravity doesn't affect movement
    }

    void Update()
    {
        if (!hasDied) // kalau gak mati dia masi bisa ngelakuin yg didalam if, kalau mati player gak bisa ngelakuin apa2
        {
            HandleInput();
            HandleShooting();
            HandleFootstep();
        }
    }

    void HandleInput()
    {
        // Movement
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveHorizontal = transform.up * -moveInput.x;
        Vector3 moveVertical = transform.right * moveInput.y;

        // Apply movement as force instead of direct velocity
        rb.velocity = Vector2.Lerp(rb.velocity, (moveHorizontal + moveVertical) * moveSpeed, Time.deltaTime * 10f);

        // Camera rotation
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        // Smooth rotation
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(
            currentRotation.x,
            currentRotation.y,
            currentRotation.z - mouseInput.x
        );

        // Camera rotation
        Vector3 currentCamRotation = viewCam.transform.localRotation.eulerAngles;
        viewCam.transform.localRotation = Quaternion.Euler(
            currentCamRotation.x,
            currentCamRotation.y + mouseInput.y,
            currentCamRotation.z
        );
    }

    void HandleShooting()
    {
        if (Input.GetMouseButton(0))
        {
            AudioController.instance.PlayGunShot();
            Ray ray = viewCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(bulletImpact, hit.point, Quaternion.identity);

                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
                    Debug.Log(hit.transform.name);
                }
                else
                {
                    Debug.Log("Missed");
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reduce velocity on collision to prevent bouncing
        rb.velocity *= 0.5f;
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            deathScreen.SetActive(true);
            hasDied = true;
        }
    }
    public void AddHealth(int healthAmount)
    {
       currentHealth += healthAmount;
       if(currentHealth > maxHealth)
       {
           currentHealth = maxHealth;
       }
    }
    public void HandleFootstep()
    {
        bool isMoving = rb.velocity.sqrMagnitude > 0.01f;
        if (isMoving){
            AudioController.instance.PlayFootStep();
        }
        else{
            AudioController.instance.StopFootStep();
        }
    }
}


