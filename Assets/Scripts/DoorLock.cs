using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public enum LockType { Key, Keypad }
    [Header("Lock Settings")]
    public LockType lockType = LockType.Key;

    [Header("Key Lock Settings")]
    public string requiredKey;

    [Header("Keypad Lock Settings")]
    public string requiredCode = "1234";

    [Header("State")]
    public bool isLocked = true;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        UpdateLockState();
    }

    public void Unlock()
    {
        if (!isLocked) return;

        isLocked = false;
        UpdateLockState();
        Debug.Log($"{gameObject.name} unlocked!");
    }

    public void Lock()
    {
        if (isLocked) return;

        isLocked = true;
        UpdateLockState();
        Debug.Log($"{gameObject.name} locked.");
    }

    private void UpdateLockState()
    {
        if (rb != null)
            rb.isKinematic = isLocked;
    }

    // --- Validation Methods ---
    public bool CheckKey(string keyID)
    {
        return lockType == LockType.Key && keyID == requiredKey;
    }

    public bool CheckCode(string code)
    {
        return lockType == LockType.Keypad && code == requiredCode;
    }
}
