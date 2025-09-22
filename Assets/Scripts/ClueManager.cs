using UnityEngine;
using TMPro;

public class ClueManager : MonoBehaviour
{
    [Header("Clue UI")]
    public GameObject cluePanel;
    public TextMeshProUGUI clueText;
    public GameObject interactPrompt;

    [Header("Interaction")]
    public float interactDistance = 5f;
    public LayerMask interactLayer; // assign to "Clue" layer

    private Camera cam;
    private bool lookingAtPaper = false;

    void Start()
    {
        cam = Camera.main;
        cluePanel.SetActive(false);
        interactPrompt.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            if (hit.collider.CompareTag("Clue"))
            {
                // Show prompt if panel is closed and not interacting
                if (!cluePanel.activeSelf && !InteractionManager.isInteracting)
                    interactPrompt.SetActive(true);

                lookingAtPaper = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!InteractionManager.isInteracting)
                    {
                        CluePaper paper = hit.collider.GetComponentInParent<CluePaper>();
                        if (paper != null)
                        {
                            clueText.text = paper.clueMessage;
                            cluePanel.SetActive(true);
                            interactPrompt.SetActive(false);
                            InteractionManager.isInteracting = true;
                        }
                    }
                    else if (cluePanel.activeSelf)
                    {
                        cluePanel.SetActive(false);
                        interactPrompt.SetActive(true);
                        InteractionManager.isInteracting = false;
                    }
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

    void HidePrompt()
    {
        if (lookingAtPaper)
        {
            interactPrompt.SetActive(false);
            lookingAtPaper = false;
        }
    }
}
