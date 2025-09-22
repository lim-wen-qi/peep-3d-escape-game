using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public static PipeManager Instance;
    public PipeRotator[] allPipes;
    public ToiletLid lid;

    private void Awake() => Instance = this;

    public void CheckAllPipes()
    {
        foreach (PipeRotator pipe in allPipes)
        {
            Debug.Log(pipe.name + " correct? " + pipe.IsCorrect());
            if (!pipe.IsCorrect()) return;
        }

        lid.Unlock();
    }
}
