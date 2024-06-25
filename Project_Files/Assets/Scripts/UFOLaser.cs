using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOLaser : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [Tooltip("how much time to take off the timer when the player is hit")]
    public float damage;

    [Min(1)]
    [Tooltip("how far the line renderer will render past the player and the distace of the raycast")]
    public float laserDistanceMultiplyer;

    [Tooltip("places where the laser can spawn from")]
    public Transform[] laserPoints;

    [Tooltip("time before the laser will start rendering")]
    public float timeBeforeAim;

    [Tooltip("how much time will pass after rendering the laser before it fires")]
    public float timeBeforeShoot;

    public float briggaUppies;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(ShootRoutine());
    }

    public void StartShootRoutine()
    {
        StartCoroutine(ShootRoutine());
    }

    public IEnumerator ShootRoutine()
    {
        //wait before aiming
        yield return new WaitForSeconds(timeBeforeAim);

        //find the players current position and find a laser position
        Vector3 playerHeadPos = Camera.main.transform.position;

        playerHeadPos.y += briggaUppies;

        int currntLaserPoint = Random.Range(0, laserPoints.Length);

        //render the laser from the laser point to the player
        lineRenderer.SetPositions(new Vector3[] { laserPoints[currntLaserPoint].position, (playerHeadPos - laserPoints[currntLaserPoint].position).normalized * laserDistanceMultiplyer });

        //give the player time to dodge
        yield return new WaitForSeconds(timeBeforeShoot);

        //check if the player was hit
        RaycastHit hit;

        if (Physics.Raycast(laserPoints[currntLaserPoint].position, playerHeadPos - laserPoints[currntLaserPoint].position, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "MainCamera")
            {
                //if the player was hit take damage
                GameManager.singleton.TakeDamage(damage);
            }
        }

        //move the line renderer away
        lineRenderer.SetPositions(new Vector3[] { new Vector3(0, -1000, 0), new Vector3(0, -1000, 0) });

        //restart
        StartShootRoutine();
    }
}
