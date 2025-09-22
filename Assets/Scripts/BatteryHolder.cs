using UnityEngine;

public class BatteryHolder : MonoBehaviour
{
    [Header("Battery")]
    public GameObject batteryInHolder;
    public float interactDistance = 3f;

    [Header("Switch Box Lights")]
    public GameObject redLight;
    public GameObject redLightOff;
    public GameObject greenLight;
    public GameObject greenLightOff;

    [Header("Scene Lights")]
    public LightManager lightManager;

    [Header("Keypad Material")]
    public Renderer keypadRenderer;
    public Material keypadPoweredMaterial;
    public Material keypadUnpoweredMaterial;

    private Camera cam;
    private bool isFilled = false;

    void Start()
    {
        cam = Camera.main;

        // Hide battery in holder at start
        if (batteryInHolder != null)
            batteryInHolder.SetActive(false);

        // Lights setup at start
        if (redLight != null) redLight.SetActive(true);
        if (redLightOff != null) redLightOff.SetActive(false);
        if (greenLight != null) greenLight.SetActive(false);
        if (greenLightOff != null) greenLightOff.SetActive(true);

        // Keypad material at start
        if (keypadRenderer != null && keypadUnpoweredMaterial != null)
            keypadRenderer.material = keypadUnpoweredMaterial;
    }

    void Update()
    {
        if (isFilled) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, interactDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    TryInsertBattery();
                }
            }
        }
    }

    void TryInsertBattery()
    {
        // Check if the player is holding a battery
        PhysicsObjectGrabber[] grabbers = FindObjectsOfType<PhysicsObjectGrabber>();
        foreach (var grabber in grabbers)
        {
            if (grabber.isHeld)
            {
                // Get the held object
                GameObject heldBattery = grabber.transform.parent ? grabber.transform.parent.gameObject : grabber.gameObject;

                // Show battery in holder
                if (batteryInHolder != null) batteryInHolder.SetActive(true);

                // Remove the battery
                Destroy(heldBattery);

                // Update lights
                if (redLight != null) redLight.SetActive(false);
                if (redLightOff != null) redLightOff.SetActive(true);
                if (greenLight != null) greenLight.SetActive(true);
                if (greenLightOff != null) greenLightOff.SetActive(false);

                // Change keypad material to powered
                if (keypadRenderer != null && keypadPoweredMaterial != null)
                    keypadRenderer.material = keypadPoweredMaterial;

                // Power up the keypad
                Keypad keypad = FindObjectOfType<Keypad>();
                if (keypad != null)
                    keypad.PowerOn();

                // Scene lights
                if (lightManager != null)
                {
                    lightManager.PowerOn();
                }

                isFilled = true;
                Debug.Log("Battery inserted! Lights updated and keypad powered.");
                return;
            }
        }

        Debug.Log("You are not holding a battery!");
    }
}
