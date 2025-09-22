using UnityEngine;

public class ItemData : MonoBehaviour
{
    public string itemName = "Unknown Item";
    public int itemValue = 0;

    public string GetTooltipText()
    {
        return $"<b>{itemName}</b>\nValue: ${itemValue}";
    }
}

