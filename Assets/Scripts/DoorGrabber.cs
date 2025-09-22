using UnityEngine;

public class DoorGrabber : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask doorLayer;
    [SerializeField] private float grabRange = 5f;

    private Transform selectedDoor;
    private GameObject dragPoint;
    private HingeJoint hinge;
    private Rigidbody doorRb;

    void Update()
    {
        HandleUnlockInput();
        HandleGrab();
        HandleDragging();
    }

    private void HandleUnlockInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, grabRange, doorLayer))
            {
                DoorLock doorLock = hit.collider.GetComponent<DoorLock>();
                if (doorLock != null && doorLock.isLocked)
                {
                    // Find the currently held object
                    PhysicsObjectGrabber[] allGrabbers = FindObjectsOfType<PhysicsObjectGrabber>();
                    PhysicsObjectGrabber heldKey = null;

                    foreach (var grabber in allGrabbers)
                    {
                        if (grabber.isHeld)
                        {
                            heldKey = grabber;
                            break;
                        }
                    }

                    if (heldKey != null)
                    {
                        KeyItem key = heldKey.GetComponent<KeyItem>();
                        if (key == null)
                            key = heldKey.GetComponentInChildren<KeyItem>();

                        if (key != null && key.keyID == doorLock.requiredKey)
                        {
                            // Unlock the door
                            doorLock.isLocked = false;
                            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                            if (rb != null) rb.isKinematic = false;

                            HingeJoint hj = hit.collider.GetComponent<HingeJoint>();
                            if (hj != null) hj.useMotor = false;
                            
                            Destroy(heldKey.gameObject);

                            Debug.Log("Door unlocked with key: " + key.keyID);
                        }
                        else
                        {
                            Debug.Log("Held key is not the correct key for this door.");
                        }
                    }
                    else
                    {
                        Debug.Log("You are not holding any key.");
                    }
                }
            }
        }
    }

    private void HandleGrab()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, grabRange, doorLayer))
            {
                DoorLock doorLock = hit.collider.GetComponent<DoorLock>();

                // Locked doors cannot be grabbed
                if (doorLock != null && doorLock.isLocked)
                    return;

                doorRb = hit.collider.GetComponent<Rigidbody>();
                selectedDoor = hit.collider.transform;
                hinge = selectedDoor.GetComponent<HingeJoint>();

                if (hinge != null && doorRb != null)
                {
                    dragPoint = new GameObject("DragPoint");
                    doorRb.isKinematic = false; // allow dragging
                    hinge.useMotor = true;
                }
            }
        }
    }

    private void HandleDragging()
    {
        if (selectedDoor != null && hinge != null && doorRb != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            dragPoint.transform.position = ray.GetPoint(Vector3.Distance(selectedDoor.position, cam.transform.position));

            Vector3 localPos = selectedDoor.InverseTransformPoint(dragPoint.transform.position);
            float direction = Mathf.Sign(localPos.x);

            JointMotor motor = hinge.motor;
            motor.force = 50f;
            motor.targetVelocity = direction * 70f;
            hinge.motor = motor;

            if (Input.GetMouseButtonUp(0))
            {
                selectedDoor = null;
                if (hinge != null)
                {
                    JointMotor holdMotor = hinge.motor;
                    holdMotor.force = 100f;
                    holdMotor.targetVelocity = 0f;
                    hinge.motor = holdMotor;
                    hinge.useMotor = true;
                }

                Destroy(dragPoint);
            }
        }
    }
}
