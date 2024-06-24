using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBasketball : MonoBehaviour
{

    public GameObject basketball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnBall()
    {
        basketball.transform.localPosition = Vector3.zero;
    }
}
