using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOLaser : MonoBehaviour
{
    [Tooltip("the line renderer for when the laser does damage")]
    public LineRenderer lethalLineRenderer;

    [Tooltip("the line renderer for when the laser doesnt do damage")]
    public LineRenderer dottedLineRenderer;

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

    [Tooltip("how long the laser will fire for")]
    public float shootingTime;

    bool killMode = false;

    int currentLaserPoint;

    Vector3 playerHeadPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootRoutine());
        dottedLineRenderer.SetPositions(new Vector3[] { new Vector3(0, -1000, 0), new Vector3(0, -1000, 0) });
        lethalLineRenderer.SetPositions(new Vector3[] { new Vector3(0, -1000, 0), new Vector3(0, -1000, 0) });
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
        Vector3 playerHeadPos = Camera.main.gameObject.transform.position;

        currentLaserPoint = Random.Range(0, laserPoints.Length);

        Vector3 endAimPos = new Vector3(playerHeadPos.x - (laserPoints[currentLaserPoint].position.x - playerHeadPos.x), playerHeadPos.y - (laserPoints[currentLaserPoint].position.y - playerHeadPos.y), playerHeadPos.z - (laserPoints[currentLaserPoint].position.z - playerHeadPos.z));

        //render the laser from the laser point to the player
        dottedLineRenderer.SetPositions(new Vector3[] { laserPoints[currentLaserPoint].position, endAimPos });

        //give the player time to dodge
        yield return new WaitForSeconds(timeBeforeShoot);

        killMode = true;

        dottedLineRenderer.SetPositions(new Vector3[] { new Vector3(0, -1000, 0), new Vector3(0, -1000, 0) });

        lethalLineRenderer.SetPositions(new Vector3[] { laserPoints[currentLaserPoint].position, endAimPos });

        yield return new WaitForSeconds(shootingTime);

        //move the line renderer away
        lethalLineRenderer.SetPositions(new Vector3[] { new Vector3(0, -1000, 0), new Vector3(0, -1000, 0) });

        killMode = false;

        //restart
        StartShootRoutine();
    }

    private void Update()
    {
        if(killMode)
        {
            //check if the player was hit
            RaycastHit hit;

            if (Physics.Raycast(laserPoints[currentLaserPoint].position, new Vector3(playerHeadPos.x - laserPoints[currentLaserPoint].position.x, playerHeadPos.y - laserPoints[currentLaserPoint].position.y, playerHeadPos.z - laserPoints[currentLaserPoint].position.z), out hit, Mathf.Infinity))
            {
                Debug.Log("HIT tag = " + hit.collider.tag);
                Debug.Log(hit.collider.gameObject);
                if (hit.collider.tag == "MainCamera")
                {
                    Debug.Log("HIT CAMERA");
                    //if the player was hit take damage
                    lethalLineRenderer.SetPositions(new Vector3[] { new Vector3(0, -1000, 0), new Vector3(0, -1000, 0) });
                    GameManager.singleton.TakeDamage(damage);
                    killMode = false;
                }
            }
        }
    }
}
