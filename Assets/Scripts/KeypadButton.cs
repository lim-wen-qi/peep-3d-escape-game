using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public string buttonValue;

    public void Press(Keypad keypad)
    {
        if (buttonValue == "Enter")
        {
            keypad.Enter();
        }
        else
        {
            keypad.AddDigit(buttonValue);
        }
    }
}
