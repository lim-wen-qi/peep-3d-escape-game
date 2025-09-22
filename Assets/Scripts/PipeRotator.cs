using UnityEngine;

public class PipeRotator : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up;
    public float rotationAmount = 90f;
    public Vector3 correctRotation;
    private bool isCorrect = false;

    public void RotatePipe()
    {
        transform.Rotate(rotationAxis * rotationAmount);
        CheckCorrect();
        PipeManager.Instance.CheckAllPipes();
    }

    void CheckCorrect()
    {
        Vector3 current = transform.localEulerAngles;
        isCorrect = Vector3.Distance(current, correctRotation) < 5f;
    }

    public bool IsCorrect() => isCorrect;
}
