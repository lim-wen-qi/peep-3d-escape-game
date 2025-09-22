using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public enum ButtonType { Play, Quit }
    public ButtonType buttonType;

    private void OnMouseDown()
    {
        if (buttonType == ButtonType.Play)
        {
            Debug.Log("Play clicked!");
            SceneManager.LoadScene("Level1");
        }
        else if (buttonType == ButtonType.Quit)
        {
            Debug.Log("Quit clicked!");
            Application.Quit();
        }
    }
}
