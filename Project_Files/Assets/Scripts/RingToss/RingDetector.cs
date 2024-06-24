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
    private XRGrabInteractable grabScript;
    public GameObject waitingPosition;
    [SerializeField] private Rigidbody ringRB;
    [SerializeField] private GameObject[] ringColliders;

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

        grabScript.enabled = false;
        ringRB.isKinematic = true;
        StopCoroutine(ThrownTimer());
        foreach (GameObject ring in ringColliders) { ring.SetActive(false); }
        rm.SpawnNextRing();
        this.enabled = false;
    }
    
    private IEnumerator ThrownTimer()
    {
        timerStarted = true;
        Debug.Log("ThrownTimer() Coroutine Started...");
        yield return new WaitForSecondsRealtime(1.5f); // Wait for 3 seconds.

        Debug.Log("Checking If Ring Is On The Catcher...");
        // If the ring is NOT caught on the catcher.
        if (onCatcher == false)
        {
            Debug.Log("Ring Is Not On The Catcher...");
            missedCatcher = true;
            MoveToWaitPosition(); // Move the ring back to the waiting position.
            wasThrown = false;
        }
        yield return new WaitForEndOfFrame();
        timerStarted = false;
        StopCoroutine(ThrownTimer());
    }

    /// <summary>
    /// Moves the ring to the the wait position.
    /// </summary>
    private void MoveToWaitPosition()
    {
        ringRB.isKinematic = true;
        gameObject.transform.parent.transform.position = waitingPosition.transform.position;
        gameObject.transform.parent.transform.localRotation = new Quaternion(90, 0, 0, 90);
        if (grabScript.enabled == false)
        {
            grabScript.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ringRB != null)
        {
            if (ringRB.isKinematic == false)
            {
                ringRB.isKinematic = true;
            }
        }
        foundCatcher = false;
        onCatcher = false;
        grabScript = gameObject.transform.parent.GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the ring is currently being held.
        if (inHand == true) 
        {
            // Resets the booleans.
            ringRB.isKinematic = false;
            StopCoroutine(ThrownTimer());
            wasThrown = false;
            foundCatcher = false;
            onCatcher = false;
            missedCatcher = false;
            timerStarted = false;
        }

        // If the ring has been thrown.
        if (wasThrown == true)
        {
            // If the reset timer hasnt already been activated.
            if (timerStarted == false)
            {
                grabScript.enabled = false;
                // Start the countdown timer.
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
            if (gameObject.transform.parent.position != waitingPosition.transform.localPosition || gameObject.transform.parent.position != waitingPosition.transform.position)
            {
                MoveToWaitPosition();
            }
            else
            {
                if (grabScript.enabled == false)
                {
                    grabScript.enabled = true;
                }
                missedCatcher = false;
            }
        }
    }
}
