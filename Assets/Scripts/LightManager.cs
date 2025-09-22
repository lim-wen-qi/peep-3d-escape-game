using UnityEngine;

public class LightManager : MonoBehaviour
{
    [Header("Lights that should be off at start")]
    public Light[] lightsToControl;

    private void Start()
    {
        // Make sure all lights start OFF
        foreach (Light l in lightsToControl)
        {
            if (l != null) l.enabled = false;
        }
    }

    // Call this when the generator is powered on
    public void PowerOn()
    {
        foreach (Light l in lightsToControl)
        {
            if (l != null) l.enabled = true;
        }

        Debug.Log("Lights powered on!");
    }
}