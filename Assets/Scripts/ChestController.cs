using UnityEngine;

public class ChestController : MonoBehaviour
{
    [Header("Chest Parts")]
    public HingeJoint lidJoint;
    public GameObject lockObject; // visual lock object

    private bool unlocked = false;
    private JointLimits lockedLimits;
    private JointLimits openLimits;

    void Start()
    {
        if (lidJoint != null)
        {
            // Save hinge open limits (set in inspector, e.g., 0 to 70)
            openLimits = lidJoint.limits;

            // Create locked limits (force lid shut at 0)
            lockedLimits = lidJoint.limits;
            lockedLimits.min = 0;
            lockedLimits.max = 0;

            // Apply locked state at start
            lidJoint.limits = lockedLimits;
            lidJoint.useLimits = true;
        }

        // Lock object visible at start
        if (lockObject != null)
            lockObject.SetActive(true);
    }

    /// <summary>
    /// Unlocks the chest and allows lid to open.
    /// </summary>
    public void UnlockChest()
    {
        if (unlocked) return;

        unlocked = true;

        // Restore hinge open limits
        if (lidJoint != null)
        {
            lidJoint.limits = openLimits;
            lidJoint.useLimits = true;
        }

        // Hide visual lock
        if (lockObject != null)
            lockObject.SetActive(false);

        Debug.Log("Chest unlocked! Lid can now be opened.");
    }

    /// <summary>
    /// Public getter for unlocked state.
    /// </summary>
    /// <returns>True if chest is unlocked, false otherwise.</returns>
    public bool IsUnlocked()
    {
        return unlocked;
    }
}
