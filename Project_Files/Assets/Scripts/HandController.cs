using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    public XRRayInteractor rayHandler;

    public GameObject currentRayOutObject;

    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //keep track of the object currently ray casted
        if (rayHandler.TryGetCurrent3DRaycastHit(out RaycastHit rayOut))
        {
            currentRayOutObject = rayOut.collider.gameObject;
        } else
        {
            currentRayOutObject = null;
        }
    }

    public void Shoot(InputAction.CallbackContext value)
    {
        //if the object in the ray cast is an enemy make it take damage
        if(currentRayOutObject != null && value.started)
        {
            if(currentRayOutObject.GetComponent<Enemy>())
            {
                currentRayOutObject.GetComponent<Enemy>().TakeDamage(damage);
            } else if (currentRayOutObject.tag == "Start")
            {
                FindAnyObjectByType<GameManager>().StartGame();
                Destroy(currentRayOutObject);
            }
        }
    }
}
