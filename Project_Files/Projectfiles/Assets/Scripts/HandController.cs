using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    public XRRayInteractor rayHandler;

    public GameObject currentRayOutObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if(currentRayOutObject != null && value.started)
        {
            currentRayOutObject.transform.localScale *= 10;
        }
    }
}
