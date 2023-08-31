using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Pickup
{
    public override void Interact()
    {
        onPickup.Invoke();
        Manager.Instance.money += item.value;
        Manager.Instance.UpdateMoney();
        Destroy(gameObject);
    }
}
