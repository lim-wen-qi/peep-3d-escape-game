using TMPro;
using UnityEngine;

public class CandleLighter : MonoBehaviour
{
    [Header("Settings")]
    public float interactDistance = 5f;
    public LayerMask interactLayer;
    public GameObject lighter;

    [Header("UI")]
    public GameObject interactPromptUI;       // Parent panel or GameObject
    public TextMeshProUGUI interactPromptText; // TMP text component
    
    [Header("Puzzle Shadow")]
    public GameObject ShadowNumber;

    private Camera cam;
    private GameObject currentTarget;

    void Start()
    {
        cam = Camera.main;

        if (interactPromptUI != null)
            interactPromptUI.SetActive(false); // Hide at start

        // Hide shadow number at start
        if (ShadowNumber != null)
            ShadowNumber.SetActive(false);
    }

    void Update()
    {
        CheckForCandle();

        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
        {
            LightUpCandle(currentTarget);
        }
    }

    void CheckForCandle()
    {
        currentTarget = null;

        if (interactPromptUI != null)
            interactPromptUI.SetActive(false);

        // Check lighter
        if (lighter == null) return;

        PhysicsObjectGrabber grabber = lighter.GetComponent<PhysicsObjectGrabber>();
        if (grabber == null || !grabber.isHeld) return;

        // Raycast from center of screen
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            Transform parent = hit.collider.GetComponentInParent<Transform>();
            Debug.Log("Raycast hit: " + hit.collider.name + " | Parent: " + parent.name + " | Tag: " + parent.tag);

            if (parent.CompareTag("Candle"))
            {
                currentTarget = parent.gameObject;

                // Show UI
                if (interactPromptUI != null && interactPromptText != null)
                {
                    interactPromptText.text = "Press [E] to light candle";
                    interactPromptUI.SetActive(true);
                }
            }
        }
    }

    void LightUpCandle(GameObject candle)
    {
        // Enable lights
        Debug.Log("Candle name: " + candle.name + " | Children: " + candle.transform.childCount);
        Light[] lights = candle.GetComponentsInChildren<Light>();
        Debug.Log("Number of lights found: " + lights.Length);
        foreach (Light l in lights)
        {
            l.enabled = true;
            Debug.Log("Enabled light: " + l.name);
        }
        // Show shadow numbers
        if (ShadowNumber != null)
            ShadowNumber.SetActive(true);

        // Hide UI
        if (interactPromptUI != null)
            interactPromptUI.SetActive(false);

        Debug.Log("Candle lit: " + candle.name);
    }
}
