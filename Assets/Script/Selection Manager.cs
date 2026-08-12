using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Đã bỏ UnityEngine.UI vì không cần thiết
 
public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; } // Bảo mật hơn
    
    // Có thể xóa biến Ontarget nếu không dùng ở kịch bản khác, nhưng tạm giữ lại theo code cũ
    [HideInInspector] public bool Ontarget;
 
    public GameObject interaction_Info_UI;
    private TextMeshProUGUI interaction_text;
 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Ontarget = false;
        if (interaction_Info_UI != null)
        {
            interaction_text = interaction_Info_UI.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
 
    void Update()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        // Bắn Raycast
        if (Physics.Raycast(ray, out hit))
        {
            InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
 
            // Nếu nhìn trúng vật thể VÀ người chơi đang trong vùng Trigger
            if (interactableObject != null && interactableObject.PlayerInRange)
            {   
                Ontarget = true;
                interaction_text.text = interactableObject.GetItemName();
                interaction_Info_UI.SetActive(true);

                // --- XỬ LÝ NHẶT VẬT PHẨM TẠI ĐÂY ---
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Debug.Log("Đã nhặt " + interactableObject.ItemName);
                    Destroy(interactableObject.gameObject);
                    
                    // Ẩn UI ngay lập tức sau khi nhặt
                    interaction_Info_UI.SetActive(false);
                    Ontarget = false;
                }
            }
            else 
            { 
                Ontarget = false;
                interaction_Info_UI.SetActive(false);
            }
        }
        else
        {
            // Tắt UI nếu tia Raycast không trúng bất kỳ cái gì
            Ontarget = false;
            interaction_Info_UI.SetActive(false);
        }
    }
}