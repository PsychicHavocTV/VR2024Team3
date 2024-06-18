using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterBehaviour : MonoBehaviour
{
    private Vector3 startPos;

    private float assaultTimer;
    private LineRenderer lineRenderer;

    [Tooltip("time taken to complete the assualt")]
    public float assaultTime;

    [Tooltip("how much time to take off the timer when the player is hit")]
    public float damage;

    [Tooltip("how far forwards the enemy will move forwards before shooting")]
    public float assaultDistance;

    private Vector3 assaultGoal;

    [Min(1)]
    public float laserDistanceMultiplyer;

    private bool startedShooting = false;

    public Transform laserPoint;

    public float timeBeforeAim;

    public float timeBeforeShoot;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        assaultGoal = transform.position + (transform.forward * assaultDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (assaultTimer / assaultTime < 1)
        {
            transform.position = Vector3.Lerp(startPos, assaultGoal, assaultTimer / assaultTime);
            assaultTimer += Time.deltaTime;
        } else if(!startedShooting)
        {
            StartShootRoutine();
            startedShooting = true;
        }
    }

    public void StartShootRoutine()
    {
        StartCoroutine(ShootRoutine());
    }

    public IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(timeBeforeAim);

        Vector3 playerHeadPos = Camera.main.transform.position;

        lineRenderer.SetPositions(new Vector3[] { laserPoint.position, (playerHeadPos - laserPoint.position).normalized * laserDistanceMultiplyer});

        yield return new WaitForSeconds(timeBeforeShoot);

        RaycastHit hit;

        if(Physics.Raycast(laserPoint.position, playerHeadPos - laserPoint.position, out hit, Mathf.Infinity))
        {
            if(hit.collider.tag == "MainCamera")
            {
                //FindFirstObjectByType<GameManager>().TakeDamage(damage);
            }
        }

        lineRenderer.SetPositions(new Vector3[] { new Vector3(0, -1000, 0), new Vector3(0, -1000, 0) });

        StartShootRoutine();
    }
}
