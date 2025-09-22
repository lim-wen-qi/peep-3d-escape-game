using UnityEngine;

public class Candle : MonoBehaviour
{
    void Awake()
    {
        Light[] lights = GetComponentsInChildren<Light>();
        foreach (Light l in lights)
            l.enabled = false;
    }
}