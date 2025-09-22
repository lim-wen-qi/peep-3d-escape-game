using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootBoxManager : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI totalPriceText;

    private HashSet<ItemData> itemsInside = new HashSet<ItemData>();
    private int totalValue = 0;

    private void Start()
    {
        UpdateUI();
    }

    void OnTriggerEnter(Collider other)
    {
        ItemData item = other.GetComponent<ItemData>();
        if (item != null && !itemsInside.Contains(item))
        {
            itemsInside.Add(item);
            totalValue += item.itemValue;
            UpdateUI();
        }
    }

    void OnTriggerExit(Collider other)
    {
        ItemData item = other.GetComponent<ItemData>();
        if (item != null && itemsInside.Contains(item))
        {
            itemsInside.Remove(item);
            totalValue -= item.itemValue;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (totalPriceText != null)
        {
            totalPriceText.text = $"Total Loot:\n${totalValue}";
        }
    }
}
