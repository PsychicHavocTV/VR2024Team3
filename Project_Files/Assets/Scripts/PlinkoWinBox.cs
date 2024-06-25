using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkoWinBox : MonoBehaviour
{
    public PlinkoManager pm;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlinkoCoin")
        {
            pm.AddBox(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlinkoCoin")
        {
            pm.RemoveBox(this);
        }
    }
}
