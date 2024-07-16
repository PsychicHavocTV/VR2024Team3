using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBasketball : MonoBehaviour
{

    public GameObject basketball;
    private GameObject basketballReferance;
    public Transform ballSpawn;

    // Start is called before the first frame update
    void Start()
    {
        basketballReferance = Instantiate(basketball);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnBall()
    {
        Destroy(basketballReferance);
        basketballReferance = Instantiate(basketball, ballSpawn);
    }
}
