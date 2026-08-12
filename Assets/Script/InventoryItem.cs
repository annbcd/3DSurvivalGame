using UnityEngine;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public int count = 1;
    public TextMeshProUGUI countText; // Kéo Text hiển thị số lượng vào đây

    private void Start()
    {
        UpdateCountUI();
    }

    public void AddCount(int amount = 1)
    {
        count += amount;
        UpdateCountUI();
    }

    public void UpdateCountUI()
    {
        if (countText != null)
        {
            // Chỉ hiển thị số lượng khi lớn hơn 1
            countText.text = count > 1 ? count.ToString() : "";
        }
    }
}