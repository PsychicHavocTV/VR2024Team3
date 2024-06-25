using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.singleton.rotator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
