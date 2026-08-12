using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; }
    public bool onTarget;

    public GameObject interaction_Info_UI;
    private TextMeshProUGUI interaction_text;

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

    private void Start()
    {
        onTarget = false;

        if (interaction_Info_UI != null && interaction_text == null)
        {
            interaction_text = interaction_Info_UI.GetComponentInChildren<TextMeshProUGUI>();
        }

        if (interaction_Info_UI != null)
        {
            interaction_Info_UI.SetActive(false);
        }
    }

    void Update()
    {
        if (Camera.main == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            InteractableObject interactableObject = selectionTransform.GetComponent<InteractableObject>();

            if (interactableObject != null && interactableObject.playerInRange)
            {
                onTarget = true;

                if (interaction_text != null)
                {
                    interaction_text.text = interactableObject.GetItemName();
                }

                if (interaction_Info_UI != null)
                    interaction_Info_UI.SetActive(true);
            }
            else
            {
                onTarget = false;
                if (interaction_Info_UI != null)
                    interaction_Info_UI.SetActive(false);
            }
        }
        else
        {
            onTarget = false;
            if (interaction_Info_UI != null)
                interaction_Info_UI.SetActive(false);
        }
    }
}
