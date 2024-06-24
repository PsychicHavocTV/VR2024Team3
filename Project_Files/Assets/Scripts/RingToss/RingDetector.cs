using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RingDetector : MonoBehaviour
{
    private RingManager rm;
    private bool missedCatcher = false;
    private bool foundCatcher = false;
    private bool timerStarted = false;
    private bool onCatcher = false;
    private bool wasThrown = false;
    private bool inHand = false;
    [SerializeField] private GameObject waitingPosition;
    [SerializeField] private Rigidbody ringRB;

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the ring is in the players hand.
        if (other.gameObject.tag.ToLower() == "handattach")
        {
            if (inHand == false) { inHand = true; }
        }

        // Checks if the ring has hit the catcher.
        if(other.gameObject.tag.ToLower() == "catcher")
        {
            if (foundCatcher == false)
            {
                foundCatcher = true;
            }
            Debug.Log("Found Catcher..");
        }

        // Checks if the ring has been caught on the catcher.
        if (other.gameObject.tag.ToLower() == "catcherbase")
        {
            rm = other.GetComponent<RingManager>();
            if (onCatcher == false)
            {
                onCatcher = true;
            }
            Debug.Log("Ring is on the pole.");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        // Checks if the player has dropped/thrown the ring.
        if (other.gameObject.tag.ToLower() == "handattach")
        {
            if (inHand == true) 
            { 
                inHand = false;
                if (wasThrown == false) { wasThrown = true; }
                if (ringRB.isKinematic == true) { ringRB.isKinematic = false; }
            }
        }

        // If the player has bounced off of the catcher.
        if (other.gameObject.tag.ToLower() == "catcher")
        {
            if (foundCatcher == true)
            {
                foundCatcher = false;
            }
        }
    }

    private void RingCaught()
    {
        XRGrabInteractable grabScript = gameObject.transform.parent.GetComponent<XRGrabInteractable>();

        grabScript.enabled = false;
        rm.SpawnNextRing();
    }
    
    private IEnumerator ThrownTimer()
    {
        Debug.Log("ThrownTimer() Coroutine Started...");
        yield return new WaitForSecondsRealtime(3.0f); // Wait for 3 seconds.

        Debug.Log("Checking If Ring Is On The Catcher...");
        // If the ring is NOT caught on the catcher.
        if (onCatcher == false)
        {
            Debug.Log("Ring Is Not On The Catcher...");
            missedCatcher = true;
            MoveToWaitPosition(); // Move the ring back to the waiting position.
            wasThrown = false;
        }
        timerStarted = false;
        yield return new WaitForEndOfFrame();
        StopCoroutine(ThrownTimer());
    }

    /// <summary>
    /// Moves the ring to the the wait position.
    /// </summary>
    private void MoveToWaitPosition()
    {
        ringRB.isKinematic = true;
        gameObject.transform.parent.transform.position = waitingPosition.transform.localPosition;
        gameObject.transform.parent.transform.localRotation = new Quaternion(90, 0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        foundCatcher = false;
        onCatcher = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If the ring is currently being held.
        if (inHand == true) 
        {
            // Resets the booleans.
            ringRB.isKinematic = false;
            wasThrown = false;
            foundCatcher = false;
            onCatcher = false;
            missedCatcher = false;
            if (timerStarted == true)
            {
                StopCoroutine(ThrownTimer());
            }
            timerStarted = false;
        }

        // If the ring has been thrown.
        if (wasThrown == true)
        {
            // If the reset timer hasnt already been activated.
            if (timerStarted == false)
            {
                // Start the countdown timer.
                timerStarted = true;
                StartCoroutine(ThrownTimer());
            }
        }

        // If the ring has been caught on the catcher.
        if (onCatcher == true)
        {
            RingCaught();
        }

        // If the ring has missed the catcher, and isnt already at the waiting position.
        if (missedCatcher == true)
        {
            if (gameObject.transform.parent.position != waitingPosition.transform.localPosition)
            {
                MoveToWaitPosition();
            }
            else
            {
                missedCatcher = false;
            }
        }
    }
}
