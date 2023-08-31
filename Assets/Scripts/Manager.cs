using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public Inventory inventoryManager;
    public float money;
    public TextMeshProUGUI moneyUI;
    public PlayerController playerController;
    public CursorLockMode lastMode;

    private void OnEnable()
    {
        Instance = this;
    }
    private void Start()
    {
        moneyUI.text = "£0.00";
    }

    public void CursorToUI() 
    {
        lastMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.Confined;
        playerController.SetLookStatus(false);
    }
    public void CursorFromUI()
    {
        Cursor.lockState = lastMode;
        playerController.SetLookStatus(true);
    }


    public class InventoryItem
    {
        public Item item;
        public Button button;
        public int quantity;
    }

    public void GetItem(Item item)
    {
        InventoryItem inventoryItem = new InventoryItem();
        if(inventory.Count > 0)
        {
            inventoryItem = inventory.Find((x) => x.item == item);
        }
        
        if (inventoryItem.item != null)
        {
            inventoryItem.quantity++;
        }
        else
        {
            InventoryItem _item = new InventoryItem();
            _item.item = item;
            inventory.Add(_item);
        }
        inventoryManager.UpdateInventory();
    }

    public bool Buy(Item item)
    {
        if(money > item.value)
        {
            money -= item.value;
            GetItem(item);
            UpdateMoney();
            Debug.Log("Got the " + item.name);
            return true;
        }
        else
        {
            Debug.Log("Not enough money");
            return false;
        }
    }
    public void UpdateMoney()
    {
        moneyUI.text = "£" + money.ToString("###.##");
    }
}
