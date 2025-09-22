using UnityEngine;
using TMPro;

public class ToiletLid : MonoBehaviour
{
    public bool canOpen = false;
    private bool isOpen = false;
    private Rigidbody rb;

    [Header("Interaction UI")]
    public GameObject interactPrompt;       // UI object for "Press E"
    public TextMeshProUGUI interactPromptText;

    public float interactDistance = 3f;
    private bool lookingAtLid = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // locked at start

        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }

    public void Unlock()
    {
        canOpen = true;
        rb.isKinematic = false; // now physics can move it
        Debug.Log("Toilet lid unlocked and now non-kinematic!");
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                lookingAtLid = true;

                // Show prompt text
                if (interactPrompt != null && interactPromptText != null)
                {
                    interactPrompt.SetActive(true);
                    interactPromptText.text = canOpen ? "[Unlocked]" : "[Locked]";
                }

                // Only toggle lid if unlocked
                if (canOpen && Input.GetKeyDown(KeyCode.E))
                {
                    ToggleLid();
                }
            }
            else
            {
                HidePrompt();
            }
        }
        else
        {
            HidePrompt();
        }
    }

    void ToggleLid()
    {
        if (!isOpen)
        {
            transform.localRotation = Quaternion.Euler(-90, 0, 0);
            isOpen = true;
        }
        else
        {
            transform.localRotation = Quaternion.identity;
            isOpen = false;
        }
    }

    void HidePrompt()
    {
        if (lookingAtLid)
        {
            if (interactPrompt != null)
                interactPrompt.SetActive(false);

            lookingAtLid = false;
        }
    }
}
