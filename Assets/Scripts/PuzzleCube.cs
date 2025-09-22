using UnityEngine;

public class PuzzleCube : MonoBehaviour
{
    [Header("Book Settings")]
    public int bookCount;
    public Vector3 clickMove = new Vector3(0, 0, -0.015f);

    [HideInInspector]
    public bool isClicked = false;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    // Detect mouse clicks
    void OnMouseDown()
    {
        Debug.Log("Cube clicked: " + gameObject.name);

        BookPuzzleManager manager = FindObjectOfType<BookPuzzleManager>();
        if (manager != null)
        {
            manager.CubeClicked(this);
        }
        else
        {
            Debug.LogWarning("Puzzle Manager not found in scene!");
        }
    }

    // Called by manager when click is valid
    public void ClickCube()
    {
        if (!isClicked)
        {
            transform.position += clickMove;
            isClicked = true;
            Debug.Log(gameObject.name + " moved!");
        }
    }

    // Reset cube if sequence fails
    public void ResetCube()
    {
        transform.position = originalPosition;
        isClicked = false;
    }
}
