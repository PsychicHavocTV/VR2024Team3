using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTarget : MonoBehaviour
{
    public float delayMin, delayMax;
    float realDelay;

    public GameObject popper;

    // Start is called before the first frame update
    void Start()
    {
        StartPopup();
    }

    public void StartPopup()
    {
        realDelay = Random.Range(delayMin, delayMax);
        StartCoroutine(Popup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Popup()
    {
        yield return new WaitForSeconds(realDelay);
        popper.SetActive(true);
    }

}
