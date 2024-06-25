using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedbackController : MonoBehaviour
{
    public XRBaseController xr;
    public void ActivateHaptic(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            xr.SendHapticImpulse(0.5f, 0.1f);
        }
        return;
    }


}
