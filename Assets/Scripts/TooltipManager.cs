using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float rayDistance = 10f;
    public LayerMask grabbableLayer;

    [Header("UI Elements")]
    public TextMeshProUGUI tooltipText;
    public GameObject tooltipUI;
    public Vector3 tooltipOffset = new Vector3(0, 0.5f, 0); // Slightly above the hit point

    private Camera cam;
    private Transform lastHit;

    void Start()
    {
        cam = Camera.main;
        if (tooltipUI != null)
            tooltipUI.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            ItemData valuable = hit.collider.GetComponent<ItemData>();

            if (valuable != null)
            {
                // Refresh if looking at new object
                if (hit.transform != lastHit)
                {
                    tooltipText.text = valuable.GetTooltipText();
                    lastHit = hit.transform;
                }

                tooltipUI.SetActive(true);
                tooltipUI.transform.position = hit.point + tooltipOffset;
                return;
            }
        }

        // Hide tooltip if no objects
        tooltipUI.SetActive(false);
        lastHit = null;
    }
}

