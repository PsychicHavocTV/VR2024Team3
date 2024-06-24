using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOLaser : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [Tooltip("how much time to take off the timer when the player is hit")]
    public float damage;

    [Min(1)]
    public float laserDistanceMultiplyer;

    public Transform[] laserPoints;

    public float timeBeforeAim;

    public float timeBeforeShoot;

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
        yield return new WaitForSeconds(timeBeforeAim);

        Vector3 playerHeadPos = Camera.main.transform.position;

        int currntLaserPoint = Random.Range(0, laserPoints.Length);

        lineRenderer.SetPositions(new Vector3[] { laserPoints[currntLaserPoint].position, (playerHeadPos - laserPoints[currntLaserPoint].position).normalized * laserDistanceMultiplyer });

        yield return new WaitForSeconds(timeBeforeShoot);

        RaycastHit hit;

        if (Physics.Raycast(laserPoints[currntLaserPoint].position, playerHeadPos - laserPoints[currntLaserPoint].position, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "MainCamera")
            {
                GameManager.singleton.TakeDamage(damage);
            }
        }

        lineRenderer.SetPositions(new Vector3[] { new Vector3(0, -1000, 0), new Vector3(0, -1000, 0) });

        StartShootRoutine();
    }
}
