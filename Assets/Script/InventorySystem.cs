using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
 
   public static InventorySystem Instance { get; set; }
 
    public GameObject inventoryScreenUI;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;
    public bool isOpen;
    // public bool isFull;
 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
 
 
    void Start()
    {
        isOpen = false;
        PopulateSlotList();
    }
 
    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if(child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }
 
    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
 
            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
 
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }
    public bool AddToInventory(string itemName)
{
    // 1. Kiểm tra xem vật phẩm này đã có sẵn trong ô nào chưa
    InventoryItem existingItem = FindItemInInventory(itemName);

    if (existingItem != null)
    {
        existingItem.AddCount(1);
        return true; // Nhặt thành công vào ô cũ
    }

    // 2. Nếu chưa có, kiểm tra xem túi đồ có đầy không
    if (CheckIfFull())
    {
        Debug.Log("Inventory is full");
        return false;
    }

    // 3. Tìm ô trống và tạo vật phẩm mới
    whatSlotToEquip = FindNextEmptySlot();
    GameObject prefab = Resources.Load<GameObject>(itemName);

    if (prefab == null)
    {
        Debug.LogError($"Không tìm thấy Prefab '{itemName}' trong Resources!");
        return false;
    }

    itemToAdd = Instantiate(prefab, whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
    itemToAdd.transform.SetParent(whatSlotToEquip.transform);
    itemToAdd.transform.localPosition = Vector3.zero;

    // Gán component InventoryItem nếu chưa có
    InventoryItem itemComp = itemToAdd.GetComponent<InventoryItem>();
    if (itemComp == null)
    {
        itemComp = itemToAdd.AddComponent<InventoryItem>();
    }
    itemComp.itemName = itemName;

    itemList.Add(itemName);
    return true;
}

// Hàm hỗ trợ tìm vật phẩm trùng tên đã có trong túi
private InventoryItem FindItemInInventory(string itemName)
{
    foreach (GameObject slot in slotList)
    {
        if (slot.transform.childCount > 0)
        {
            InventoryItem item = slot.transform.GetChild(0).GetComponent<InventoryItem>();
            if (item != null && item.itemName == itemName)
            {
                return item;
            }
        }
    }
    return null;
}
    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                counter+=1;
            }
        }
        if(counter == 21)
            {
                return true;
            }
            else
            {
                return false;
            }
    }
    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }
}
