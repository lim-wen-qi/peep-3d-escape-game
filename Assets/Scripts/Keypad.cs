using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    [Header("Door Lock Reference")]
    public DoorLock targetDoor;

    [Header("Code Settings")]
    public string currentInput = "";
    public int maxDigits = 4;

    [Header("UI Display")]
    public TextMeshProUGUI displayText;

    [Header("Power Settings")]
    public bool isPowered = false;

    void Update()
    {
        if (!isPowered) return; // no power, no input

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 3f))
            {
                KeypadButton button = hit.collider.GetComponent<KeypadButton>();
                if (button != null)
                {
                    button.Press(this);
                }
            }
        }
    }

    public void AddDigit(string digit)
    {
        if (!isPowered) return; // ignore input if no power

        if (currentInput.Length < maxDigits)
        {
            currentInput += digit;
            UpdateDisplay();
        }
    }

    public void Enter()
    {
        if (!isPowered) return;

        if (targetDoor != null && targetDoor.CheckCode(currentInput))
        {
            targetDoor.Unlock();
            Debug.Log("Correct code! Door unlocked.");
            if (displayText != null) displayText.text = "Granted";
        }
        else
        {
            Debug.Log("Incorrect code.");
            if (displayText != null) displayText.text = "Error";
        }

        Invoke(nameof(ClearInput), 1.5f);
    }

    private void ClearInput()
    {
        currentInput = "";
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
            displayText.text = currentInput;
    }

    // Called from BatteryHolder when power is restored
    public void PowerOn()
    {
        isPowered = true;
        ClearInput();
        Debug.Log("Keypad is now powered!");
    }
}
