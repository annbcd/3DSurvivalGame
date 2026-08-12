using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class InteractableObject : MonoBehaviour
{
    // Thêm [HideInInspector] để biến này không hiện ra ở Unity Editor (tránh click nhầm), 
    // nhưng các script khác vẫn có thể truy cập được.
    [HideInInspector] public bool PlayerInRange = false; 
    public string ItemName;
 
    public string GetItemName()
    {
        return ItemName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
    
    // Đã xóa hàm Update() ở đây
}