using UnityEngine;
using TMPro;

public class ChestPasswordUI : MonoBehaviour
{
    [Header("Settings")]
    public float interactDistance = 5f;
    public LayerMask interactLayer; // assign to "Chest" layer
    public Camera playerCamera;
    public GameObject cursorDot;

    [Header("UI")]
    public GameObject interactPromptUI;
    public TextMeshProUGUI interactPromptText;
    public GameObject passwordPanel;
    public TextMeshProUGUI passwordText;

    [Header("Chest")]
    public ChestController chest;
    public string correctPassword = "1234";

    private string input = "";
    private GameObject currentTarget;

    void Start()
    {
        passwordPanel.SetActive(false);
        interactPromptUI.SetActive(false);
        cursorDot.SetActive(true);
        UpdatePasswordUI();
    }

    void Update()
    {
        CheckForChest();

        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
        {
            if (!InteractionManager.isInteracting)
            {
                passwordPanel.SetActive(true);
                cursorDot.SetActive(false);
                interactPromptUI.SetActive(false);
                InteractionManager.isInteracting = true;
            }
            else if (passwordPanel.activeSelf)
            {
                passwordPanel.SetActive(false);
                cursorDot.SetActive(true);
                interactPromptUI.SetActive(true);
                InteractionManager.isInteracting = false;
            }
        }

        if (passwordPanel.activeSelf)
            HandlePasswordInput();
    }

    void CheckForChest()
    {
        currentTarget = null;
        if (interactPromptUI != null && !passwordPanel.activeSelf)
            interactPromptUI.SetActive(false);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            if (hit.collider.CompareTag("Chest"))
            {
                currentTarget = hit.collider.GetComponentInParent<ChestController>().gameObject;
                ChestController chestCtrl = currentTarget.GetComponent<ChestController>();

                if (chestCtrl != null && !chestCtrl.IsUnlocked() && !passwordPanel.activeSelf)
                {
                    interactPromptText.text = "Find the password.\nPress [E] to enter password";
                    interactPromptUI.SetActive(true);
                }
            }
        }
    }

    void HandlePasswordInput()
    {
        for (KeyCode k = KeyCode.Alpha0; k <= KeyCode.Alpha9; k++)
        {
            if (Input.GetKeyDown(k) && input.Length < correctPassword.Length)
            {
                input += (k - KeyCode.Alpha0).ToString();
                UpdatePasswordUI();
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace) && input.Length > 0)
        {
            input = input.Substring(0, input.Length - 1);
            UpdatePasswordUI();
        }

        if (input.Length == correctPassword.Length)
        {
            if (input == correctPassword)
            {
                chest.UnlockChest();
                passwordPanel.SetActive(false);
                cursorDot.SetActive(true);
                interactPromptText.text = "[Unlocked]";
                interactPromptUI.SetActive(true);

                input = "";
                UpdatePasswordUI();
                InteractionManager.isInteracting = false;
            }
            else
            {
                input = "";
                UpdatePasswordUI();
            }
        }
    }

    void UpdatePasswordUI()
    {
        string display = "";
        for (int i = 0; i < correctPassword.Length; i++)
            display += (i < input.Length) ? input[i] + " " : "_ ";

        passwordText.text = "Enter Password:\n" + display;
    }
}
