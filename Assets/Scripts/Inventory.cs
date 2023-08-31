using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject invVisuals;
    public List<Button> buttons;
    public GameObject previewObjParent;
    public GameObject previewUI;

    bool isDown;
    private void Start()
    {
        Manager.Instance.inventoryManager = this;
    }
    public void UpdateInventory()
    {
        List<Manager.InventoryItem> inventory = Manager.Instance.inventory;
        for (int i = 0; i < inventory.Count; i++)
        {
            int index = i;
            inventory[i].button = buttons[i];
            inventory[index].button.onClick.AddListener(delegate { Preview(inventory[index].item); });
            //these are horrible but i am in a rush!
            inventory[index].button.transform.GetComponentInChildren<TextMeshProUGUI>().text = inventory[index].item.name;
            inventory[index].button.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = inventory[index].quantity.ToString();
            inventory[index].button.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isDown)
        {
            invVisuals.SetActive(!invVisuals.activeSelf);
            if (invVisuals.activeSelf)
            {
                Manager.Instance.CursorToUI();
            }
            else
            {
                Manager.Instance.CursorFromUI();
            }
            isDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab)) 
        { 
            isDown = false;
        }

    }

    public void Preview(Item item)
    {

    }
    public void ClosePreview()
    {

    }
}
