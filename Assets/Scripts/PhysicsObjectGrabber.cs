using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsObjectGrabber : MonoBehaviour
{
    [Header("Grab Settings")]
    public float force = 600f;
    public float damping = 6f;
    public float maxGrabDistance = 5f;

    private Transform jointTransform;
    private float dragDepth;
    private Camera mainCamera;

    [HideInInspector] public bool isHeld = false;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        TryGrab(Input.mousePosition);

        if (jointTransform != null)
        {
            isHeld = true;
            ConfigurableJoint cj = jointTransform.GetComponent<ConfigurableJoint>();
            if (cj != null && cj.connectedBody != null)
            {
                Debug.Log("Grabbed: " + cj.connectedBody.name);
            }
        }
    }

    void OnMouseUp()
    {
        if (isHeld)
        {
            ConfigurableJoint cj = jointTransform?.GetComponent<ConfigurableJoint>();
            if (cj != null && cj.connectedBody != null)
            {
                Debug.Log("Released: " + cj.connectedBody.name);
            }
        }

        Release();
        isHeld = false;
    }

    void OnMouseDrag()
    {
        Drag(Input.mousePosition);
    }

    void TryGrab(Vector3 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        int grabMask = ~LayerMask.GetMask("LootBoxTrigger");

        if (Physics.Raycast(ray, out RaycastHit hit, maxGrabDistance, grabMask))
        {
            Rigidbody rb = hit.rigidbody;
            if (rb == null) return;

            // Ignore doors
            if (rb.GetComponent<DoorLock>() != null) return;

            // Allow to grab
            dragDepth = Vector3.Distance(mainCamera.transform.position, hit.point);
            jointTransform = CreateJoint(rb, hit.point);
        }
    }

    void Drag(Vector3 screenPosition)
    {
        if (jointTransform == null) return;

        Vector3 targetWorldPos = mainCamera.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, dragDepth)
        );

        jointTransform.position = targetWorldPos;
    }

    void Release()
    {
        if (jointTransform != null)
        {
            Destroy(jointTransform.gameObject);
            jointTransform = null;
        }
    }

    Transform CreateJoint(Rigidbody targetRb, Vector3 attachPoint)
    {
        GameObject jointGO = new GameObject("GrabJoint")
        {
            hideFlags = HideFlags.HideInHierarchy
        };

        jointGO.transform.position = attachPoint;

        Rigidbody jointRb = jointGO.AddComponent<Rigidbody>();
        jointRb.isKinematic = true;

        ConfigurableJoint joint = jointGO.AddComponent<ConfigurableJoint>();
        joint.connectedBody = targetRb;
        joint.configuredInWorldSpace = true;
        joint.rotationDriveMode = RotationDriveMode.Slerp;

        // Linear drives
        joint.xDrive = CreateJointDrive(force, damping);
        joint.yDrive = CreateJointDrive(force, damping);
        joint.zDrive = CreateJointDrive(force, damping);

        // Angular drive
        joint.slerpDrive = CreateJointDrive(force, damping);

        return jointGO.transform;
    }

    JointDrive CreateJointDrive(float spring, float damper)
    {
        return new JointDrive
        {
            positionSpring = spring,
            positionDamper = damper,
            maximumForce = Mathf.Infinity
        };
    }
}
