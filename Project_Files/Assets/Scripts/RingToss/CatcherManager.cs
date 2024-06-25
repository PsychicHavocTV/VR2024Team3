using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Rendering;

public class CatcherManager : MonoBehaviour
{
    public RingManager rManager;
    public Color ringCaughtColor;
    private IList<Material> catcherMaterials = new List<Material>();
    [SerializeField] private MeshRenderer[] catcherRenderers;

    private void Start()
    {
        foreach (MeshRenderer catcherRend in catcherRenderers)
        {
            catcherMaterials.Add(catcherRend.material);
        }
    }

    public void ChangeColor()
    {
        foreach (Material catcherMat in catcherMaterials)
        {
            catcherMat.color = ringCaughtColor;
        }
    }
}
