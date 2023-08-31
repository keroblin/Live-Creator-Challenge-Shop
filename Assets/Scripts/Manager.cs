using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public float money;
    public TextMeshProUGUI moneyUI;

    private void Start()
    {
        Instance = this;
        moneyUI.text = "£0.00";
    }
    public class InventoryItem
    {
        public Item item;
        public int quantity;
    }

    public void GetItem(Item item)
    {
        InventoryItem inventoryItem = new InventoryItem();
        if(inventory.Count > 0)
        {
            inventoryItem = inventory.Find((x) => x.item == item);
        }
        
        if (inventoryItem != null)
        {
            inventoryItem.quantity++;
        }
        else
        {
            InventoryItem _item = new InventoryItem();
            _item.item = item;
            inventory.Add(_item);
        }
    }

    public void Buy(Item item)
    {
        if(money > item.value)
        {
            money -= item.value;
            GetItem(item);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void UpdateMoney()
    {
        moneyUI.text = "£" + money.ToString("###.##");
    }
}
