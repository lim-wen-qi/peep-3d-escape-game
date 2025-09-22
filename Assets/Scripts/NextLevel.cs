using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [Header("Scene to Load")]
    public string level2SceneName = "Level2";

    [Header("Interaction Settings")]
    public float maxDistance = 5f; // how far the player can click
    public Camera playerCamera;     // assign main camera here

    [Header("Player Key")]
    public KeyCode interactKey = KeyCode.Mouse0; // left mouse button

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Clicked Play Button! Loading Level 2...");
                    SceneManager.LoadScene(level2SceneName);
                }
            }
        }
    }
}
