using UnityEngine;

public class PipeButton : MonoBehaviour
{
    public PipeRotator[] pipesToRotate; // assign pipes that should rotate

    [Header("Interaction UI")]
    public GameObject interactPrompt; // assign your "Press [E] to rotate" UI

    private Camera cam;
    public float interactDistance = 3f;
    private bool lookingAtButton = false;

    void Start()
    {
        cam = Camera.main;

        if (interactPrompt != null)
            interactPrompt.SetActive(false); // hide prompt at start
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // Show prompt
                if (interactPrompt != null)
                    interactPrompt.SetActive(true);

                lookingAtButton = true;

                // Press E to rotate
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActivateButton();
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

    void ActivateButton()
    {
        foreach (PipeRotator pipe in pipesToRotate)
        {
            if (pipe != null)
                pipe.RotatePipe();
        }
    }

    void HidePrompt()
    {
        if (lookingAtButton)
        {
            if (interactPrompt != null)
                interactPrompt.SetActive(false);

            lookingAtButton = false;
        }
    }
}
