using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;

    public string ItemName;
 
    public string GetItemName()
    {
        return ItemName;
    }

   void Update()
{
    if(Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.Instance.onTarget)
    {
        // Thử thêm vào túi đồ, nếu thành công mới xóa vật thể khỏi bản đồ
        if(InventorySystem.Instance.AddToInventory(ItemName))
        {
            Destroy(gameObject);
        }
    }
}   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
