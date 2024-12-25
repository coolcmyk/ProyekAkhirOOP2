using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Transform doorModel;
    public GameObject colObject;
    public float openSpeed;
    private bool shouldOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // pintu bergerak z axis aja (buka keatas tutup ke bawah)
      if(shouldOpen && doorModel.position.z != 1f)
      {
        doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, doorModel.position.y, 1f), openSpeed * Time.deltaTime);
        if(doorModel.position.z == 1f)
        {
            colObject.SetActive(false);
        }
      }else if(!shouldOpen && doorModel.position.z != 0f)
      {
        doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, doorModel.position.y, 0f), openSpeed * Time.deltaTime);
        if(doorModel.position.z == 0f)
        {
            colObject.SetActive(true);
        }
      }

    }
    // kebuka kalau ada player
    private void onTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            shouldOpen = true;
        }
    }
    // tutup kalau gaada player

    private void onTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            shouldOpen = false;
        }
    }
}
