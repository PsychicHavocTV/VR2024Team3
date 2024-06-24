using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    public XRRayInteractor rayHandler;

    public GameObject currentRayOutObject;

    public UnityEvent onShoot;

    public float damage;

    [Tooltip("time it takes for the gun to be able to shoot again")]
    public float shootTime;

    private bool canShoot = true;
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

        if (canShoot)
        {
            onShoot.Invoke();
            if (currentRayOutObject != null && value.started)
            {
                if (currentRayOutObject.tag == "Enemy")
                {
                    currentRayOutObject.GetComponent<Enemy>().TakeDamage(damage);
                }
                else if (currentRayOutObject.tag == "Start")
                {
                    FindAnyObjectByType<GameManager>().StartGame();
                    Destroy(currentRayOutObject);
                }
            }
            canShoot = false;

            StartCoroutine(StartShoot());
        }
    }

    public IEnumerator StartShoot()
    {
        yield return new WaitForSeconds(shootTime);
        canShoot = true;
    }
}
